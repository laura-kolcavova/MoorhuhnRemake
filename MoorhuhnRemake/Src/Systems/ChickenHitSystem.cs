using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MoorhuhnRemake.Src.Systems
{
    public class ChickenHitSystem : EntitySystem
    {
        private readonly GameApp _gameApp;
        private readonly OrthographicCamera _camera;

        private ComponentMapper<Transform2D> _transform2DMapper;
        private ComponentMapper<ChickenComponent> _chickenInfoMapper;
        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;
        private ComponentMapper<RigidBody2D> _rigidBody2DMapper;

        private EventManager _eventManager;

        public ChickenHitSystem(GameApp gameApp) : base(
            Aspect.All(typeof(ChickenComponent)))
        {
            _gameApp = gameApp;
            _camera = _gameApp.Services.GetService<OrthographicCamera>();
        }

        public override void Initialize(IComponentMapperService componentService)
        {
            // Components
            _transform2DMapper = componentService.GetMapper<Transform2D>();
            _chickenInfoMapper = componentService.GetMapper<ChickenComponent>();
            _animatedSpriteMapper = componentService.GetMapper<AnimatedSprite>();
            _rigidBody2DMapper = componentService.GetMapper<RigidBody2D>();

            // Event Manager
            _eventManager = _gameApp.Services.GetService<EventManager>();
            _eventManager.Subscribe<ShootEvent>(this, OnShoot);

            var content = _gameApp.Content;
        }

        private IEnumerable<int> GetSortedChickens()
        {
            return ActiveEntities
                .OrderBy(id => _chickenInfoMapper.Get(id).ChickenType);
        }

        private void OnShoot(ShootEvent e)
        {
            foreach(int entityId in GetSortedChickens())
            {
                var transform = _transform2DMapper.Get(entityId);

                var position = e.Position + _camera.Position.ToPoint();

                var scale = _gameApp.GetViewScale();

                var bounds = new Rectangle();
                bounds.Size = Vector2.Multiply(transform.Size, scale).ToPoint();
                bounds.Location = _camera.WorldToScreen(transform.Position).ToPoint();

                if (bounds.Intersects(position))
                {
                    HitChicken(entityId);
                    return;
                }
            }
        }

        private void HitChicken(int entityId)
        {
            var rigidbody = _rigidBody2DMapper.Get(entityId);
            var chickenInfo = _chickenInfoMapper.Get(entityId);
            var sprite = _animatedSpriteMapper.Get(entityId);

            // Animation
            sprite.Play("dead");

            // Fall Down
            float fallSpeed = GetChickenFallSpeed(chickenInfo.ChickenType);
            rigidbody.Velocity = Transform2D.Down * fallSpeed;

            // Sound
            var hitSfx = GetChickenHitSfx(chickenInfo.ChickenType);
            hitSfx.Play();
        }

        private SoundEffect GetChickenHitSfx(ChickenType chickenType)
        {
            switch (chickenType)
            {
                case ChickenType.LARGE:  return Assets.SfxHitLargeChicken;
                case ChickenType.MEDIUM: return Assets.SfxHitMediumChicken;
                case ChickenType.SMALL:  return Assets.SfxHitSmallChicken;
            }

            return null;
        }

        private float GetChickenFallSpeed(ChickenType chickenType)
        {
            switch (chickenType)
            {
                case ChickenType.LARGE: return LargeChicken.FallSpeed;
                case ChickenType.MEDIUM: return MediumChicken.FallSpeed;
                case ChickenType.SMALL: return SmallChicken.FallSpeed;
            }

            return 0f;
        }

    }
}
