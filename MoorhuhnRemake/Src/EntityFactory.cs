using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Ecs;
using MonoECS.Engine.Graphics;
using MonoECS.Engine.Graphics.Sprites;
using MonoECS.Engine.Physics;
using MoorhuhnRemake.Data;
using MoorhuhnRemake.Src.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src
{
    public class EntityFactory
    {
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphicsDevice;
        
        private Dictionary<ChickenType, SpriteSheet> _chickenSpriteSheets;
        private SpriteSheet _spriteSheetAmmo;

        private World _world;

        public EntityFactory(GameApp gameApp)
        {  
            _content = gameApp.Content;
            _graphicsDevice = gameApp.GraphicsDevice;
        }

        public void LoadContent(World world)
        {
            _world = world;

            _chickenSpriteSheets = new Dictionary<ChickenType, SpriteSheet>()
            {
                { ChickenType.LARGE, Assets.SpriteSheetLargeChicken },
                { ChickenType.MEDIUM, Assets.SpriteSheetMediumChicken },
                { ChickenType.SMALL, Assets.SpriteSheetSmallChicken }
            };

            _spriteSheetAmmo = Assets.SpriteSheetAmmo;
        }

        public Entity CreateBackground()
        {
            var entity = _world.CreateEntity();

            entity.AttachComponent(new Transform2D());
            entity.AttachComponent(new Renderer());
            entity.AttachComponent(new RenderFormComponent() { Name = "Background" });

            return entity;
        }

        public Entity CreateChicken(ChickenType chickenType)
        {
            var spriteSheet = _chickenSpriteSheets[chickenType];

            var entity = _world.CreateEntity();
            entity.AttachComponent(new Transform2D());
            entity.AttachComponent(new RigidBody2D());
            entity.AttachComponent(new ChickenInfoComponent() { ChickenType = chickenType });
            entity.AttachComponent(new AnimatedSprite(spriteSheet));
            entity.AttachComponent(new RenderFormComponent() {  Name = "Chicken"} );
            entity.AttachComponent(new TagChickenComponent());

            return entity;
        }
        
        public Entity CreateAmmo()
        {
            var entity = _world.CreateEntity();

            entity.AttachComponent(new Transform2D());
            entity.AttachComponent(new AnimatedSprite(_spriteSheetAmmo));
            entity.AttachComponent(new RenderFormComponent() { Name = "Ammo" });
            entity.AttachComponent(new TagAmmoComponent());
            entity.AttachComponent(new TagHUDComponent());

            return entity;
        }
    }
}
