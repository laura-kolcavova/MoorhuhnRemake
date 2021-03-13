using MonoECS.Ecs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src.Components
{
    public class ChickenInfoComponent : IEntityComponent
    {
        public ChickenType ChickenType { get; set; }
    }
}
