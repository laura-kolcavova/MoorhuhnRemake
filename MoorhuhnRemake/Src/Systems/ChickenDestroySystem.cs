using Microsoft.Xna.Framework;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using MonoECS.Engine.Physics;
using MoorhuhnRemake.Src.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src.Systems
{
    public class ChickenDestroySystem : EntitySystem, IUpdateSystem
    {
        private readonly GameApp _gameApp;
        private readonly MapInfo _mapInfo;

        private ComponentMapper<Transform2D> _transform2DMapper;

        public ChickenDestroySystem(GameApp gameApp) : base(
            Aspect.All(typeof(TagChickenComponent)))
        {
            _gameApp = gameApp;
            _mapInfo = gameApp.Services.GetService<MapInfo>();
        }

        public override void Initialize(IComponentMapperService componentService)
        {
            _transform2DMapper = componentService.GetMapper<Transform2D>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                var transform = _transform2DMapper.Get(entityId);

                
                if(transform.AbsolutePosition.X + transform.Size.X < _mapInfo.X || 
                    transform.AbsolutePosition.X > _mapInfo.Width ||
                    transform.AbsolutePosition.Y + transform.Size.Y < _mapInfo.Y ||
                    transform.AbsolutePosition.Y > _mapInfo.Height)
                {
                    DestroyEntity(entityId);
                }
            }
        }
    }
}
