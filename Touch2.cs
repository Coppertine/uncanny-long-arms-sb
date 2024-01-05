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
    public class Touch2 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    OsbSprite scribbleR = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 681, OsbLoopType.LoopForever);
            OsbSprite scribbleG = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 531, OsbLoopType.LoopForever);
            OsbSprite scribbleB = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 206, OsbLoopType.LoopForever);
            OsbSprite scribbleOverlay = GetLayer("Scribble").CreateAnimation("sb/scribble/.png", 4, 253, OsbLoopType.LoopForever);
            
            scribbleR.Scale(136271, 0.444444f);
            scribbleG.Scale(136271, 0.444444f);
            scribbleG.FlipH(136271);
            scribbleB.Scale(136271, 0.444444f);
            scribbleOverlay.Scale(136271, 0.444444f);

            scribbleOverlay.Fade(136271, 1f);
            scribbleR.Fade(136271, 0.8f);
            scribbleG.Fade(136271, 0.8f);
            scribbleB.Fade(136271, 0.8f);

            scribbleR.Additive(136271);
            scribbleG.Additive(136271);
            scribbleB.Additive(136271);
            scribbleR.Color(136271, Color4.Red);
            scribbleG.Color(136271, Color4.Green);
            scribbleB.Color(136271, Color4.Blue);

            scribbleR.Fade(141044,0);
            scribbleG.Fade(141044,0);
            scribbleB.Fade(141044,0);
            scribbleOverlay.Fade(141044, 0);

            scribbleR.Fade(141726, 0.5f);
            scribbleG.Fade(141726, 0.5f);
            scribbleB.Fade(141726, 0.5f);

            scribbleR.Fade(146499,0);
            scribbleG.Fade(146499,0);
            scribbleB.Fade(146499,0);

            scribbleR.Fade(147180, 0.5f);
            scribbleG.Fade(147180, 0.5f);
            scribbleB.Fade(147180, 0.5f);

            scribbleR.Fade(OsbEasing.OutQuad, 152635, 153317, 1,0.5f);
            scribbleG.Fade(OsbEasing.OutQuad, 152635, 153317, 1,0.5f);
            scribbleB.Fade(OsbEasing.OutQuad, 152635, 153317, 1,0.5f);

            scribbleR.Fade(156726,0);
            scribbleG.Fade(156726,0);
            scribbleB.Fade(156726,0);

            // This should flash 1/30th of a second
            OsbSprite whiteFlash = GetLayer("Foreground").CreateSprite("sb/p.png");
            whiteFlash.ScaleVec(135589, 854, 480);
            whiteFlash.Fade(135589, 1);
            // osu is dumb, doesn't know if it should be white or red..
            whiteFlash.Color(135589, Color4.White);
            whiteFlash.Color(135589 + 33.33333, Color4.Red);
            whiteFlash.Color(135589 + 66.66666, Color4.White);
            whiteFlash.Fade(135589 + 99.99999, 0);

        }
    }
}
