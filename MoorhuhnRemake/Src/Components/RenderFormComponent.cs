using MonoECS.Ecs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoorhuhnRemake.Src.Components
{
    public class RenderFormComponent : IEntityComponent
    {
        public string Name { get; set; }
    }
}
