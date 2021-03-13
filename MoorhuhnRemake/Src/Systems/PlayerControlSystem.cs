using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using MonoECS.Collections;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using MonoECS.Engine;
using MonoECS.Engine.Events;
using MonoECS.Engine.Graphics.Sprites;
using MonoECS.Engine.Physics;
using MonoECS.MathExt;
using MoorhuhnRemake.Data;
using MoorhuhnRemake.Src.Components;
using MoorhuhnRemake.Src.Events;
using MoorhuhnRemake.Src.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoorhuhnRemake.Src.Systems
{
    class PlayerControlSystem : BaseSystem, IUpdateSystem
    {
        public const int AmmoCapacity = 8;
        public const float CameraMovementSpeed = 500f;

        private readonly GameApp _gameApp;
        private readonly EntityFactory _entityFactory;
        private readonly OrthographicCamera _camera;
        private readonly MapInfo _mapInfo;
        private readonly EventManager _eventManager;

        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;

        private MouseState _currentMouseState;
        private MouseState _prevMouseState;

        private int _currentAmmo;
        private Bag<int> _ammoEntities;
        private bool _ammoBuilded;

        public PlayerControlSystem(GameApp gameApp)
        {
            _gameApp = gameApp;
            _entityFactory = gameApp.Services.GetService<EntityFactory>();
            _camera = gameApp.Services.GetService<OrthographicCamera>();
            _mapInfo = gameApp.Services.GetService<MapInfo>();
            _eventManager = gameApp.Services.GetService<EventManager>();

            // Properties
            _currentAmmo = AmmoCapacity;
        }

        public override void Initialize(IComponentMapperService componentService)
        {   
            // Components
            _animatedSpriteMapper = componentService.GetMapper<AnimatedSprite>();
        }

        public void Update(GameTime gameTime)
        {
            if(!_ammoBuilded)
            {
                _ammoEntities = BuildAmmo();
                _ammoBuilded = true;
            }
            // Current MouseState
            _currentMouseState = Mouse.GetState();

            // Move Camera
            var move = GetCameraMovementDirection() * CameraMovementSpeed * gameTime.GetElapsedSeconds();

            if(move != Vector2.Zero)
            {
                MoveCamerea(move);
                
            }

            // Shoot
            if(_currentMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                if (_currentAmmo > 0)
                {
                    Shoot();
                    _eventManager.Publish(new ShootEvent(_currentMouseState.Position));
                }         
                else
                    Assets.SfxEmptyMagazine.Play();
            }

            // Reload
            if(_currentMouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released)
            {
                Reload();
            }

            // Prev MouseState
            _prevMouseState = _currentMouseState;
        }

        private Vector2 GetCameraMovementDirection()
        {
            var movementDirection = Vector2.Zero;

            if (_currentMouseState.Position.X >= _gameApp.GraphicsDevice.Viewport.Width -30)
            {
                movementDirection += Vector2.UnitX;
            }

            if (_currentMouseState.Position.X <=  30)
            {
                movementDirection -= Vector2.UnitX;
            }

            return movementDirection;
        }

        private void MoveCamerea(Vector2 move)
        {
            var cameraBounds = _camera.BoundingRectangle;
            float moveLeft = cameraBounds.X + move.X;
            float moveRight = cameraBounds.Right + move.X;

            float leftDif = moveLeft > _mapInfo.Left ? 1 : (float)Math.Floor(_mapInfo.Left / (moveLeft));
            float rightDif = moveRight < _mapInfo.Right ? 1 : (float)Math.Floor(_mapInfo.Right / (moveRight));

            _camera.Move(move * leftDif * rightDif);
        }

        private Bag<int> BuildAmmo()
        {
            var ammoEntities = new Bag<int>(AmmoCapacity);

            var viewport = _gameApp.GraphicsDevice.Viewport;
            var scale = _gameApp.GetViewScale();

            float yPos = GameApp.DefaultHeight - ((GameApp.DefaultHeight) * ( 0.125f ));
            float xPos = GameApp.DefaultWidth - ((GameApp.DefaultWidth) * ( 0.28f ));

            for (int i = 0; i < AmmoCapacity; i++)
            {
                float gap = i * 50;
                var entity = _entityFactory.CreateAmmo();

                var transform = entity.GetComponent<Transform2D>();
                transform.Position = new Vector2(xPos + gap, yPos);
                transform.Size = new Vector2(Prototypes.Ammo.Width, Prototypes.Ammo.Height);

                var animatedSprite = entity.GetComponent<AnimatedSprite>();
                animatedSprite.Depth = Prototypes.Ammo.Depth;

                ammoEntities[i] = entity.Id;
            }

            return ammoEntities;
        }

        private void Shoot()
        {
            // Animation
            int ammoId = _ammoEntities[AmmoCapacity - _currentAmmo];
            var sprite = _animatedSpriteMapper.Get(ammoId);
            sprite.Play("explode");

            // Sound
            Assets.SfxGunBlast.Play();

            // Shot
            _currentAmmo--;
        }

        private void Reload()
        {
            // Reload
            _currentAmmo = AmmoCapacity;

            // Animation
            foreach(int entityId in _ammoEntities)
            {
                var sprite = _animatedSpriteMapper.Get(entityId);
                sprite.Stop();
            }

            // Sound
            Assets.SfxGunReload.Play();
        }
    }
}
