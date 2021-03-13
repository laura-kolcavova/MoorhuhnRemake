using Microsoft.Xna.Framework;
using MonoECS.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src.Events
{
    public class ShootEvent : IEvent
    {
        public Point Position { get; }

        public ShootEvent(Point position)
        {
            Position = position;
        }
    }
}
