using Microsoft.Xna.Framework;
using MonoECS.Engine.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Data
{
    public static class SpriteSheetBuilder
    {
        public static SpriteSheet LargeChicken()
        {
            // Large Chicken Sprite Sheet
            var sheet = new SpriteSheet("large_chickens", Assets.SpriteSourceChickens, new Dictionary<string, Rectangle>
            {
                { "fly_01", new Rectangle(3, 3, 139, 151) },
                { "fly_02", new Rectangle(3, 155, 139, 151) },
                { "fly_03", new Rectangle(3, 307, 139, 151) },
                { "fly_04", new Rectangle(3, 459, 139, 151) },
                { "fly_05", new Rectangle(3, 611, 139, 151) },
                { "fly_06", new Rectangle(3, 763, 139, 151) },
                { "fly_07", new Rectangle(3, 915, 139, 151) },
                { "fly_08", new Rectangle(3, 1067, 139, 151) },
                { "fly_09", new Rectangle(3, 1219, 139, 151) },
                { "fly_10", new Rectangle(3, 1371, 139, 151) },
                { "fly_11", new Rectangle(3, 1523, 139, 151) },
                { "fly_12", new Rectangle(3, 1675, 139, 151) },
                { "fly_13", new Rectangle(3, 1827, 139, 151) },

                { "dead_01", new Rectangle(145, 3, 139, 151) },
                { "dead_02", new Rectangle(145, 155, 139, 151) },
                { "dead_03", new Rectangle(145, 307, 139, 151) },
                { "dead_04", new Rectangle(145, 459, 139, 151) },
                { "dead_05", new Rectangle(145, 611, 139, 151) },
                { "dead_06", new Rectangle(145, 763, 139, 151) },
                { "dead_07", new Rectangle(145, 915, 139, 151) },
                { "dead_08", new Rectangle(145, 1067, 139, 151) },
            });

            // Fly Animation Cycle
            var flyCycle = new SpriteSheetAnimationCycle(0, 12)
            {
                IsLooping = true,
                FrameDuration = 0.1f
            };

            // Dead Animation Cycle
            var deadCycle = new SpriteSheetAnimationCycle(13, 20)
            {
                FrameDuration = 0.1f
            };

            sheet.Cycles.Add("fly", flyCycle);
            sheet.Cycles.Add("dead", deadCycle);

            return sheet;
        }

        public static SpriteSheet MediumChicken()
        {
            // Medium Chicken Sprite Sheet
            var sheet = new SpriteSheet("medium_chickens", Assets.SpriteSourceChickens, new Dictionary<string, Rectangle>()
            {
                { "fly_01", new Rectangle(287, 3, 99, 119) },
                { "fly_02", new Rectangle(287, 123, 99, 119) },
                { "fly_03", new Rectangle(287, 243, 99, 119) },
                { "fly_04", new Rectangle(287, 363, 99, 119) },
                { "fly_05", new Rectangle(287, 483, 99, 119) },
                { "fly_06", new Rectangle(287, 603, 99, 119) },
                { "fly_07", new Rectangle(287, 723, 99, 119) },
                { "fly_08", new Rectangle(287, 843, 99, 119) },
                { "fly_09", new Rectangle(287, 963, 99, 119) },
                { "fly_10", new Rectangle(287, 1083, 99, 119) },
                { "fly_11", new Rectangle(287, 1203, 99, 119) },
                { "fly_12", new Rectangle(287, 1323, 99, 119) },
                { "fly_13", new Rectangle(287, 1443, 99, 119) },

                { "dead_01", new Rectangle(409, 3, 99, 119) },
                { "dead_02", new Rectangle(409, 123, 99, 119) },
                { "dead_03", new Rectangle(409, 243, 99, 119) },
                { "dead_04", new Rectangle(409, 363, 99, 119) },
                { "dead_05", new Rectangle(409, 483, 99, 119) },
                { "dead_06", new Rectangle(409, 603, 99, 119) },
                { "dead_07", new Rectangle(409, 723, 99, 119) },
                { "dead_08", new Rectangle(409, 842, 99, 119) },
            });

            // Fly Animation Cycle
            var flyCycle = new SpriteSheetAnimationCycle(0, 12)
            {
                IsLooping = true,
                FrameDuration = 0.1f
            };

            // Dead Animation Cycle
            var deadCycle = new SpriteSheetAnimationCycle(13, 20)
            {
                FrameDuration = 0.1f
            };

            sheet.Cycles.Add("fly", flyCycle);
            sheet.Cycles.Add("dead", deadCycle);

            return sheet;
        }

        public static SpriteSheet SmallChicken()
        { 
            // Small Chicken Sprite Sheet
            var sheet = new SpriteSheet("small_chickens", Assets.SpriteSourceChickens, new Dictionary<string, Rectangle>()
            {
                { "fly_01", new Rectangle(531, 3, 43, 31) },
                { "fly_02", new Rectangle(531, 35, 43, 31) },
                { "fly_03", new Rectangle(531, 67, 43, 31) },
                { "fly_04", new Rectangle(531, 99, 43, 31) },
                { "fly_05", new Rectangle(531, 131, 43, 31) },
                { "fly_06", new Rectangle(531, 163, 43, 31) },
                { "fly_07", new Rectangle(531, 195, 43, 31) },
                { "fly_08", new Rectangle(531, 227, 43, 31) },
                { "fly_09", new Rectangle(531, 259, 43, 31) },
                { "fly_10", new Rectangle(531, 291, 43, 31) },
                { "fly_11", new Rectangle(531, 323, 43, 31) },
                { "fly_12", new Rectangle(531, 355, 43, 31) },
                { "fly_13", new Rectangle(531, 387, 43, 31) },

                { "dead_01", new Rectangle(577, 3, 43, 31) },
                { "dead_02", new Rectangle(577, 35, 43, 31) },
                { "dead_03", new Rectangle(577, 67, 43, 31) },
                { "dead_04", new Rectangle(577, 99, 43, 31) },
                { "dead_05", new Rectangle(577, 131, 43, 31) },
                { "dead_06", new Rectangle(577, 163, 43, 31) },
                { "dead_07", new Rectangle(577, 195, 43, 31) },
                { "dead_08", new Rectangle(577, 227, 43, 31) },
            });

            // Fly Animation Cycle
            var flyCycle = new SpriteSheetAnimationCycle(0, 12)
            {
                IsLooping = true,
                FrameDuration = 0.1f
            };

            // Dead Animation Cycle
            var deadCycle = new SpriteSheetAnimationCycle(13, 20)
            {

                FrameDuration = 0.1f
            };

            sheet.Cycles.Add("fly", flyCycle);
            sheet.Cycles.Add("dead", deadCycle);

            return sheet;
        }

        public static SpriteSheet Ammo()
        {
            var sheet = new SpriteSheet("ammo", Assets.SpriteSourceAmmo, new Dictionary<string, Rectangle>()
            {
                { "ammo_01", new Rectangle(0, 0, 112, 84) },
                { "ammo_02", new Rectangle(0, 84, 112, 84) },
                { "ammo_03", new Rectangle(0, 168, 112, 84) },
                { "ammo_04", new Rectangle(0, 252, 112, 84) },
                { "ammo_05", new Rectangle(0, 336, 112, 84) },
                { "ammo_06", new Rectangle(0, 420, 112, 84) },
                { "ammo_07", new Rectangle(0, 504, 112, 84) },
                { "ammo_08", new Rectangle(0, 588, 112, 84) },
                { "ammo_09", new Rectangle(0, 672, 112, 84) },
                { "ammo_10", new Rectangle(0, 756, 112, 84) },
                { "ammo_11", new Rectangle(0, 840, 112, 84) },
                { "ammo_12", new Rectangle(0, 924, 112, 84) },
                { "ammo_13", new Rectangle(0, 1008, 112, 84) },
                { "ammo_14", new Rectangle(0, 1092, 112, 84) },
                { "ammo_15", new Rectangle(0, 1176, 112, 84) },
                { "ammo_16", new Rectangle(0, 1260, 112, 84) },
                { "ammo_17", new Rectangle(0, 1344, 112, 84) },
                { "ammo_18", new Rectangle(0, 1428, 112, 84) },
                { "ammo_19", new Rectangle(0, 1512, 112, 84) },
                { "ammo_20", new Rectangle(0, 1596, 112, 84) },
                { "ammo_21", new Rectangle(0, 1680, 112, 84) },
            });

            var explodeCycle = new SpriteSheetAnimationCycle(0, 20)
            {
                FrameDuration = 0.02f,
            };

            sheet.Cycles.Add("explode", explodeCycle);

            return sheet;
        }
    }
}
