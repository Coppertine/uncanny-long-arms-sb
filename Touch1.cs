using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class Touch1 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // 43544 - 69453
		    OsbSprite scribbleR = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 1252, OsbLoopType.LoopForever);
		    OsbSprite scribbleG = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 1143, OsbLoopType.LoopForever);
		    OsbSprite scribbleB = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 982, OsbLoopType.LoopForever);
            
            OsbAnimation scribble = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, Beatmap.GetTimingPointAt(43544).BeatDuration - 200, OsbLoopType.LoopForever);

            scribble.Scale(48999, 0.444444f);
            scribble.Fade(48999, 53771, 0, 0.2);
            scribble.Fade(53771, 0);
            scribble.Fade(54453, 59226, 0.2, 0.4);
            scribble.Fade(59226, 0);
            scribble.Fade(59908, 69453, 0.4, 0.6);
            scribble.Fade(69453, 0);

            // WHAT IS HAPPENING TO ME
            scribbleR.Scale(65874, 0.5f);
            scribbleG.Scale(65874,0.5f);
            scribbleB.Scale(65874, 0.5f);
            scribbleR.Fade(65874,66385, 0, 0.4);
            scribbleG.Fade(65874,66385, 0, 0.4);
            scribbleG.FlipH(65874);
            scribbleG.FlipV(65874);
            scribbleB.Fade(65874,66385, 0, 0.4);
            scribbleR.Color(65874, Color4.Red);
            scribbleG.Color(65874, Color4.Green);
            scribbleB.Color(65874, Color4.Blue);

            scribbleR.StartLoopGroup(65874, (69453 - 65874) / 100);
                scribbleR.Move(0, new Vector2(332.25f,255f));
                scribbleR.Move(50, new Vector2(302.25f,235f));
                scribbleR.Move(100, new Vector2(332.25f,255f));
            scribbleR.EndGroup();

            scribbleG.StartLoopGroup(65874, ((69453 - 65874) / 100));
                scribbleG.Move(0, new Vector2(312.25f,232.265f));
                scribbleG.Move(50, new Vector2(325.381f,242.585f));
                scribbleG.Move(100, new Vector2(312.25f,232.265f));
            scribbleG.EndGroup();

            scribbleR.Fade(69453, 0);
            scribbleG.Fade(69453, 0);
            scribbleB.Fade(69453, 0);

            OsbAnimation noise = GetLayer("Scribble").CreateAnimation("sb/noise/.png", 4, 33.333333333, OsbLoopType.LoopForever);            
            noise.Additive(48999);
            noise.Fade(48999, 53771, 0, 0.8);
            noise.Fade(53771, 0);
            noise.Fade(54453, 1);
            noise.Fade(59226, 0);
            noise.Fade(59908, 1);
            noise.Fade(69453, 0);



        }
    }
}
