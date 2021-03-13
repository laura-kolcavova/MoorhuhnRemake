using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Data
{
    public static class Res
    {
        public struct Textures
        {
            public const string SKY = "Textures/sky";
            public const string CLOUDS_1 = "Textures/clouds_1";
            public const string CLOUDS_2 = "Textures/clouds_2";
            public const string CLOUDS_3 = "Textures/clouds_3";
            public const string CLOUDS_4 = "Textures/clouds_4";
            public const string ROCKS_1 = "Textures/rocks_1";
            public const string ROCKS_2 = "Textures/rocks_2";
                 
            public const string AIM = "Textures/aim";
        }

        public struct SpriteSources
        {
            public const string CHICKENS = "Sprites/chickens";
            public const string AMMO = "Sprites/ammo";
        }

        public struct Sfx
        {
            public const string GUN_BLAST = "Sfx/gunblast";
            public const string GUN_RELOAD = "Sfx/gun reload";
            public const string EMPTY_MAGAZINE = "Sfx/empty magazine";

            public const string HIT_SMALL_CHICKEN = "Sfx/chick_hit1";
            public const string HIT_MEDIUM_CHICKEN = "Sfx/chick_hit2";
            public const string HIT_LARGE_CHICKEN = "Sfx/chick_hit3";
        }
    }
}
