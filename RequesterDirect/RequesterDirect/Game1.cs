using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RequesterDirect.Content;
using RequesterDirect.Content.Controllers;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace RequesterDirect
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private DrawController _drawController;
        private ContentController _contentController;
        private UpdateController _updateController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(IsActive)
            {
                _updateController.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(178, 178, 178, 255));

            _drawController.Draw();

            base.Draw(gameTime);
        }
    }
}
