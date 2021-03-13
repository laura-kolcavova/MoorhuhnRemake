using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Src
{
    public class MapInfo
    {
        public float Width { get; }
        public float Height { get; }
        public float X { get; }
        public float Y { get; }

        public MapInfo(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;
    }
}
