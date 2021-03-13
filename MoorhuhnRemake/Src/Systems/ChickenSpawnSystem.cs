using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Ecs;
using MonoECS.Ecs.Systems;
using MonoECS.Engine;
using MonoECS.Engine.Graphics.Sprites;
using MonoECS.Engine.Physics;
using MoorhuhnRemake.Src.Components;
using MoorhuhnRemake.Src.Prototypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MoorhuhnRemake.Src.Systems
{
    public class ChickenSpawnSystem : EntitySystem, IUpdateSystem
    {
        public const float ChickenSpawnDuration = 1500f;

        public const int MaxLargeChickenCnt = 5;
        public const int MaxMediumChickenCnt = 5;
        public const int MaxSmallChickenCnt = 5;

        public const int maxChickenCnt = MaxLargeChickenCnt + MaxMediumChickenCnt + MaxSmallChickenCnt;   

        private readonly GameApp _gameApp;
        private readonly EntityFactory _entityFactory;
        private readonly MapInfo _mapInfo;
        private readonly Random _r;
        private readonly ChickenSpawnBounds _largeChickenSpawning;
        private readonly ChickenSpawnBounds _mediumChickenSpawning;
        private readonly ChickenSpawnBounds _smallChickenSpawning;

        private float _chickenSpawnTimer;

        private ComponentMapper<ChickenInfoComponent> _chickenInfoMapper;

        public ChickenSpawnSystem(GameApp gameApp) : base(
            AspectBuilder.All(typeof(TagChickenComponent)))
        {
            _gameApp = gameApp;
            _entityFactory = _gameApp.Services.GetService<EntityFactory>();
            _mapInfo = _gameApp.Services.GetService<MapInfo>();

            _r = new Random();

            _largeChickenSpawning = new ChickenSpawnBounds(400, 800);
            _mediumChickenSpawning = new ChickenSpawnBounds(200, 600);
            _smallChickenSpawning = new ChickenSpawnBounds(200, 500);
        }

        public override void Initialize(IComponentMapperService componentService)
        {
            _chickenInfoMapper = componentService.GetMapper<ChickenInfoComponent>();
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            _chickenSpawnTimer += deltaTime;

            int chickenCnt = ActiveEntities.Count();

            // Spawn Large Chicken
            if(_chickenSpawnTimer >= ChickenSpawnDuration)
            {
                _chickenSpawnTimer = 0f;

                if(chickenCnt < maxChickenCnt)
                    SpawnChicken(GetRandomChickenType());
            }

        }

        private ChickenType GetRandomChickenType()
        {
            var chickenTypes = new List<ChickenType>();

            chickenTypes.AddRange(Enum.GetValues(typeof(ChickenType)) as IEnumerable<ChickenType>);

            if (GetChickenTypeCnt(ChickenType.LARGE) == MaxLargeChickenCnt)
                chickenTypes.Remove(ChickenType.LARGE);

            if (GetChickenTypeCnt(ChickenType.MEDIUM) == MaxMediumChickenCnt)
                chickenTypes.Remove(ChickenType.MEDIUM);

            if (GetChickenTypeCnt(ChickenType.SMALL) == MaxSmallChickenCnt)
                chickenTypes.Remove(ChickenType.SMALL);

            int index = 0;
            int length = chickenTypes.Count;

            if(length > 1) index = _r.Next(length);

            return chickenTypes[index];
        }

        private ChickenFlyDirection GetRandomChickenFlyDirection()
        {
            var directions = Enum.GetValues(typeof(ChickenFlyDirection));
            return (ChickenFlyDirection)directions.GetValue(_r.Next(directions.Length));
        }

        private Entity SpawnChicken(ChickenType chickenType)
        {
            var chicken = _entityFactory.CreateChicken(chickenType);

            var transform = chicken.GetComponent<Transform2D>();
            var rigidbody = chicken.GetComponent<RigidBody2D>();
            var animatedSprite = chicken.GetComponent<AnimatedSprite>();

            Vector2 size = Vector2.Zero;
            float depth = 0f;
            float speed = 0f;
            ChickenSpawnBounds spawning = null;
            float posY;
            float posX;
            SpriteEffects effects;
            Vector2 velocity;

            switch(chickenType)
            {
                case ChickenType.LARGE:
                    {
                        size = new Vector2(LargeChicken.Width, LargeChicken.Height);
                        speed = LargeChicken.Speed;
                        depth = LargeChicken.Depth;
                        spawning = _largeChickenSpawning;
                        break;
                    }
                case ChickenType.MEDIUM:
                    {
                        size = new Vector2(MediumChicken.Width, MediumChicken.Height);  
                        speed = MediumChicken.Speed;
                        depth = MediumChicken.Depth;
                        spawning = _mediumChickenSpawning;
                        break;
                    }
                case ChickenType.SMALL:
                    {
                        size = new Vector2(SmallChicken.Width, SmallChicken.Height);
                        speed = SmallChicken.Speed;
                        depth = SmallChicken.Depth;
                        spawning = _smallChickenSpawning;
                        break;
                    }
                      
            }

            transform.Size = size;         
            animatedSprite.Depth = depth;
            animatedSprite.Play("fly");

            // Random  Y Position
            posY = _r.Next(spawning.MinY, spawning.MaxY + 1);

            // Random Direction
            if (GetRandomChickenFlyDirection() == ChickenFlyDirection.LEFT)
            {
                //LEFT
                posX = _mapInfo.Right;
                effects= SpriteEffects.None;
                velocity = Transform2D.Left;
            }
            else
            {
                // RIGHT
                posX = _mapInfo.Left - transform.Bounds.Width;
                effects = SpriteEffects.FlipHorizontally;
                velocity = Transform2D.Right;
            }

            transform.Position = new Vector2(posX, posY);
            animatedSprite.Effects = effects;
            rigidbody.Velocity = velocity * speed;

            return chicken;
        }

        public int GetChickenTypeCnt(ChickenType chickenType)
        {
            return ActiveEntities
                .Where(id => _chickenInfoMapper.Get(id).ChickenType.Equals(chickenType))
                .Count();
        }

        private class ChickenSpawnBounds
        {
            public int MinY { get; set; }
            public int MaxY { get; set; }

            public ChickenSpawnBounds(int minY, int maxY)
            {
                MinY = minY;
                MaxY = maxY;
            }
        }   
    }
}
