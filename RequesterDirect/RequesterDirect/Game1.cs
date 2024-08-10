using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RequesterDirect.Content;
using RequesterDirect.Content.Controllers;
using RequesterDirect.Content.Models;
using RequesterDirect.Content.Views;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Color = Microsoft.Xna.Framework.Color;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace RequesterDirect
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private DrawController _drawController;
        private ContentController _contentController;
        private UpdateController _updateController;
        private PerspectiveObjectView _perspective;
        private RenderTarget2D _renderTarget;

        private string _fpsText = "";
        private int _frameCount;
        private double _elapsedTime;
        private const double FPS_UPDATE_INTERVAL = 1.0; // Update FPS every 1 second

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.DeviceReset += OnResize;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            Window.AllowUserResizing = true;

            // Disable V-Sync
            _graphics.SynchronizeWithVerticalRetrace = false;

            // Optional: Set IsFixedTimeStep to false for more control over update timing
            IsFixedTimeStep = false;

            //Load Libraries
            foreach(string path in Directory.GetFiles(@"libs\\"))
            {
                if (path.ToLower().EndsWith(".dll") && path.ToLower().Contains("addon_"))
                {
                    try
                    {
                        FileInfo info = new FileInfo(path);
                        Assembly assembly = Assembly.LoadFrom(path);

                        Type type = assembly.GetType($"{info.Name.Split(".dll")[0].Replace("addon_", "")}.Main");

                        // Create an instance of the type
                        object instance = Activator.CreateInstance(type);

                        // Invoke a method on the instance
                        MethodInfo method = type.GetMethod("LibraryLoad");
                        method.Invoke(instance, null);

                        Console.WriteLine($"Loaded Addon: {info.Name}");

                        Globals.LoadedAssemblies.Add(new()
                        {
                            Name = info.Name.Replace("addon_", ""),
                            type = type,
                            instance = instance
                        });
                    }catch(Exception e)
                    {
                        Console.WriteLine($"Failed to load Addon: {path}");
                    }
                }
            }
        }

        protected override void Initialize()
        {
            int windowWidth = GraphicsDevice.DisplayMode.Width / 2;
            int windowHeight = GraphicsDevice.DisplayMode.Height / 2;
            Globals.WindowSize = new Size(windowWidth, windowHeight);

            // Setup frame buffer.

            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        public void OnResize(Object sender, EventArgs e)
        {
            // Create the render target
            _renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None
            );

            if ((_graphics.PreferredBackBufferWidth != _graphics.GraphicsDevice.Viewport.Width) ||
                (_graphics.PreferredBackBufferHeight != _graphics.GraphicsDevice.Viewport.Height))
            {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
                _graphics.ApplyChanges();

            }

            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;
            Globals.WindowSize = new Size(windowWidth, windowHeight);
            LoadContent();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Globals.Frames = new();
            Globals.Fonts = new();
            Globals.DebugLabels = new();
            _drawController = new(new SpriteBatch(GraphicsDevice));
            _contentController = new(GraphicsDevice, Content);
            _updateController = new();
            _perspective = new();

            // Create the render target
            _renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None
            );

            //Run Library functions
            foreach (LibraryModel library in Globals.LoadedAssemblies)
            {
                try
                {
                    // Invoke a method on the instance
                    MethodInfo method = library.type.GetMethod("LoadContent");
                    method.Invoke(library.instance, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to execute LoadContent function in Library");
                }
            }
        }

        protected override void UnloadContent()
        {
            _renderTarget.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(!IsActive) { return; }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _updateController.Update();
            _perspective.Update();

            #region FPS
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            _frameCount++;

            if (_elapsedTime >= FPS_UPDATE_INTERVAL)
            {
                int fps = (int)(_frameCount / _elapsedTime);
                _fpsText = $"FPS: {fps}";
                _elapsedTime = 0;
                _frameCount = 0;
            }

            if (!Globals.DebugLabels.ContainsKey("fps"))
            {
                Globals.DebugLabels.Add("fps", _fpsText);
            } 
            else
            {
                Globals.DebugLabels["fps"] = _fpsText;
            }

            #endregion

            //Run Library functions
            foreach (LibraryModel library in Globals.LoadedAssemblies)
            {
                try
                {
                    // Invoke a method on the instance
                    MethodInfo method = library.type.GetMethod("Update");
                    method.Invoke(library.instance, null);
                }
                catch (Exception ex) { }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!IsActive) { return; }

            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.FromNonPremultiplied(178, 178, 178, 255));

            _drawController.GetSpriteBatch().Begin();

            //Main Draw
            _drawController.Draw();

            //Draw debugging labels
            int offsety = 0;
            foreach (string value in Globals.DebugLabels.Values)
            {
                Drawing.String(_drawController.GetSpriteBatch(), Globals.Fonts["Arial Bold"], new Vector2(10, 50 + offsety), Color.Red, value);
                offsety += 15;
            }

            //Run Library functions
            foreach (LibraryModel library in Globals.LoadedAssemblies)
            {
                try
                {
                    // Invoke a method on the instance
                    MethodInfo method = library.type.GetMethod("Draw");
                    method.Invoke(library.instance, null);
                }catch(Exception ex) { }
            }

            _drawController.GetSpriteBatch().End();

            // Reset the render target to null to draw to the screen
            GraphicsDevice.SetRenderTarget(null);

            // Draw the render target to the screen
            GraphicsDevice.Clear(Color.Black); // Clear the screen to a solid color

            _drawController.GetSpriteBatch().Begin();
            _drawController.GetSpriteBatch().Draw(_renderTarget, Vector2.Zero, Color.White);
            _drawController.GetSpriteBatch().End();

            base.Draw(gameTime);
        }
    }
}
