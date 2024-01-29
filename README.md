# Uncanny long arms - osu! storyboard
A "simple" storyboard made using Storybrew (using Storybrew3D, therefore requiring a dev build of Storybrew)

https://osu.ppy.sh/beatmapsets/2094154#osu/4390731

## Script Credits

## Script Inspirations

## Script Times
- -00:10:00 - 00:00:00 - Lyrics.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Lyrics.cs#L60-L190))
- 00:01:272 - 00:43:544 - [Intro.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Intro.cs) + Lyrics.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Lyrics.cs#L192-L358))
- 00:43:544 - 01:09:453 - [Touch1.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Touch1.cs) + Lyrics.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Lyrics.cs#L361-L507))
- 01:09:453 - 01:36:726 - [Breaks.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Breaks.cs) + Lyrics.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/8b6477e57d6ad26a1bcb3caccb3d87f2d7370358/Lyrics.cs#L508-L551))
- 01:36:726 - 02:10:817 - [Verse.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Verse.cs#L19-L105) + Lyrics.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Lyrics.cs#L554-L720))
- 02:10:817 - 02:36:726 - [Touch2.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Touch2.cs) + Lyrics.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Lyrics.cs#L722-L854))
- 02:38:090 - 02:54:453 - [ArmReduction.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/ArmReduction.cs) (TW: autistic abelism slur in variable name)
- 02:54:453 - 04:26:328 - [Afterlife.cs](https://github.com/Coppertine/uncanny-long-arms-sb/blob/master/Afterlife.cs) (Uses Storybrew3d for underwater movement) + Lyrics ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Lyrics.cs#L856-L1348))
- 04:27:009 - 04:32:634 - Inlaws.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Inlaws.cs#L40-L71)) + Lyrics ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Lyrics.cs#L1354-L1368))
- 04:32:634 - 05:10:816 - Inlaws.cs ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Inlaws.cs#L74-L225)) + Lyrics ([Lines](https://github.com/Coppertine/uncanny-long-arms-sb/blob/d2b999e4bf38df993a96ca75c3bd413398382435/Lyrics.cs#L1369-L1503))

### General Scripts
- `scriptslibrary/GaussianBlur.cs` - dynamically blur the lyrics for the Inlaws reflection. (grabed from [this repo](https://github.com/mdymel/superfastblur/blob/master/SuperfastBlur/GaussianBlur.cs))
- `scriptslibrary/Animation3d.cs` - copy of [Sprite3d](https://github.com/Damnae/storybrew/blob/master/common/Storyboarding3d/Sprite3d.cs) with support for [OsbAnimation](https://github.com/Damnae/storybrew/blob/master/common/Storyboarding/OsbAnimation.cs) objects.
- `DebugVisual.cs` - displays guides as lines and playfield center stuff (thanks to R3aCt10n)

### Non-Script Files
#### File programs
- `.blend` - Blender 4+
- `.kra` - Krita (requires at least 2GB ram allocated to view some files)
- `.psd` - Photopea (or Photoshop equivelent that can open `.psd` files)
- `.svg` - any image editor or browser
### Files
- `clouds.blend` - procedual clouds done in blender, (uses [The Sky Is On Fire HDRI](https://polyhaven.com/a/the_sky_is_on_fire) for lighting)
- `glow.psd` - `sb/light.png`
- `hand.psd` - rotoscoped hand (of my own) for hand drawings in `sb/arm`, `sb/hand` and `LONGHAND.kra/.psd`
- `handtunnel.blend` - a failed attempt of creating a tunnel for 02:05:362 - 02:10:817 -
  
https://github.com/Coppertine/uncanny-long-arms-sb/assets/37494321/fb7fdf2b-de16-4a5b-9855-e7bd39eedb93
- `horseshoe.svg/.psd` - underscores / watersockets logo, taken from [secretariat.tech](https://secretariat.tech/)
- `LONGHAND.kra/psd` - rotoscoped hand, with extended arm, made for `sb/arms`
- 

## Acknowledgements
underscores horseshoe and secretariat colour scheme belongs to underscores.
