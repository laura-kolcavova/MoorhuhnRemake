using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using MonoECS.Engine;
using MonoECS.Engine.Graphics;
using MonoECS.Engine.Graphics.Sprites;
using MonoECS.Engine.Physics;
using MoorhuhnRemake.Src.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src.Systems
{
    public class RendererSystem : EntitySystem, IDrawSystem
    {
        private readonly GameApp _gameApp;
        private readonly SpriteBatch _sb;
        private readonly OrthographicCamera _camera;

        public RendererSystem(GameApp gameApp) : base(Aspect
            .All(typeof(RenderFormComponent), typeof(Transform2D)))
        {
            _gameApp = gameApp;
            _sb = new SpriteBatch(_gameApp.GraphicsDevice);
            _camera = gameApp.Services.GetService<OrthographicCamera>();
        }

        private ComponentMapper<RenderFormComponent> _renderFormMapper;
        private ComponentMapper<Transform2D> _transform2DMapper;
        private ComponentMapper<Renderer> _rendererMapper;
        private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;

        public override void Initialize(IComponentMapperService componentService)
        {
            _renderFormMapper = componentService.GetMapper<RenderFormComponent>();
            _transform2DMapper = componentService.GetMapper<Transform2D>();
            _rendererMapper = componentService.GetMapper<Renderer>();
            _animatedSpriteMapper = componentService.GetMapper<AnimatedSprite>();
        }


        public void Draw(GameTime gameTime)
        {
            // Begin
            _sb.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack,
                blendState: BlendState.AlphaBlend, transformMatrix: _camera.GetViewMatrix());

            // Draw Entities
            foreach (int entityId in ActiveEntities)
            {
                var renderForm = _renderFormMapper.Get(entityId);
                var transform = _transform2DMapper.Get(entityId);
                
                if(string.Compare("Background", renderForm.Name) == 0)
                {
                    var renderer = _rendererMapper.Get(entityId);
                    RenderBackground(renderer, transform);
                }
                else if (string.Compare("Chicken", renderForm.Name) == 0)
                {
                    var animatedSprite = _animatedSpriteMapper.Get(entityId);
                    RenderChicken(animatedSprite, transform);
                }
            
            }
            // End
            _sb.End();
        }

        private void RenderBackground(Renderer renderer, Transform2D transform)
        {
            var texture = renderer.MainTexture;

            _sb.Draw(texture, transform.Position, transform.Bounds, renderer.Color, 
                transform.Rotation, Vector2.Zero, transform.Scale, renderer.Effects, renderer.Depth);
        }


        private void RenderChicken(AnimatedSprite animatedSprite, Transform2D transform)
        {
            var region = animatedSprite.TextureRegion;

            _sb.Draw(region.Texture, transform.ScaleBounds, region.Bounds, animatedSprite.Color,
                transform.Rotation, Vector2.Zero, animatedSprite.Effects, animatedSprite.Depth);
        }

        

    }
}
