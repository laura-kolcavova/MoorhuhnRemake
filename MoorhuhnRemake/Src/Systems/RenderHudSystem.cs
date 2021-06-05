using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using MonoECS.Engine;
using MonoECS.Engine.Graphics.Sprites;
using MonoECS.Engine.Physics;
using MonoECS.Engine.ViewportAdapters;
using MoorhuhnRemake.Src.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src.Systems
{
    public class RenderHudSystem : EntitySystem, IDrawSystem
    {
        private readonly GameApp _gameApp;
        private readonly SpriteBatch _sb;
        private readonly ScalingViewportAdapter _viewportAdapter;    

        public RenderHudSystem(GameApp gameApp) : base(Aspect
            .All(typeof(TagHUDComponent), typeof(RenderFormComponent)))
        {
            _gameApp = gameApp;
            _sb = new SpriteBatch(_gameApp.GraphicsDevice);

            _viewportAdapter = new ScalingViewportAdapter(_gameApp.GraphicsDevice, GameApp.DefaultWidth, GameApp.DefaultHeight);
        }

        private ComponentMapper<RenderFormComponent> _renderFormMapper;
        private ComponentMapper<Transform2D> _transform2DMapper;
        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;

        public override void Initialize(IComponentMapperService componentService)
        {
            _renderFormMapper = componentService.GetMapper<RenderFormComponent>();
            _transform2DMapper = componentService.GetMapper<Transform2D>();
            _animatedSpriteMapper = componentService.GetMapper<AnimatedSprite>();
        }

        public void Draw(GameTime gameTime)
        {
   
            // Begin
            _sb.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack,
                blendState: BlendState.AlphaBlend, transformMatrix: _viewportAdapter.GetScaleMatrix() );

            // Draw Entities
            foreach (int entityId in ActiveEntities)
            {
                var renderForm = _renderFormMapper.Get(entityId);
                var transform = _transform2DMapper.Get(entityId);

                if (string.Compare("Ammo", renderForm.Name) == 0)
                {
                    var animatedSprite = _animatedSpriteMapper.Get(entityId);
                    RenderAmmo(animatedSprite, transform);
                }
            }
            // End
            _sb.End();
        }

      

        private void RenderAmmo(AnimatedSprite animatedSprite, Transform2D transform)
        {
            var region = animatedSprite.TextureRegion;

            _sb.Draw(region.Texture, transform.Bounds, region.Bounds, animatedSprite.Color,
                transform.Rotation, Vector2.Zero, animatedSprite.Effects, animatedSprite.Depth);
        }
    }
}
