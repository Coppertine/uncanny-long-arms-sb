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
using System.Linq;

namespace StorybrewScripts
{
    public class ArmReduction : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    

            OsbSprite horseshoeSpaz = GetLayer("").CreateSprite("sb/horseshoe.png");
            horseshoeSpaz.Scale(163544, 0.05);
            horseshoeSpaz.Fade(163544, 0.4);
            horseshoeSpaz.Fade(167976, 0);

            for (int t = 163544; t < 167976; t+=24)
            {
                horseshoeSpaz.Move(t, new Vector2(320 + Random(-10, 10), 240 + Random(-10, 10)));               
            }

            OsbSprite horseshoe = GetLayer("").CreateSprite("sb/horseshoe.png");
            horseshoe.Scale(163544, 0.05);
            horseshoe.Fade(163544, 1);
            horseshoe.Fade(OsbEasing.OutExpo, 167976, 168828, 1, 0);


            OsbAnimation noise = GetLayer("").CreateAnimation("sb/noise/.png", 4, 33.333333333, OsbLoopType.LoopForever);            
            noise.Additive(158089);
            noise.Fade(158089, 1);
            noise.Fade(174453, 0);
            

            OsbSprite strong_vig = GetLayer("").CreateSprite("sb/vig.png");
            strong_vig.Fade(158089, 0.4);
            strong_vig.Fade(174453, 0);

            // This should flash 1/30th of a second
            OsbSprite whiteFlash = GetLayer("Foreground").CreateSprite("sb/p.png");
            whiteFlash.ScaleVec(158089, 854, 480);
            whiteFlash.Fade(158089, 1);

            whiteFlash.Color(158089, Color4.Red);
            whiteFlash.Color(158089 + 66,"#4FA504");
            whiteFlash.Fade(158089 + 99, 0);

            whiteFlash = GetLayer("Foreground").CreateSprite("sb/p.png");
            whiteFlash.ScaleVec(163544, 854, 480);
            whiteFlash.Fade(163544, 1);

            whiteFlash.Color(163544, "#A7BCD6");
            whiteFlash.Color(163544 + 66, Color4.White);
            whiteFlash.Fade(163544 + 99, 0);

            whiteFlash = GetLayer("Foreground").CreateSprite("sb/p.png");
            whiteFlash.ScaleVec(168999, 854, 480);
            whiteFlash.Fade(168999, 1);

            whiteFlash.Color(168999, "#4FA504");
            whiteFlash.Color(168999 + 66, Color4.White);
            whiteFlash.Fade(168999 + 99, 0);

            
        }
    }
}
