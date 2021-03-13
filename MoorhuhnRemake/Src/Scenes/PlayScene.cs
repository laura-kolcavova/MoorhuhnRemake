using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoECS.Ecs;
using MonoECS.Engine;
using MonoECS.Engine.Events;
using MonoECS.Engine.Graphics;
using MonoECS.Engine.Graphics.Sprites;
using MonoECS.Engine.Physics;
using MonoECS.Engine.Scenes;
using MonoECS.Engine.ViewportAdapters;
using MoorhuhnRemake.Data;
using MoorhuhnRemake.Src.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src.Scenes
{
    public class PlayScene : IScene
    {
        private readonly GameApp _gameApp;
       
        private World _world;
        private EntityFactory _entityFactory;
        private EventManager _eventManager;

        public PlayScene(GameApp gameApp)
        {
            _gameApp = gameApp;
        }

        public void LoadContent()
        {
            // EntityFactory
            _entityFactory = new EntityFactory(_gameApp);
            _gameApp.Services.AddService<EntityFactory>(_entityFactory);

            // Map
            var mapInfo = new MapInfo(0, 0, GameApp.DefaultWidth, GameApp.DefaultHeight);
            _gameApp.Services.AddService<MapInfo>(mapInfo);

            // Camera
            var viewportAdapter = new ScalingViewportAdapter(_gameApp.GraphicsDevice, GameApp.DefaultWidth, GameApp.DefaultHeight);

            var camera = new OrthographicCamera(viewportAdapter);
            _gameApp.Services.AddService<OrthographicCamera>(camera);

            // Event Manager
            _eventManager = new EventManager();
            _gameApp.Services.AddService(_eventManager);

            // World
            _world = new WorldBuilder()                    
                .AddSystem(new ChickenSpawnSystem(_gameApp))
                .AddSystem(new ChickenDestroySystem(_gameApp))
                .AddSystem(new ChickenHitSystem(_gameApp))
                .AddSystem(new PlayerControlSystem(_gameApp))
                .AddSystem(new PhysicsSystem())
                .AddSystem(new AnimatedSpriteSystem())

                .AddSystem(new RendererSystem(_gameApp))
                .AddSystem(new RenderHudSystem(_gameApp))

                .Build();

            _entityFactory.LoadContent(_world);

            // Background
            BuildBackground();

            // Cursor
            Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.TextureAimCursor, 18, 18));
        }

        public void Dispose()
        {
            _gameApp.Services.RemoveService(typeof(EntityFactory));
            _gameApp.Services.RemoveService(typeof(MapInfo));
            _gameApp.Services.RemoveService(typeof(OrthographicCamera));
            _gameApp.Services.RemoveService(typeof(EventManager));
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            _eventManager.ProcessEvents();
            _world.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _world.Draw(gameTime);
        }

        private void BuildBackground()
        {
            CreateBackground(Assets.BgSky, Vector2.Zero);

            CreateBackground(Assets.BgClouds1, Vector2.Zero);

            CreateBackground(Assets.BgClouds2, Vector2.Zero);

            CreateBackground(Assets.BgClouds3, Vector2.Zero);

            CreateBackground(Assets.BgClouds4, Vector2.Zero);

            CreateBackground(Assets.BgRocks1, Vector2.Zero);

            CreateBackground(Assets.BgRocks2, Vector2.Zero);

        }

        private void CreateBackground(Texture2D texture, Vector2 position)
        {
            var entity = _entityFactory.CreateBackground();
            var renderer = entity.GetComponent<Renderer>();
            var transform = entity.GetComponent<Transform2D>();

            renderer.MainTexture = texture;
            renderer.Depth = DepthLayers.BACKGROUND;

            transform.Position = position;
            transform.Size = new Vector2(GameApp.DefaultWidth, GameApp.DefaultHeight);
        }
       
    }
}
