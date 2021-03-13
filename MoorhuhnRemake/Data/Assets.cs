using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Engine.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoorhuhnRemake.Data
{
    public class Assets
    {
        private readonly ContentManager _content;

        public Assets(ContentManager content)
        {
            _content = content;
        }

        private T Load<T>(string assetName) => _content.Load<T>(assetName);

        public void LoadContent()
        {
            BgSky = Load<Texture2D>(Res.Textures.SKY);
            BgClouds1 = Load<Texture2D>(Res.Textures.CLOUDS_1);
            BgClouds2 = Load<Texture2D>(Res.Textures.CLOUDS_2);
            BgClouds3 = Load<Texture2D>(Res.Textures.CLOUDS_3);
            BgClouds4 = Load<Texture2D>(Res.Textures.CLOUDS_4);
            BgRocks1 = Load<Texture2D>(Res.Textures.ROCKS_1);
            BgRocks2 = Load<Texture2D>(Res.Textures.ROCKS_2);

            TextureAimCursor = Load<Texture2D>(Res.Textures.AIM);

            SpriteSourceChickens = Load<Texture2D>(Res.SpriteSources.CHICKENS);
            SpriteSourceAmmo = Load<Texture2D>(Res.SpriteSources.AMMO);

            SpriteSheetLargeChicken = SpriteSheetBuilder.LargeChicken();
            SpriteSheetMediumChicken = SpriteSheetBuilder.MediumChicken();
            SpriteSheetSmallChicken = SpriteSheetBuilder.SmallChicken();
            SpriteSheetAmmo = SpriteSheetBuilder.Ammo();

            SfxGunBlast = Load<SoundEffect>(Res.Sfx.GUN_BLAST);
            SfxGunReload = Load<SoundEffect>(Res.Sfx.GUN_RELOAD);
            SfxEmptyMagazine = Load<SoundEffect>(Res.Sfx.EMPTY_MAGAZINE);
            SfxHitLargeChicken = Load<SoundEffect>(Res.Sfx.HIT_LARGE_CHICKEN);
            SfxHitMediumChicken = Load<SoundEffect>(Res.Sfx.HIT_MEDIUM_CHICKEN);
            SfxHitSmallChicken = Load<SoundEffect>(Res.Sfx.HIT_SMALL_CHICKEN);
        }

        // Textures
        public static Texture2D BgSky { get; private set; }

        public static Texture2D BgClouds1 { get; private set; }

        public static Texture2D BgClouds2 { get; private set; }

        public static Texture2D BgClouds3 { get; private set; }

        public static Texture2D BgClouds4 { get; private set; }

        public static Texture2D BgRocks1 { get; private set; }

        public static Texture2D BgRocks2 { get; private set; }

        public static Texture2D TextureAimCursor { get; private set; }

        // Sprite Sources

        public static Texture2D SpriteSourceChickens { get; private set; }

        public static Texture2D SpriteSourceAmmo { get; private set; }

        // SpriteSheets

        public static SpriteSheet SpriteSheetLargeChicken { get; private set; }

        public static SpriteSheet SpriteSheetMediumChicken { get; private set; }

        public static SpriteSheet SpriteSheetSmallChicken { get; private set; }

        public static SpriteSheet SpriteSheetAmmo { get; private set; }

        // Sound Effects

        public static SoundEffect SfxGunBlast { get; private set; }

        public static SoundEffect SfxGunReload { get; private set; }

        public static SoundEffect SfxEmptyMagazine { get; private set; }

        public static SoundEffect SfxHitLargeChicken { get; private set; }

        public static SoundEffect SfxHitMediumChicken { get; private set; }

        public static SoundEffect SfxHitSmallChicken { get; private set; }

    }
}
