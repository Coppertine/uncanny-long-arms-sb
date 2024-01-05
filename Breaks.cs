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
    public class Breaks : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		 // 70817 - 96726

        OsbSprite scribbleR = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 681, OsbLoopType.LoopForever);
		OsbSprite scribbleG = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 531, OsbLoopType.LoopForever);
		OsbSprite scribbleB = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 206, OsbLoopType.LoopForever);
		OsbSprite scribbleOverlay = GetLayer("Scribble - Overlay").CreateAnimation("sb/scribble/.png", 4, 105, OsbLoopType.LoopForever);
        
        scribbleR.Scale(70817, 0.444444f);
        scribbleG.Scale(70817, 0.444444f);
        scribbleG.FlipH(70817);
        scribbleB.Scale(70817, 0.444444f);
        scribbleOverlay.Scale(70817, 0.444444f);

        scribbleOverlay.Fade(70817, 1f);
        scribbleR.Fade(70817, 1);
        scribbleG.Fade(70817, 1);
        scribbleB.Fade(70817, 1);

        scribbleR.Additive(70817);
        scribbleG.Additive(70817);
        scribbleB.Additive(70817);
        scribbleR.Color(70817, Color4.Red);
        scribbleG.Color(70817, Color4.Green);
        scribbleB.Color(70817, Color4.Blue);

        scribbleOverlay.Fade(75589,0);
        scribbleR.Fade(75589,0);
        scribbleG.Fade(75589,0);
        scribbleB.Fade(75589,0);


        scribbleR.Fade(76271,1);
        scribbleG.Fade(76271,1);
        scribbleB.Fade(76271,1);

        scribbleR.Fade(81044,0);
        scribbleG.Fade(81044,0);
        scribbleB.Fade(81044,0);

        scribbleR.Fade(81726,1);
        scribbleG.Fade(81726,1);
        scribbleB.Fade(81726,1);

        scribbleR.Fade(86499,0);
        scribbleG.Fade(86499,0);
        scribbleB.Fade(86499,0);

        scribbleR.Fade(87180,1);
        scribbleG.Fade(87180,1);
        scribbleB.Fade(87180,1);

        scribbleR.Fade(96726,0);
        scribbleG.Fade(96726,0);
        scribbleB.Fade(96726,0);


        }
    }
}
