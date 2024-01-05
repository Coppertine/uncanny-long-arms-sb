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
using System.ComponentModel;
using System.Linq;

namespace StorybrewScripts
{
    public class Intro : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // Storybrew should be able to create the sprite without the fades...
		    GetLayer("TOP LAYER").CreateSprite(Beatmap.BackgroundPath).Fade(0,0);

            OsbSprite lightBG = GetLayer("").CreateSprite("sb/l.png"); 
            lightBG.Scale(1271, 0.444444);
            lightBG.Fade(1271, 9624, 0, 0.5);
            lightBG.Fade(38090, 38942, 1, 0.7);
            lightBG.Fade(42862, 43544, 1, 0);


            OsbAnimation noise = GetLayer("Foreground").CreateAnimation("sb/noise/.png", 4, 33.333333333, OsbLoopType.LoopForever);
            noise.Fade(1271, 9624, 0, 0.8);
            noise.Additive(1271);
            noise.Fade(42862, 43544, 0.8, 0);
            
        }
    }
}
