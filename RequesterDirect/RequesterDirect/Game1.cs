using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RequesterDirect.Content;
using RequesterDirect.Content.Controllers;
using RequesterDirect.Content.Views;
using SharpDX.Direct2D1;
using System.Collections.Generic;
using System.Drawing;
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
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Disable V-Sync
            _graphics.SynchronizeWithVerticalRetrace = false;

            // Optional: Set IsFixedTimeStep to false for more control over update timing
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            int windowWidth = GraphicsDevice.DisplayMode.Width / 2;
            int windowHeight = GraphicsDevice.DisplayMode.Height / 2;

            // Setup frame buffer.

            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
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
        }

        protected override void UnloadContent()
        {
            _renderTarget.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(IsActive)
            {
                _updateController.Update();
                _perspective.Update();
            }

            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            _frameCount++;

            if (_elapsedTime >= FPS_UPDATE_INTERVAL)
            {
                int fps = (int)(_frameCount / _elapsedTime);
                _fpsText = $"FPS: {fps}";
                _elapsedTime = 0;
                _frameCount = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.FromNonPremultiplied(178, 178, 178, 255));

            _drawController.GetSpriteBatch().Begin();

            _drawController.Draw();
            Drawing.String(_drawController.GetSpriteBatch(), Globals.Fonts["Arial Bold"], new Vector2(10, 10), Color.Red, _fpsText);

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
