using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoECS.Engine;
using MonoECS.Engine.Scenes;
using MonoECS.Engine.ViewportAdapters;
using MoorhuhnRemake.Data;
using MoorhuhnRemake.Src;
using MoorhuhnRemake.Src.Scenes;

namespace MoorhuhnRemake
{
    public class GameApp : Game
    {
        public const int DefaultWidth = 1920, DefaultHeight = 1080;

        private GraphicsDeviceManager _graphics;
        private SceneManager _sceneManager;

        public GameApp()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            IsFixedTimeStep = true;

            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
           
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = 1680;
            _graphics.PreferredBackBufferHeight = 980;

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            _graphics.PreferMultiSampling = false;
            _graphics.PreferredDepthStencilFormat = DepthFormat.None;
            _graphics.SynchronizeWithVerticalRetrace = true;

            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Assets
            var assets = new Assets(Content);
            assets.LoadContent();

            // Scene Manager
            _sceneManager = new SceneManager(this);
            Components.Add(_sceneManager);

            _sceneManager.LoadScene(new PlayScene(this));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public Vector2 GetViewScale()
        {
            return new Vector2((float)GraphicsDevice.Viewport.Width / GameApp.DefaultWidth, (float)GraphicsDevice.Viewport.Height / GameApp.DefaultHeight);
        }
    }
}
