using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Commands;
using StorybrewCommon.Storyboarding.CommandValues;
using StorybrewCommon.Storyboarding.Display;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    public class Lyrics : StoryboardObjectGenerator
    {
        public FontGenerator lyricFont, lyricintroFont, lyricitalicFont;
        public override void Generate()
        {
            lyricintroFont = LoadFont("sb/f/intro", new FontDescription()
            {
                FontPath = "fonts/Montserrat-VariableFont_wght.ttf",
                Color = Color4.White,

            });

            lyricFont = LoadFont("sb/f", new FontDescription()
            {
                FontPath = "fonts/MontserratAlternates-Regular.ttf",
                Color = Color4.White,
            });

            lyricitalicFont = LoadFont("sb/f/i", new FontDescription()
            {
                FontPath = "fonts/MontserratAlternates-Italic.ttf",
                Color = Color4.White,
            });

            GenerateRecomendations();

            GenerateIntro();

            GenerateTouch1();
            GenerateCredits();
            GenerateVerse();
            GenerateTouch2();

            GenerateAfterlife();
            GenerateInlaws();
        }

        void GenerateRecomendations()
        {
            // 30% dim, hud hidden (shift + tab), beatmap skin enabled
            // add fan project disclaimer as well, don't want fans trying to theory craft a fan made lyric video 
            // (there is an ARG element with the album.. and i don't want to fuck it over)

            // Let's go 5 seconds before start
            // Actually, let's do 10 seconds before start, at 5 seconds, show the recomended mod / settings for lazer

            double t = -10000;
            double et = -5000;

            OsbSprite section_pass = GetLayer("Disclaimer").CreateSprite("section-pass.png", OsbOrigin.Centre, new Vector2(-25, 200));
            section_pass.Fade(t - 200, t, 0, 1);
            section_pass.Scale(t - 200, 200 / 1536.0f);
            section_pass.Fade(et - 200, et, 1, 0);

            OsbSprite section_pass_check = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("if you can see the").Path, OsbOrigin.Centre, new Vector2(-25, 250));
            section_pass_check.Fade(t - 200, t, 0, 1);
            section_pass_check.Scale(t - 200, 0.1);
            section_pass_check.Fade(et - 200, et, 1, 0);

            // pass indicator above
            OsbSprite section_pass_check2 = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("pass indicator above").Path, OsbOrigin.Centre, new Vector2(-25, 260));
            section_pass_check2.Fade(t - 200, t, 0, 1);
            section_pass_check2.Scale(t - 200, 0.1);
            section_pass_check2.Fade(et - 200, et, 1, 0);

            OsbSprite section_pass_check3 = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("disable 'ignore beatmap skin'").Path, OsbOrigin.Centre, new Vector2(-25, 270));
            section_pass_check3.Fade(t - 200, t, 0, 1);
            section_pass_check3.Scale(t - 200, 0.1);
            section_pass_check3.Fade(et - 200, et, 1, 0);


            // attempted pass-sound, but osu can't play skinnable sounds??

            OsbSprite header = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("for the best playing experience:").Path, OsbOrigin.Centre, new Vector2(320, 160));
            header.Fade(t - 200, t, 0, 1);
            header.Scale(t, 0.2);
            header.Fade(et - 200, et, 1, 0);

            OsbSprite dim30 = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 0% - 30% background dim").Path, OsbOrigin.CentreLeft, new Vector2(190, 190));
            dim30.Fade(t - 200, t, 0, 1);
            dim30.Scale(t, 0.2);
            dim30.Fade(et - 200, et, 1, 0);

            OsbSprite hudhide = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- hud hidden (shift + tab)").Path, OsbOrigin.CentreLeft, new Vector2(190, 220));
            hudhide.Fade(t - 200, t, 0, 1);
            hudhide.Scale(t, 0.2);
            hudhide.Fade(et - 200, et, 1, 0);

            OsbSprite skinenable = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'ignore beatmap skin' disabled (stable)").Path, OsbOrigin.CentreLeft, new Vector2(190, 250));
            skinenable.Fade(t - 200, t, 0, 1);
            skinenable.Scale(t, 0.2);
            skinenable.Fade(et - 200, et, 1, 0);

            OsbSprite hsenable = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'ignore beatmap hitsounds' disabled (stable)").Path, OsbOrigin.CentreLeft, new Vector2(190, 280));
            hsenable.Fade(t - 200, t, 0, 1);
            hsenable.Scale(t, 0.2);
            hsenable.Fade(et - 200, et, 1, 0);

            OsbSprite display = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 1920 x 1080 (fullscreen), vsync or higher").Path, OsbOrigin.CentreLeft, new Vector2(190, 310));
            display.Fade(t - 200, t, 0, 1);
            display.Scale(t, 0.2);
            display.Fade(et - 200, et, 1, 0);

            // Intended to exploit the import of float times for lazer only sprites.
            // Problem is that stable HATES it.
            // OsbSprite depth = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- enable depth mod (with approach circles)").Path, OsbOrigin.CentreLeft, new Vector2(190, 340));
            // depth.Fade(t - 200.222222f, t+ 0.2222222f, 0, 1);
            // depth.Scale(t + 0.2222222f, 0.2);
            // depth.Fade(et - 200.222222f, et+ 0.2222222f, 1, 0);


            OsbSprite disclaimerL1 = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("this fan-made project is not affiliated with underscores").Path, OsbOrigin.Centre, new Vector2(320, 420));
            disclaimerL1.Fade(t - 200, t, 0, 0.3);
            disclaimerL1.Scale(t, 0.1);
            disclaimerL1.Fade(et - 200, et, 0.3, 0);

            OsbSprite disclaimerL2 = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("this storyboard should not be used for any theories towards the wallsockets storyline").Path, OsbOrigin.Centre, new Vector2(320, 430));
            disclaimerL2.Fade(t - 200, t, 0, 0.3);
            disclaimerL2.Scale(t, 0.1);
            disclaimerL2.Fade(et - 200, et, 0.3, 0);

            OsbSprite epilepsy = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("this storyboard includes bright and quick flashing scenes").Path, OsbOrigin.Centre, new Vector2(320, 20));
            epilepsy.Fade(t - 200, t, 0, 0.3);
            epilepsy.Scale(t, 0.1);
            epilepsy.Fade(et - 200, et, 0.3, 0);

            OsbSprite epilepsy2 = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("if you feel uneasy with the brightness, turn up the background dim").Path, OsbOrigin.Centre, new Vector2(320, 30));
            epilepsy2.Fade(t - 200, t, 0, 0.3);
            epilepsy2.Scale(t, 0.1);
            epilepsy2.Fade(et - 200, et, 0.3, 0);

            GenerateLazerSettings();
        }

        void GenerateLazerSettings()
        {   
            int t = -5000;
            int et = 0;
            OsbSprite additionalTitle = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("additional recomendations (osu! 2024+):").Path,OsbOrigin.Centre, new Vector2(320, 160));
            additionalTitle.Fade(t - 200, t, 0, 1);
            additionalTitle.Scale(t, 0.2);
            additionalTitle.Fade(et - 200, et, 1, 0);

            OsbSprite fadeRedOff = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'fade playfield to red when health is low' disabled").Path, OsbOrigin.CentreLeft,new Vector2(130,190));
            fadeRedOff.Fade(t - 200, t, 0, 1);
            fadeRedOff.Scale(t, 0.2);
            fadeRedOff.Fade(et - 200, et, 1, 0);

            OsbSprite lightenPlayfield = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'lighten playfield during breaks' enabled").Path, OsbOrigin.CentreLeft,new Vector2(130,220));
            lightenPlayfield.Fade(t - 200, t, 0, 1);
            lightenPlayfield.Scale(t, 0.2);
            lightenPlayfield.Fade(et - 200, et, 1, 0);

            OsbSprite playfieldBorderNone = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'playfield border style' to none").Path, OsbOrigin.CentreLeft,new Vector2(130,250));
            playfieldBorderNone.Fade(t - 200, t, 0, 1);
            playfieldBorderNone.Scale(t, 0.2);
            playfieldBorderNone.Fade(et - 200, et, 1, 0);

            // OsbSprite enablebeatmapstuff = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'beatmap skins / colors / hitsounds' enabled").Path, OsbOrigin.CentreLeft,new Vector2(130,280));
            // enablebeatmapstuff.Fade(t - 200, t, 0, 1);
            // enablebeatmapstuff.Scale(t, 0.2);
            // enablebeatmapstuff.Fade(et - 200, et, 1, 0);

            OsbSprite depth = GetLayer("Disclaimer").CreateSprite(lyricitalicFont.GetTexture("- 'depth' mod (default settings)").Path, OsbOrigin.CentreLeft,new Vector2(130,280));
                depth.Fade(t - 200, t, 0, 1);
                depth.Scale(t, 0.2);
                depth.Fade(et - 200, et, 1, 0);
        }

        void GenerateIntro()
        {
            //10135 - 43544
            /* 
                10135 - I had this
                crazy dream that
             
                14567 - I got my head stuck at the top of the stairs
                20703 - I swear I could've 
                22749 - put myself to sleep
                26158 - Had I not been calling 
                29226 - for help
                31783 - And in the morning 
                32976 - I saw myself in the mirror
                And my hands were in a
                different place than where
                    they started

            */
            OsbSprite p1l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("i had this").Path, OsbOrigin.CentreLeft, new Vector2(-80, 30));
            p1l1.Fade(10135, 1);
            p1l1.Scale(10135, 0.2);
            p1l1.Fade(37408, 0);

            OsbSprite p1l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("crazy dream that").Path, OsbOrigin.CentreLeft, new Vector2(-80, 50));
            p1l2.Fade(11840, 1);
            p1l2.Scale(11840, 0.2);
            p1l2.Fade(37408, 0);

            OsbSprite p2l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("i got my head stuck at").Path, OsbOrigin.CentreLeft, new Vector2(-80, 70));
            p2l1.Fade(14567, 1);
            p2l1.Scale(14567, 0.2);
            p2l1.Fade(37408, 0);

            OsbSprite p2l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("the top of the stairs").Path, OsbOrigin.CentreLeft, new Vector2(-80, 90));
            p2l2.Fade(16953, 1);
            p2l2.Scale(16953, 0.2);
            p2l2.Fade(37408, 0);

            OsbSprite p3l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("i swear i could've").Path, OsbOrigin.CentreRight, new Vector2(722, 30));
            p3l1.Fade(20703, 1);
            p3l1.Scale(20703, 0.2);
            p3l1.Fade(37408, 0);

            OsbSprite p3l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("put myself to sleep").Path, OsbOrigin.CentreRight, new Vector2(722, 50));
            p3l2.Fade(22749, 1);
            p3l2.Scale(22749, 0.2);
            p3l2.Fade(37408, 0);

            OsbSprite p4l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("had i not been calling").Path, OsbOrigin.CentreRight, new Vector2(722, 70));
            p4l1.Fade(26158, 1);
            p4l1.Scale(26158, 0.2);
            p4l1.Fade(37408, 0);

            OsbSprite p4l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("for help").Path, OsbOrigin.CentreRight, new Vector2(722, 90));
            p4l2.Fade(29226, 1);
            p4l2.Scale(29226, 0.2);
            p4l2.Fade(37408, 0);

            OsbSprite p5l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("and in the morning").Path, OsbOrigin.Centre, new Vector2(320, 386));
            p5l1.Fade(31783, 1);
            p5l1.Scale(31783, 0.2);
            p5l1.Fade(37408, 0);

            OsbSprite p5l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("i saw my self in the mirror").Path, OsbOrigin.Centre, new Vector2(320, 406));
            p5l2.Fade(32976, 1);
            p5l2.Scale(32976, 0.2);
            p5l2.Fade(37408, 0);


            OsbSprite p6l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("and my").Path);
            p6l1.Fade(37408, 1);
            p6l1.Scale(37408, 0.2);
            p6l1.Color(37408, Color4.Black);
            p6l1.Fade(37919, 0);

            // we need a duplicate that is moving about
            OsbSprite p6l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("and my hands").Path);
            p6l2.Fade(37919, 1);
            p6l2.Scale(37919, 0.2);
            p6l2.Color(37919, Color4.Black);
            p6l2.Fade(38260, 0);

            OsbSprite p6l3 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("and my hands were").Path);
            p6l3.Fade(38260, 1);
            p6l3.Scale(38260, 0.2);
            p6l3.Color(38260, Color4.Black);
            p6l3.Fade(38601, 0);

            OsbSprite p6l4 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("and my hands were in").Path);
            p6l4.Fade(38601, 1);
            p6l4.Scale(38601, 0.2);
            p6l4.Color(38601, Color4.Black);
            p6l4.Fade(38942, 0);

            OsbSprite p6l5 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("and my hands were in a").Path);
            p6l5.Fade(38942, 1);
            p6l5.Move(38942, new Vector2(320, 240));
            p6l5.ScaleVec(38942, 0.2, 0.2);
            p6l5.Color(38942, Color4.Black);
            p6l5.Move(39283, new Vector2(320, 220));
            p6l5.Fade(43544, 0);


            OsbSprite p7l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("diff").Path);
            p7l1.Fade(39283, 1);
            p7l1.Scale(39283, 0.2);
            p7l1.Color(39283, Color4.Black);
            p7l1.Fade(39624, 0);

            OsbSprite p7l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("different").Path);
            p7l2.Fade(39624, 1);
            p7l2.Scale(39624, 0.2);
            p7l2.Color(39624, Color4.Black);
            p7l2.Fade(39965, 0);

            OsbSprite p7l3 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("different place").Path);
            p7l3.Fade(39965, 1);
            p7l3.Scale(39965, 0.2);
            p7l3.Color(39965, Color4.Black);
            p7l3.Fade(40305, 0);

            OsbSprite p7l4 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("different place than").Path);
            p7l4.Fade(40305, 1);
            p7l4.Scale(40305, 0.2);
            p7l4.Color(40305, Color4.Black);
            p7l4.Fade(40646, 0);

            OsbSprite p7l5 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("different place than where").Path);
            p7l5.Fade(40646, 1);
            p7l5.Move(40646, new Vector2(320, 240));
            p7l5.ScaleVec(40646, 0.2, 0.2);
            p7l5.Color(40646, Color4.Black);
            p7l5.Fade(43544, 0);

            OsbSprite p8l1 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("they").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p8l1.Fade(40987, 1);
            p8l1.Scale(40987, 0.2);
            p8l1.Color(40987, Color4.Black);
            p8l1.Fade(41328, 0);

            OsbSprite p8l2 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("they star").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p8l2.Fade(41328, 1);
            p8l2.Scale(41328, 0.2);
            p8l2.Color(41328, Color4.Black);
            p8l2.Fade(41669, 0);

            OsbSprite p8l3 = GetLayer("Intro").CreateSprite(lyricintroFont.GetTexture("they started").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p8l3.Fade(41669, 1);
            p8l3.ScaleVec(41669, 0.2, 0.2);
            p8l3.Move(41669, new Vector2(320, 260));
            p8l3.Color(41669, Color4.Black);
            p8l3.Fade(43544, 0);

            // random moves
            for (int t = 42521; t < 43544; t += 100)
            {
                p7l5.Move(t, new Vector2(Random(320 - Random(10, 20), 320 + Random(10, 20)), Random(220 - Random(10, 20), 220 + Random(10, 20))));
                p6l5.Move(t, new Vector2(Random(320 - Random(10, 20), 320 + Random(10, 20)), Random(240 - Random(10, 20), 240 + Random(10, 20))));
                p8l3.Move(t, new Vector2(Random(320 - Random(10, 20), 320 + Random(10, 20)), Random(260 - Random(10, 20), 260 + Random(10, 20))));

                p7l5.ScaleVec(t, Random(0.1, 2), Random(0.1, 2));
                p6l5.ScaleVec(t, Random(0.1, 2), Random(0.1, 2));
                p8l3.ScaleVec(t, Random(0.1, 2), Random(0.1, 2));
            }

        }

        // 43544 - 69453
        void GenerateTouch1()
        {
            StoryboardLayer touch1 = GetLayer("Touch 1");
            // OsbSprite iCan_ref = touch2.CreateSprite("sb/f/toes/ref.png");
            // iCan_ref.Move(43544, new Vector2(320,240));
            // iCan_ref.Scale(43544,0.4444444);
            // iCan_ref.Fade(43544, 1);
            // iCan_ref.Fade(53771, 0);

            // "i can" sprites should be seperate
            OsbSprite iSprite = touch1.CreateSprite("sb/f/toes/i.png", OsbOrigin.Centre, new Vector2(247.5f, 197.5f));
            iSprite.Scale(43544, 0.4444444);
            iSprite.Fade(43544, 1);
            iSprite.Fade(53771, 0);

            OsbSprite can = touch1.CreateSprite("sb/f/toes/can.png", OsbOrigin.Centre, new Vector2(344, 202));
            can.Scale(44908, 0.4444444);
            can.Fade(44908, 1);
            can.Fade(53771, 0);

            OsbSprite touch = touch1.CreateSprite("sb/f/toes/touch.png", OsbOrigin.Centre, new Vector2(320, 236));
            touch.Scale(46271, 0.4444444);
            touch.Fade(46271, 1);
            touch.Fade(53771, 0);

            OsbSprite my = touch1.CreateSprite("sb/f/toes/my.png", OsbOrigin.Centre, new Vector2(274.5f, 285));
            my.Scale(47635, 0.4444444);
            my.Fade(47635, 1);
            my.Fade(53771, 0);

            // toes uses 6 duplicates. or we could vec scale the toes vertically
            OsbSprite toes = touch1.CreateSprite("sb/f/toes/toes.png", OsbOrigin.TopCentre, new Vector2(357.5f, 266));
            // toes.Scale(48999,0.4444444);
            toes.ScaleVec(48999, 53771, new Vector2(0.4444444f, 0.4444444f), new Vector2(0.4444444f, 1f));


            // without bending over
            // OsbSprite without_ref = touch1.CreateSprite(lyricFont.GetTexture("without").Path);
            // without_ref.Move(54453, new Vector2(320,202));
            // without_ref.Scale(54453,0.4444444);
            // without_ref.Fade(54453, 1);
            // without_ref.Fade(59226, 0);

            OsbSprite with = touch1.CreateSprite(lyricFont.GetTexture("with").Path, OsbOrigin.Centre, new Vector2(281.5f, 202));
            with.Scale(54453, 0.4444444);
            with.Fade(54453, 1);
            with.Fade(59226, 0);
            with.Fade(59908, 1);
            with.Fade(62635, 0);


            OsbSprite _out = touch1.CreateSprite(lyricFont.GetTexture("out").Path, OsbOrigin.Centre, new Vector2(373f, 202));
            _out.Scale(55817, 0.4444444);
            _out.Fade(55817, 1);
            _out.Fade(59226, 0);
            _out.Fade(59908, 1);
            _out.Fade(62635, 0);


            // OsbSprite bending = touch1.CreateSprite(lyricFont.GetTexture("bending").Path);
            // bending.Move(54453, new Vector2(320,236));
            // bending.Scale(54453,0.42);
            // bending.Fade(54453, 1);
            // bending.Fade(59226, 0);

            OsbSprite ben = touch1.CreateSprite(lyricFont.GetTexture("ben").Path, OsbOrigin.Centre, new Vector2(270.5f, 236));
            ben.Scale(57180, 0.42);
            ben.Fade(57180, 1);
            ben.Fade(59226, 0);
            ben.Fade(59908, 1);
            ben.Fade(62635, 0);


            OsbSprite ding = touch1.CreateSprite(lyricFont.GetTexture("ding").Path, OsbOrigin.Centre, new Vector2(362, 236));

            ding.Scale(58544, 0.42);
            ding.Fade(58544, 1);
            ding.Fade(59226, 0);
            ding.Fade(59908, 1);
            ding.Fade(62635, 0);

            OsbSprite over = touch1.CreateSprite(lyricFont.GetTexture("over").Path, OsbOrigin.Centre, new Vector2(320, 270));
            // over.Scale(59908,0.4444444);
            over.ScaleVec(59908, 62635, new Vector2(0.4444444f, 0.4444444f), new Vector2(0.9F, 0.4444444f));
            over.Fade(59908, 1);
            over.Fade(62635, 0);

            // what is happening to me
            // OsbSprite whatRef = touch1.CreateSprite("sb/f/toes/ref2.png", OsbOrigin.Centre);
            // whatRef.Scale(63317, 0.444444);
            // whatRef.Fade(63317,1);
            // whatRef.Fade(65362,0);

            // add duplicate sprites here that jitter around.

            OsbSprite whatis = touch1.CreateSprite("sb/f/toes/whatis.png", OsbOrigin.Centre, new Vector2(320, 198));
            whatis.Scale(63317, 0.444444);
            whatis.Fade(63317, 1);
            whatis.Fade(65362, 0);
            whatis.Fade(66044, 1);
            whatis.Fade(68089, 0);
            whatis.Fade(68771, 1);
            whatis.Fade(70817, 0);

            // split into 3 seperate sprites for performance reasons (apparently the sprite being faded out could compute a bit weridly)
            OsbSprite happening = touch1.CreateSprite(lyricFont.GetTexture("happening").Path, OsbOrigin.Centre, new Vector2(319.5f, 241));
            happening.Scale(63999, 0.5445);
            happening.Fade(63999, 1);
            happening.Fade(65362, 0);

            happening = touch1.CreateSprite(lyricFont.GetTexture("happening").Path, OsbOrigin.Centre, new Vector2(319.5f, 241));
            happening.Scale(66726, 0.5445);
            happening.Fade(66726, 1);
            happening.Fade(68089, 0);

            happening = touch1.CreateSprite(lyricFont.GetTexture("happening").Path, OsbOrigin.Centre, new Vector2(319.5f, 241));
            happening.Scale(69453, 0.5445);
            happening.Fade(69453, 1);
            happening.Fade(70817, 0);

            // split into 3 seperate sprites for performance reasons (apparently the sprite being faded out could compute a bit weridly)
            OsbSprite _to = touch1.CreateSprite("sb/f/toes/to.png", OsbOrigin.Centre, new Vector2(212, 285.5F));
            _to.Scale(64680, 0.444444);
            _to.Fade(64680, 1);
            _to.Fade(65362, 0);

            _to = touch1.CreateSprite("sb/f/toes/to.png", OsbOrigin.Centre, new Vector2(212, 285.5F));
            _to.Scale(67408, 0.444444);
            _to.Fade(67408, 1);
            _to.Fade(68089, 0);

            _to = touch1.CreateSprite("sb/f/toes/to.png", OsbOrigin.Centre, new Vector2(212, 285.5F));
            _to.Scale(70135, 0.444444);
            _to.Fade(70135, 1);
            _to.Fade(70817, 0);

            // split into 3 seperate sprites for performance reasons (apparently the sprite being faded out could compute a bit weridly)
            OsbSprite _me = touch1.CreateSprite("sb/f/toes/ME.png", OsbOrigin.Centre, new Vector2(414, 285.5f));
            _me.Scale(65021, 0.444444);
            _me.Fade(65021, 1);
            _me.Fade(65362, 0);

            _me = touch1.CreateSprite("sb/f/toes/ME.png", OsbOrigin.Centre, new Vector2(414, 285.5f));
            _me.Scale(67749, 0.444444);
            _me.Fade(67749, 1);
            _me.Fade(68089, 0);

            _me = touch1.CreateSprite("sb/f/toes/ME.png", OsbOrigin.Centre, new Vector2(414, 285.5f));
            _me.Scale(70476, 0.444444);
            _me.Fade(70476, 1);
            _me.StartLoopGroup(70817, (75589 - 70817) / 120);
            _me.Color(0, Color4.White);
            _me.Scale(0, 0.444444);
            _me.Color(30, Color4.Black);
            _me.Scale(30, 1.5);
            _me.Color(60, Color4.White);
            _me.Scale(60, 0.444444);
            _me.Color(90, Color4.Black);
            _me.Scale(90, 4);
            _me.Color(120, Color4.White);
            _me.Scale(120, 0.444444);
            _me.EndGroup();
            _me.Fade(75589, 0);

        }

        // 70817 - 96726
        void GenerateCredits()
        {
            StoryboardLayer credits = GetLayer("Credits");

            OsbSprite title = credits.CreateSprite("sb/f/title.png");
            title.Scale(76271, 0.4444444);
            title.Fade(76271, 1);
            title.Fade(81044, 0);

            OsbSprite underscores = credits.CreateSprite("sb/f/artist.png");
            underscores.Scale(81726, 0.4444444);
            underscores.Fade(81726, 1);
            underscores.Fade(86499, 0);

            OsbSprite wafer = credits.CreateSprite(lyricFont.GetTexture("wafer").Path, OsbOrigin.BottomCentre, new Vector2(320, 250));
            wafer.Fade(87180, 1);
            wafer.ScaleVec(87180, new Vector2(0.85f, 0.4444444f));
            wafer.Fade(96726, 0);

            OsbSprite coppertine = credits.CreateSprite(lyricFont.GetTexture("coppertine").Path, OsbOrigin.TopCentre, new Vector2(320, 230));
            coppertine.Fade(87180, 1);
            coppertine.Scale(87180, 0.4444444);
            coppertine.Fade(96726, 0);
        }

        // 97067
        void GenerateVerse()
        {
            // This technically overlaps with Touch2, due to the inclusion of `- Jane Remover` on the bottom left for the next 2 sections.
            StoryboardLayer verse = GetLayer("Verse");


            /*
                And I had this crazy feeling
                That nothing was out of 
                reach anymore 
                    Anymore
                And I had a terrible feeling 
                I could touch you from over here
                And I thought it would be 
                too fucked to be truе
                But I woke up and 
                I felt my hands 
                all on the carpеt
            */
            OsbSprite p1l1 = verse.CreateSprite(lyricintroFont.GetTexture("and I had this crazy feeling").Path, OsbOrigin.CentreLeft, new Vector2(-90, 20));
            p1l1.Fade(97067 - 200, 97067, 0, 1);
            p1l1.Scale(97067 - 200, 0.15);
            p1l1.Fade(107805, 108317, 1, 0);

            OsbSprite p1l2 = verse.CreateSprite(lyricintroFont.GetTexture("that nothing was out of").Path, OsbOrigin.CentreLeft, new Vector2(-90, 35));
            p1l2.Fade(101839 - 200, 101839, 0, 1);
            p1l2.Scale(101839 - 200, 0.15);
            p1l2.Fade(107805, 108317, 1, 0);

            OsbSprite p1l3 = verse.CreateSprite(lyricintroFont.GetTexture("reach anymore").Path, OsbOrigin.CentreLeft, new Vector2(-90, 50));
            p1l3.Fade(104567 - 200, 104567, 0, 1);
            p1l3.Scale(104567 - 200, 0.15);
            p1l3.Fade(107805, 108317, 1, 0);

            OsbSprite p1l4 = verse.CreateSprite(lyricintroFont.GetTexture("anymore").Path, OsbOrigin.CentreLeft, new Vector2(602, 50));
            p1l4.Fade(106612 - 200, 106612, 0, 0.8);
            p1l4.Scale(106612 - 200, 0.15);
            p1l4.Fade(107805, 108317, 0.8, 0);

            OsbSprite p2l1 = verse.CreateSprite(lyricintroFont.GetTexture("and i had a terrible feeling").Path, OsbOrigin.CentreLeft, new Vector2(-90, 20));
            p2l1.Fade(108317 - 200, 108317, 0, 1);
            p2l1.Scale(108317 - 200, 0.15);
            p2l1.Fade(118970, 119226, 1, 0);

            OsbSprite p2l2 = verse.CreateSprite(lyricintroFont.GetTexture("i could touch you from over here").Path, OsbOrigin.CentreLeft, new Vector2(-90, 35));
            p2l2.Fade(113430 - 200, 113430, 0, 1);
            p2l2.Scale(113430 - 200, 0.15);
            p2l2.Fade(118970, 119226, 1, 0);

            OsbSprite p3l1 = verse.CreateSprite(lyricintroFont.GetTexture("and i thought it would be").Path, OsbOrigin.CentreLeft, new Vector2(-90, 20));
            OsbSprite p3l2 = verse.CreateSprite(lyricintroFont.GetTexture("too fucked to be true").Path, OsbOrigin.CentreLeft, new Vector2(-90, 35));

            p3l1.Fade(119226 - 200, 119226, 0, 1);
            p3l1.Scale(119226 - 200, 0.15);

            p3l2.Fade(121101 - 200, 119226, 0, 1);
            p3l2.Scale(121101 - 200, 0.15);

            p3l1.StartLoopGroup(124169, (124680 - 124169) / 20);
            p3l1.Fade(0, 10, 1, 0);
            p3l1.Fade(10, 20, 0, 1);
            p3l1.EndGroup();

            p3l2.StartLoopGroup(124169, (124680 - 124169) / 20);
            p3l2.Fade(0, 10, 1, 0);
            p3l2.Fade(10, 20, 0, 1);
            p3l2.EndGroup();

            // but i
            OsbSprite but = verse.CreateSprite(lyricintroFont.GetTexture("but").Path, OsbOrigin.CentreRight, new Vector2(348, 225));
            OsbSprite i_sprite = verse.CreateSprite(lyricintroFont.GetTexture("i").Path, OsbOrigin.CentreLeft, new Vector2(421, 225));
            OsbSprite woke = verse.CreateSprite(lyricintroFont.GetTexture("woke").Path, OsbOrigin.TopCentre, new Vector2(320, 240));

            but.Fade(124680, 1);
            i_sprite.Fade(124851, 1);
            woke.Fade(125192, 1);
            but.Fade(125362, 0);
            i_sprite.Fade(125362, 0);
            woke.Fade(125362, 0);

            OsbSprite p4l0 = verse.CreateSprite(lyricintroFont.GetTexture("but i woke").Path);
            p4l0.Fade(125362, 1);
            p4l0.Scale(125362, 0.25);
            p4l0.Move(125362, new Vector2(320, 240));

            p4l0.Fade(125533, 0);

            OsbSprite p4l1 = verse.CreateSprite(lyricintroFont.GetTexture("but i woke up").Path);
            p4l1.Fade(125533, 1);
            p4l1.Scale(125533, 0.25);
            p4l1.Move(125533, new Vector2(320, 240));
            p4l1.Move(125874, new Vector2(320, 220));

            p4l1.Fade(130817, 0);

            OsbSprite p4l2 = verse.CreateSprite(lyricintroFont.GetTexture("and").Path);
            p4l2.Fade(125874, 1);
            p4l2.Scale(125874, 0.25);
            p4l2.Fade(126214, 0);
            OsbSprite p4l3 = verse.CreateSprite(lyricintroFont.GetTexture("and i").Path);
            p4l3.Fade(126214, 1);
            p4l3.Scale(126214, 0.25);
            p4l3.Fade(126555, 0);
            OsbSprite p4l4 = verse.CreateSprite(lyricintroFont.GetTexture("and i felt").Path);
            p4l4.Fade(126555, 1);
            p4l4.Scale(126555, 0.25);
            p4l4.Fade(126896, 0);
            OsbSprite p4l5 = verse.CreateSprite(lyricintroFont.GetTexture("and i felt my").Path);
            p4l5.Fade(126896, 1);
            p4l5.Scale(126896, 0.25);
            p4l5.Fade(130817, 0);

            OsbSprite p4l6 = verse.CreateSprite(lyricintroFont.GetTexture("hands").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p4l6.Fade(127237, 1);
            p4l6.Scale(127237, 0.25);
            p4l6.Fade(127578, 0);
            OsbSprite p4l7 = verse.CreateSprite(lyricintroFont.GetTexture("hands all").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p4l7.Fade(127578, 1);
            p4l7.Scale(127578, 0.25);
            p4l7.Fade(127919, 0);
            OsbSprite p4l8 = verse.CreateSprite(lyricintroFont.GetTexture("hands all on").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p4l8.Fade(127919, 1);
            p4l8.Scale(127919, 0.25);
            p4l8.Fade(128260, 0);
            OsbSprite p4l9 = verse.CreateSprite(lyricintroFont.GetTexture("hands all on the").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p4l9.Fade(128260, 1);
            p4l9.Scale(128260, 0.25);
            p4l9.Fade(128601, 0);
            OsbSprite p4l10 = verse.CreateSprite(lyricintroFont.GetTexture("hands all on the car").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p4l10.Fade(128601, 1);
            p4l10.Scale(128601, 0.25);
            p4l10.Fade(128942, 0);
            OsbSprite p4l11 = verse.CreateSprite(lyricintroFont.GetTexture("hands all on the carpet").Path, OsbOrigin.Centre, new Vector2(320, 260));
            p4l11.Fade(128942, 1);
            p4l11.Scale(128942, 0.25);
            p4l11.Fade(130817, 0);

            p4l1.StartLoopGroup(129624, (130817 - 129624) / 20);
            p4l1.Fade(0, 10, 0.5, 0);
            // p4l1.Scale(0, 10, 0.25, 1);
            // p4l1.Scale(10, 20, 1, 0.25);
            p4l1.Fade(10, 20, 0, 0.5);
            p4l1.EndGroup();

            p4l5.StartLoopGroup(129624, (130817 - 129624) / 20);
            p4l5.Fade(0, 10, 0.5, 0);
            // p4l5.Scale(0, 10, 0.25, 0.8);
            // p4l5.Scale(10, 20, 0.8, 0.25);
            p4l5.Fade(10, 20, 0, 0.5);
            p4l5.EndGroup();

            p4l11.StartLoopGroup(129624, (130817 - 129624) / 20);
            p4l11.Fade(0, 10, 0.5, 0);
            // p4l11.Scale(0, 10, 0.25, 1.5);
            // p4l11.Scale(10, 20, 1.5, 0.25);
            p4l11.Fade(10, 20, 0, 0.5);
            p4l11.EndGroup();

            OsbSprite janeRemover = verse.CreateSprite(lyricFont.GetTexture("- jane remover").Path, OsbOrigin.BottomLeft, new Vector2(-90, 440));
            janeRemover.Fade(97067 - 200, 97067, 0, 1);
            janeRemover.Scale(97067, 0.2);
            janeRemover.Fade(141044, 0);
            janeRemover.Fade(141726, 1);
            janeRemover.Fade(146499, 0);
            janeRemover.Fade(147180, 1);
            janeRemover.Fade(158089, 0);
        }

        void GenerateTouch2()
        {
            StoryboardLayer touch2 = GetLayer("Touch 2");
            OsbSprite toes = touch2.CreateSprite("sb/f/toes/toes.png", OsbOrigin.BottomCentre, new Vector2(357.5f, 400 + 68.5f));
            toes.ScaleVec(136271, 141044, new Vector2(0.4444444f, 0.4444444f), new Vector2(0.4444444f, 10f));

            OsbSprite iSprite = touch2.CreateSprite("sb/f/toes/i.png", OsbOrigin.Centre, new Vector2(247.5f, 50));
            iSprite.Move(OsbEasing.OutExpo, 135249, 135589, new Vector2(247.5f, 50), new Vector2(247.5f, 370));
            iSprite.Scale(130817, 0.4444444);
            iSprite.Fade(130817, 1);
            iSprite.Fade(141044, 0);

            OsbSprite can = touch2.CreateSprite("sb/f/toes/can.png", OsbOrigin.Centre, new Vector2(344, 50 + 4.5f));
            can.Move(OsbEasing.OutExpo, 135249, 135589, new Vector2(344, 50 + 4.5f), new Vector2(344, 370 + 4.5f));
            can.Scale(132180, 0.4444444);
            can.Fade(132180, 1);
            can.Fade(141044, 0);

            OsbSprite touch = touch2.CreateSprite("sb/f/toes/touch.png");
            touch.Move(133544, new Vector2(320, 50 + 38.5f));
            touch.Move(OsbEasing.OutExpo, 135249, 135589, new Vector2(320, 50 + 38.5f), new Vector2(320, 370 + 38.5f));
            touch.Scale(133544, 0.4444444);
            touch.Fade(133544, 1);
            touch.Fade(141044, 0);

            OsbSprite my = touch2.CreateSprite("sb/f/toes/my.png");
            my.Move(134908, new Vector2(274.5f, 50 + 87.5f));
            my.Move(OsbEasing.OutExpo, 135249, 135589, new Vector2(274.5f, 50 + 87.5f), new Vector2(274.5f, 370 + 87.5f));
            my.Scale(134908, 0.4444444);
            my.Fade(134908, 1);
            my.Fade(141044, 0);

            ///////////
            //////////

            OsbSprite with = touch2.CreateSprite(lyricFont.GetTexture("with").Path, OsbOrigin.Centre, new Vector2(281.5f, 202));
            with.Scale(141726, 0.4444444);
            with.Fade(141726, 1);
            with.Fade(146499, 0);
            with.Fade(147180, 1);
            with.Fade(149908, 0);


            OsbSprite _out = touch2.CreateSprite(lyricFont.GetTexture("out").Path, OsbOrigin.Centre, new Vector2(373f, 202));
            _out.Scale(143089, 0.4444444);
            _out.Fade(143089, 1);
            _out.Fade(146499, 0);
            _out.Fade(147180, 1);
            _out.Fade(149908, 0);


            OsbSprite ben = touch2.CreateSprite(lyricFont.GetTexture("ben").Path, OsbOrigin.Centre, new Vector2(270.5f, 236));
            ben.Scale(144453, 0.42);
            ben.Fade(144453, 1);
            ben.Fade(146499, 0);
            ben.Fade(147180, 1);
            ben.Fade(149908, 0);


            OsbSprite ding = touch2.CreateSprite(lyricFont.GetTexture("ding").Path, OsbOrigin.Centre, new Vector2(362, 236));
            ding.Scale(145817, 0.42);
            ding.Fade(145817, 1);
            ding.Fade(146499, 0);
            ding.Fade(147180, 1);
            ding.Fade(149908, 0);

            OsbSprite over = touch2.CreateSprite(lyricFont.GetTexture("over").Path, OsbOrigin.Centre, new Vector2(320, 270));
            // over.Scale(147180,0.4444444);
            over.StartLoopGroup(147180, 4);
            over.ScaleVec(0, 147862 - 147180, new Vector2(0.4444444f, 0.4444444f), new Vector2(0.9F, 0.4444444f));
            over.EndGroup();
            // over.Fade(147180, 1);
            // over.Fade(149908, 0);

            // WUT
            OsbSprite whatis = touch2.CreateSprite("sb/f/toes/whatis.png", OsbOrigin.Centre, new Vector2(320, 198));
            whatis.Scale(150589, 0.444444);
            whatis.Fade(150589, 1);
            whatis.Fade(152635, 0);
            whatis.Fade(153317, 1);
            whatis.Fade(155703, 0);
            whatis.Fade(156044, 1);
            whatis.Fade(158089, 0);

            // split into 3 seperate sprites for performance reasons (apparently the sprite being faded out could compute a bit weridly)
            OsbSprite happening = touch2.CreateSprite(lyricFont.GetTexture("happening").Path, OsbOrigin.Centre, new Vector2(319.5f, 241));
            happening.Scale(151271, 0.5445);
            happening.Fade(151271, 1);
            happening.Fade(152635, 0);

            happening = touch2.CreateSprite(lyricFont.GetTexture("happening").Path, OsbOrigin.Centre, new Vector2(319.5f, 241));
            happening.Scale(153999, 0.5445);
            happening.Fade(153999, 1);
            happening.Fade(155703, 0);

            happening = touch2.CreateSprite(lyricFont.GetTexture("happening").Path, OsbOrigin.Centre, new Vector2(319.5f, 241));
            happening.Scale(156726, 0.5445);
            happening.Fade(156726, 1);
            happening.Fade(158089, 0);

            // split into 3 seperate sprites for performance reasons (apparently the sprite being faded out could compute a bit weridly)
            OsbSprite _to = touch2.CreateSprite("sb/f/toes/to.png", OsbOrigin.Centre, new Vector2(212, 285.5F));
            _to.Scale(151953, 0.444444);
            _to.Fade(151953, 1);
            _to.Fade(152635, 0);

            _to = touch2.CreateSprite("sb/f/toes/to.png", OsbOrigin.Centre, new Vector2(212, 285.5F));
            _to.Scale(154680, 0.444444);
            _to.Fade(154680, 1);
            _to.Fade(155703, 0);

            _to = touch2.CreateSprite("sb/f/toes/to.png", OsbOrigin.Centre, new Vector2(212, 285.5F));
            _to.Scale(157408, 0.444444);
            _to.Fade(157408, 1);
            _to.Fade(158089, 0);

            // split into 3 seperate sprites for performance reasons (apparently the sprite being faded out could compute a bit weridly)
            OsbSprite _me = touch2.CreateSprite("sb/f/toes/ME.png", OsbOrigin.Centre, new Vector2(414, 285.5f));
            _me.Scale(152294, 0.444444);
            _me.Fade(152294, 1);
            _me.Fade(152635, 0);

            _me = touch2.CreateSprite("sb/f/toes/ME.png", OsbOrigin.Centre, new Vector2(414, 285.5f));
            _me.Scale(155021, 0.444444);
            _me.Fade(155021, 1);
            _me.Fade(155703, 0);

            _me = touch2.CreateSprite("sb/f/toes/ME.png", OsbOrigin.Centre, new Vector2(414, 285.5f));
            _me.Scale(157749, 0.444444);
            _me.Fade(157749, 1);
            _me.Fade(158089, 0);

        }

        void GenerateAfterlife()
        {
            /* 
                and in just a couple seconds
                 of breathing in and out
                i was exiled from the heavens
                 and flung toward the ground
                i once had peace and quiet
                Now i can't turn the 
                damn 
                thing 
                off
                But i don't wanna *die* yet,
                 i haven't finished what i'm working on
            */
            StoryboardLayer layer = GetLayer("Afterlife");

            /////////
            // and in just a couple seconds // 
            ////////

            OsbSprite _and = layer.CreateSprite(lyricitalicFont.GetTexture("and").Path);
            OsbSprite _in = layer.CreateSprite(lyricitalicFont.GetTexture("in").Path);
            OsbSprite _just = layer.CreateSprite(lyricitalicFont.GetTexture("just").Path);
            OsbSprite _a = layer.CreateSprite(lyricitalicFont.GetTexture("a").Path);
            OsbSprite _couple = layer.CreateSprite(lyricitalicFont.GetTexture("couple").Path);
            OsbSprite _seconds = layer.CreateSprite(lyricitalicFont.GetTexture("seconds").Path);

            _and.Scale(221839, 0.25);
            _and.Fade(221839, 222180, 0, 1);
            _and.Fade(226612, 226782, 1, 0);
            _and.Move(221839, 226782, new Vector2(60, 287), new Vector2(47, 260));
            _and.Rotate(221839, 226782, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-2.5));
            _and.Color(221839, Color4.SlateGray);
            _and.Additive(_and.CommandsStartTime);


            _in.Scale(221839, 0.25);
            _in.Fade(221839, 222180, 0, 1);
            _in.Fade(226612, 226782, 1, 0);
            _in.Move(221839, 226782, new Vector2(121, 277), new Vector2(125, 255));
            _in.Rotate(221839, 226782, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(5));
            _in.Color(221839, Color4.SlateGray);
            _in.Additive(_in.CommandsStartTime);



            _just.Scale(221986, 0.25);
            _just.Fade(221986, 222521, 0, 1);
            _just.Fade(226612, 226782, 1, 0);
            _just.Move(221986, 226782, new Vector2(165, 292), new Vector2(172, 266));
            _just.Rotate(221986, 226782, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(8));
            _just.Color(221986, Color4.SlateGray);
            _just.Additive(_just.CommandsStartTime);



            _a.Scale(222306, 0.25);
            _a.Fade(222306, 222521, 0, 1);
            _a.Fade(226612, 226782, 1, 0);
            _a.Move(222306, 226782, new Vector2(222.5f, 282), new Vector2(210.25f, 254));
            _a.Rotate(222306, 226782, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-5.55f));
            _a.Color(222306, Color4.SlateGray);
            _a.Additive(_a.CommandsStartTime);


            _couple.Scale(222382, 0.25);
            _couple.Fade(222382, 222691, 0, 1);
            _couple.Fade(226612, 226782, 1, 0);
            _couple.Move(222382, 226782, new Vector2(283.529f, 276), new Vector2(296.2565f, 264));
            _couple.Rotate(222382, 226782, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(7.252f));
            _couple.Color(222382, Color4.SlateGray);
            _couple.Additive(_couple.CommandsStartTime);


            _seconds.Scale(223373, 0.25);
            _seconds.Fade(223373, 223544, 0, 1);
            _seconds.Fade(226612, 226782, 1, 0);
            _seconds.Move(223373, 226782, new Vector2(382.33345f, 296), new Vector2(395.2565f, 284));
            _seconds.Rotate(223373, 226782, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-5.55f));
            _seconds.Color(223373, Color4.DarkSlateBlue);
            _seconds.Additive(_seconds.CommandsStartTime);

            /////////
            // of breathing in and out // 
            ////////

            OsbSprite _of = layer.CreateSprite(lyricitalicFont.GetTexture("of").Path);
            OsbSprite _breathing = layer.CreateSprite(lyricitalicFont.GetTexture("breathing").Path);
            _in = layer.CreateSprite(lyricitalicFont.GetTexture("in").Path);
            _and = layer.CreateSprite(lyricitalicFont.GetTexture("and").Path);
            OsbSprite _out = layer.CreateSprite(lyricitalicFont.GetTexture("out").Path);

            _of.Scale(227123, 0.25);
            _of.Fade(227123, 227294, 0, 1);
            _of.Fade(232141, 232578, 1, 0);
            _of.Move(227123, 232578, new Vector2(Random(128.02f, 133.02f), Random(292.2f, 310.2f)), new Vector2(Random(128.02f, 133.02f), Random(280.2f, 300.2f)));
            _of.Rotate(227123, 232578, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)));
            _of.Color(222306, Color4.SlateGray);
            _of.Additive(_of.CommandsStartTime);


            _breathing.Scale(227549, 0.25);
            _breathing.Additive(_breathing.CommandsStartTime);

            randMovementLyrics(_breathing, 227634, 232578,
                new Vector2(Random(200.02f, 220.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(200.02f, 220.02f), Random(280.2f, 300.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _in.Scale(228231, 0.25);
            _in.Additive(_in.CommandsStartTime);
            randMovementLyrics(_in, 228231, 232578,
                new Vector2(Random(305.02f, 315.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(305.02f, 315.02f), Random(280.2f, 300.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _and.Scale(228572, 0.25);
            _and.Additive(_and.CommandsStartTime);
            randMovementLyrics(_and, 228572, 232578,
                new Vector2(Random(350.02f, 365.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(350.02f, 365.02f), Random(280.2f, 300.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _out.Scale(229274, 0.25);
            _out.Additive(_out.CommandsStartTime);
            randMovementLyrics(_out, 229274, 232578,
                new Vector2(Random(420.02f, 450.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(420.02f, 450.02f), Random(280.2f, 300.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            /////////
            // i was exiled from the heavens // 
            ////////

            OsbSprite _i = layer.CreateSprite(lyricitalicFont.GetTexture("i").Path);
            OsbSprite _was = layer.CreateSprite(lyricitalicFont.GetTexture("was").Path);
            OsbSprite _exiled = layer.CreateSprite(lyricitalicFont.GetTexture("exiled").Path);
            OsbSprite _from = layer.CreateSprite(lyricitalicFont.GetTexture("from").Path);
            OsbSprite _the = layer.CreateSprite(lyricitalicFont.GetTexture("the").Path);

            var heavensMovement = new AnimatedValue<CommandPosition>();
            var heavensRotation = new AnimatedValue<CommandDecimal>();
            OsbSprite _hea = layer.CreateSprite(lyricitalicFont.GetTexture("hea").Path, OsbOrigin.CentreRight);
            OsbSprite _vens = layer.CreateSprite(lyricitalicFont.GetTexture("vens").Path, OsbOrigin.CentreLeft);
            heavensMovement.Add(new MoveCommand(OsbEasing.None, 234817, 237757,
                new Vector2(Random(435.02f, 458.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(435.02f, 458.02f), Random(280.2f, 320.2f))));
            heavensRotation.Add(new RotateCommand(OsbEasing.None, 234817, 237757, 0, MathHelper.DegreesToRadians(Random(-10.02f, 10.02f))));

            _i.Scale(232697, 0.25);
            _i.Additive(_i.CommandsStartTime);
            randMovementLyrics(_i, 232697, 237757,
                new Vector2(Random(100.02f, 120.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(100.02f, 120.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _was.Scale(232748, 0.25);
            _was.Additive(232748);
            randMovementLyrics(_was, 232748, 237757,
                new Vector2(Random(142.02f, 160.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(142.02f, 160.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _exiled.Scale(232994, 0.25);
            _exiled.Additive(232994);
            randMovementLyrics(_exiled, 232994, 237757,
                new Vector2(Random(220.02f, 232.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(220.02f, 232.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            _from.Scale(233740, 0.25);
            _from.Additive(233740);
            randMovementLyrics(_from, 233740, 237757,
                new Vector2(Random(293.02f, 302.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(293.02f, 302.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _the.Scale(234235, 0.25);
            _the.Additive(234235);
            randMovementLyrics(_the, 234235, 237757,
                new Vector2(Random(340.02f, 365.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(340.02f, 365.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);


            _hea.Scale(234817, 0.25);
            _hea.Additive(234817);
            randMovementLyrics(_hea, 234817, 237757,
                heavensMovement.StartValue, heavensMovement.EndValue,
                heavensRotation.EndValue, Color4.DarkSlateBlue);

            _vens.Scale(236787, 0.25);
            _vens.Additive(236787);
            randMovementLyrics(_vens, 236787, 237757,
                heavensMovement.ValueAtTime(236787), heavensMovement.EndValue,
                heavensRotation.EndValue, Color4.DarkSlateBlue);

            /////////
            // and flung toward the ground // 
            ////////
            _and = layer.CreateSprite(lyricitalicFont.GetTexture("and").Path);
            OsbSprite _flung = layer.CreateSprite(lyricitalicFont.GetTexture("flung").Path);
            OsbSprite _toward = layer.CreateSprite(lyricitalicFont.GetTexture("toward").Path);
            _the = layer.CreateSprite(lyricitalicFont.GetTexture("the").Path);
            OsbSprite _ground = layer.CreateSprite(lyricitalicFont.GetTexture("ground").Path);

            _and.Scale(238078, 0.25);
            _and.Additive(_and.CommandsStartTime);
            randMovementLyrics(_and, 238078, 243604,
                new Vector2(Random(100.02f, 120.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(100.02f, 120.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _flung.Scale(238329, 0.25);
            _flung.Additive(_flung.CommandsStartTime);
            randMovementLyrics(_flung, 238329, 243604,
                new Vector2(Random(183.02f, 198.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(183.02f, 198.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            _toward.Scale(238933, 0.25);
            _toward.Additive(_toward.CommandsStartTime);
            randMovementLyrics(_toward, 238933, 243604,
                new Vector2(Random(270.02f, 283.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(270.02f, 283.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _the.Scale(239429, 0.25);
            _the.Additive(_the.CommandsStartTime);
            randMovementLyrics(_the, 239429, 243604,
                new Vector2(Random(350.02f, 365.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(350.02f, 365.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _ground.Scale(240140, 0.25);
            _ground.Additive(_ground.CommandsStartTime);
            randMovementLyrics(_ground, 240140, 243604,
                new Vector2(Random(400.02f, 485.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(400.02f, 485.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            ////////
            // i once had peace and quiet //
            ///////

            _i = layer.CreateSprite(lyricitalicFont.GetTexture("i").Path);
            OsbSprite _once = layer.CreateSprite(lyricitalicFont.GetTexture("once").Path);
            OsbSprite _had = layer.CreateSprite(lyricitalicFont.GetTexture("had").Path);
            OsbSprite _peace = layer.CreateSprite(lyricitalicFont.GetTexture("peace").Path);
            _and = layer.CreateSprite(lyricitalicFont.GetTexture("and").Path);
            OsbSprite _quiet = layer.CreateSprite(lyricitalicFont.GetTexture("quiet").Path);

            _i.Scale(243727, 0.25);
            _i.Additive(_i.CommandsStartTime);
            randMovementLyrics(_i, 243727, 248975,
                new Vector2(Random(100.02f, 120.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(100.02f, 120.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _once.Scale(243912, 0.25);
            _once.Additive(_once.CommandsStartTime);
            randMovementLyrics(_once, 243912, 248975,
                new Vector2(Random(152.02f, 162.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(152.02f, 162.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _had.Scale(244259, 0.25);
            _had.Additive(_had.CommandsStartTime);
            randMovementLyrics(_had, 244259, 248975,
                new Vector2(Random(215.02f, 228.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(215.02f, 228.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _peace.Scale(244572, 0.25);
            _peace.Additive(_peace.CommandsStartTime);
            randMovementLyrics(_peace, 244572, 248975,
                new Vector2(Random(292.02f, 320.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(292.02f, 320.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _and.Scale(245081, 0.25);
            _and.Additive(_and.CommandsStartTime);
            randMovementLyrics(_and, 245081, 248975,
                new Vector2(Random(372.02f, 388.02f), Random(280.2f, 320.2f)),
                new Vector2(Random(372.02f, 388.02f), Random(292.2f, 310.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _quiet.Scale(245602, 0.25);
            _quiet.Additive(_quiet.CommandsStartTime);
            randMovementLyrics(_quiet, 245602, 248975,
                new Vector2(Random(430.02f, 480.02f), Random(280.2f, 320.2f)),
                new Vector2(Random(430.02f, 480.02f), Random(292.2f, 310.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            ////////
            // now i can't turn the damn thing off //
            ///////

            OsbSprite _now = layer.CreateSprite(lyricitalicFont.GetTexture("now").Path);
            _i = layer.CreateSprite(lyricitalicFont.GetTexture("i").Path);
            OsbSprite _cant = layer.CreateSprite(lyricitalicFont.GetTexture("can't").Path);
            OsbSprite _turn = layer.CreateSprite(lyricitalicFont.GetTexture("turn").Path);
            _the = layer.CreateSprite(lyricitalicFont.GetTexture("the").Path);
            OsbSprite _damn = layer.CreateSprite(lyricitalicFont.GetTexture("damn").Path);
            OsbSprite _thing = layer.CreateSprite(lyricitalicFont.GetTexture("thing").Path);
            OsbSprite _off = layer.CreateSprite(lyricitalicFont.GetTexture("off").Path);

            _now.ScaleVec(249031, 254355, 0.25f, 0.25f, 0.28f, 0.23f);
            _now.Additive(_now.CommandsStartTime);
            randMovementLyrics(_now, 249031, 254355,
                new Vector2(Random(60.02f, 98.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(60.02f, 98.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _i.ScaleVec(249398, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _i.Additive(_i.CommandsStartTime);
            randMovementLyrics(_i, 249398, 254355,
                new Vector2(Random(123.02f, 132.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(123.02f, 132.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _cant.ScaleVec(249684, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _cant.Additive(_cant.CommandsStartTime);
            randMovementLyrics(_cant, 249684, 254355,
                new Vector2(Random(155.0221f, 173.021f), Random(292.2f, 310.2f)),
                new Vector2(Random(155.0221f, 173.021f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _turn.ScaleVec(250041, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _turn.Additive(_turn.CommandsStartTime);
            randMovementLyrics(_turn, 250041, 254355,
                new Vector2(Random(233.0221f, 258.021f), Random(292.2f, 310.2f)),
                new Vector2(Random(233.0221f, 258.021f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _the.ScaleVec(250409, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _the.Additive(_the.CommandsStartTime);
            randMovementLyrics(_the, 250409, 254355,
                new Vector2(Random(306.0221f, 315.021f), Random(292.2f, 310.2f)),
                new Vector2(Random(306.0221f, 315.021f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _damn.ScaleVec(250726, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _damn.Additive(_damn.CommandsStartTime);
            randMovementLyrics(_damn, 250726, 254355,
                new Vector2(Random(378.0221f, 408.021f), Random(292.2f, 310.2f)),
                new Vector2(Random(378.0221f, 408.021f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.Red);

            _thing.ScaleVec(252107, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _thing.Additive(_thing.CommandsStartTime);
            randMovementLyrics(_thing, 252107, 254355,
                new Vector2(Random(462.0221f, 488.021f), Random(292.2f, 310.2f)),
                new Vector2(Random(462.0221f, 488.021f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.Red);

            // accidental overlap.. keep it.. if bn's say no.. say no
            _off.ScaleVec(253120, 254355, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _off.Additive(_off.CommandsStartTime);
            randMovementLyrics(_off, 253120, 254355,
                new Vector2(Random(503.0221f, 515.021f), Random(292.2f, 310.2f)),
                new Vector2(Random(503.0221f, 515.021f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.Red);

            ////////
            // but i don't wanna die yet //
            ///////
            OsbSprite _but = layer.CreateSprite(lyricitalicFont.GetTexture("but").Path);
            _i = layer.CreateSprite(lyricitalicFont.GetTexture("i").Path);
            OsbSprite _dont = layer.CreateSprite(lyricitalicFont.GetTexture("don't").Path);
            OsbSprite _wanna = layer.CreateSprite(lyricitalicFont.GetTexture("wanna").Path);
            OsbSprite _die = layer.CreateSprite(lyricitalicFont.GetTexture("die").Path);
            OsbSprite _yet = layer.CreateSprite(lyricitalicFont.GetTexture("yet").Path);

            _but.ScaleVec(254470, 259751, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _but.Additive(_but.CommandsStartTime);
            randMovementLyrics(_but, 254470, 259751,
                new Vector2(Random(100.02f, 120.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(100.02f, 120.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _i.ScaleVec(254837, 259751, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _i.Additive(_i.CommandsStartTime);
            randMovementLyrics(_i, 254837, 259751,
                new Vector2(Random(158.02f, 169.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(158.02f, 169.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _dont.ScaleVec(255146, 259751, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _dont.Additive(_dont.CommandsStartTime);
            randMovementLyrics(_dont, 255146, 259751,
                new Vector2(Random(185.02f, 215.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(185.02f, 215.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _wanna.ScaleVec(255300, 259751, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _wanna.Additive(_wanna.CommandsStartTime);
            randMovementLyrics(_wanna, 255300, 259751,
                new Vector2(Random(290.02f, 315.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(290.02f, 315.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            for (int t = 256179; t <= 259751; t += 25)
            {
                _die.ScaleVec(t, new Vector2(Random(0.245f, 0.26f), Random(0.235f, 0.25f)));
            }

            _die.Additive(_die.CommandsStartTime);
            randMovementLyrics(_die, 256179, 259751,
                new Vector2(Random(290.02f, 315.02f), Random(240.2f, 260.2f)),
                new Vector2(Random(290.02f, 315.02f), Random(240.2f, 260.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.Red);

            _yet.ScaleVec(258611, 259751, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _yet.Additive(_yet.CommandsStartTime);
            randMovementLyrics(_yet, 258611, 259751,
                new Vector2(Random(390.02f, 415.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(390.02f, 415.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            //259695
            ////////
            // i haven't finished what i'm working on //
            ///////
            _i = layer.CreateSprite(lyricitalicFont.GetTexture("i").Path);
            OsbSprite _havent = layer.CreateSprite(lyricitalicFont.GetTexture("haven't").Path);
            OsbSprite _finished = layer.CreateSprite(lyricitalicFont.GetTexture("finished").Path);
            OsbSprite _what = layer.CreateSprite(lyricitalicFont.GetTexture("what").Path);
            OsbSprite _im = layer.CreateSprite(lyricitalicFont.GetTexture("i'm").Path);
            OsbSprite _working = layer.CreateSprite(lyricitalicFont.GetTexture("working").Path);
            OsbSprite _on = layer.CreateSprite(lyricitalicFont.GetTexture("on").Path);

            _i.ScaleVec(259695, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _i.Additive(_i.CommandsStartTime);
            randMovementLyrics(_i, 259695, 264632,
                new Vector2(Random(20.02f, 45.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(20.02f, 45.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _havent.ScaleVec(259885, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _havent.Additive(_havent.CommandsStartTime);
            randMovementLyrics(_havent, 259885, 264632,
                new Vector2(Random(90.02f, 110.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(90.02f, 110.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _finished.ScaleVec(260146, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _finished.Additive(_finished.CommandsStartTime);
            randMovementLyrics(_finished, 260146, 264632,
                new Vector2(Random(200.02f, 225.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(200.02f, 225.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            _what.ScaleVec(260944, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _what.Additive(_what.CommandsStartTime);
            randMovementLyrics(_what, 260944, 264632,
                new Vector2(Random(300.02f, 325.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(300.02f, 325.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _im.ScaleVec(261295, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _im.Additive(_im.CommandsStartTime);
            randMovementLyrics(_im, 261295, 264632,
                new Vector2(Random(360.02f, 385.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(360.02f, 385.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);

            _working.ScaleVec(261688, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _working.Additive(_working.CommandsStartTime);
            randMovementLyrics(_working, 261688, 264632,
                new Vector2(Random(430.02f, 475.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(430.02f, 475.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.DarkSlateBlue);

            _on.ScaleVec(264000, 264632, 0.25f, 0.25f, Random(0.24000f, 0.3f), Random(0.21000f, 0.25f));
            _on.Additive(_on.CommandsStartTime);
            randMovementLyrics(_on, 264000, 264632,
                new Vector2(Random(530.02f, 555.02f), Random(292.2f, 310.2f)),
                new Vector2(Random(530.02f, 555.02f), Random(280.2f, 320.2f)),
                MathHelper.DegreesToRadians(Random(-10.02f, 10.02f)), Color4.SlateGray);
        }

        private void randMovementLyrics(OsbSprite sprite, double startTime, double endTime, CommandPosition startPos, CommandPosition endPos, float rotation, Color4 color)
        {
            double sTime = startTime - Random(100, 200);
            double eTime = endTime - Random(100, 200);
            sprite.Fade(sTime, startTime, 0, 1);
            sprite.Fade(eTime, endTime, 1, 0);
            sprite.Move(sTime, endTime, startPos, endPos);
            sprite.Rotate(sTime, endTime, MathHelper.DegreesToRadians(0), rotation);
            sprite.Color(sTime, color);
        }
  
        void GenerateInlaws()
        {
            

            // Putting a pin on the line "Several hidden shots to the chest ..." i have NO idea how i want to display it..
            OsbSprite hiddenshots = GetLayer("Pre-Inlaws").CreateSprite(lyricFont.GetTexture("several hidden shots to the chest").Path);
            hiddenshots.Fade(266942, 267180, 0, 1);
            hiddenshots.Color(266942, Color4.Gray);
            hiddenshots.Scale(266942, 0.2);
            hiddenshots.Fade(270419,270589,1, 0);
            hiddenshots.Additive(266942); 

             OsbSprite how = GetLayer("Pre-Inlaws").CreateSprite(lyricFont.GetTexture("how did you get everyone").Path);
            how.Fade(270454, 270589, 0, 1);
            how.Color(270454, Color4.Gray);
            how.Scale(270454, 0.2);
            how.Fade(272634, 0);
            how.Additive(270454);

            StoryboardLayer behindWater = GetLayer("Inlaws - Behind Water");
            StoryboardLayer aboveWater = GetLayer("Inlaws - Above Water");

            GaussianBlur l1Blur = new GaussianBlur(GetMapsetBitmap(lyricFont.GetTexture("how did you get everyone").Path, false));
            GaussianBlur l2Blur = new GaussianBlur(GetMapsetBitmap(lyricFont.GetTexture("to think that you were depressed").Path, false));
            GaussianBlur l3Blur = new GaussianBlur(GetMapsetBitmap(lyricFont.GetTexture("tell me if it's not on purpose").Path, false));
            GaussianBlur l4Blur = new GaussianBlur(GetMapsetBitmap(lyricFont.GetTexture("why'd you do it again").Path, false));
            GaussianBlur l5Blur = new GaussianBlur(GetMapsetBitmap(lyricFont.GetTexture("just admit it").Path, false));
            GaussianBlur l6Blur = new GaussianBlur(GetMapsetBitmap(lyricFont.GetTexture("you don't have a world you're up against").Path, false));

            int radius = 5;
            var l1BlurImage = l1Blur.Process(radius);
            var l2BlurImage = l2Blur.Process(radius);
            var l3BlurImage = l3Blur.Process(radius);
            var l4BlurImage = l4Blur.Process(radius);
            var l5BlurImage = l5Blur.Process(radius);
            var l6BlurImage = l6Blur.Process(radius);

            l1BlurImage.Save(Path.Combine(MapsetPath, "sb/f/b/1.png"), ImageFormat.Png);
            l2BlurImage.Save(Path.Combine(MapsetPath, "sb/f/b/2.png"), ImageFormat.Png);
            l3BlurImage.Save(Path.Combine(MapsetPath, "sb/f/b/3.png"), ImageFormat.Png);
            l4BlurImage.Save(Path.Combine(MapsetPath, "sb/f/b/4.png"), ImageFormat.Png);
            l5BlurImage.Save(Path.Combine(MapsetPath, "sb/f/b/5.png"), ImageFormat.Png);
            l6BlurImage.Save(Path.Combine(MapsetPath, "sb/f/b/6.png"), ImageFormat.Png);
            
            OsbSprite l1Mirror = behindWater.CreateSprite("sb/f/b/1.png", OsbOrigin.TopCentre, new Vector2(63, 300));
            OsbSprite l2Mirror = behindWater.CreateSprite("sb/f/b/2.png", OsbOrigin.TopCentre, new Vector2(293, 361));
            OsbSprite l3Mirror = behindWater.CreateSprite("sb/f/b/3.png", OsbOrigin.BottomCentre, new Vector2(-10, 324));
            OsbSprite l4Mirror = behindWater.CreateSprite("sb/f/b/4.png", OsbOrigin.BottomCentre, new Vector2(453, 340));
            OsbSprite l5Mirror = behindWater.CreateSprite("sb/f/b/5.png", OsbOrigin.BottomCentre, new Vector2(-12, 311));
            OsbSprite l6Mirror = behindWater.CreateSprite("sb/f/b/6.png", OsbOrigin.BottomCentre, new Vector2(386, 318));


            //////
            /// How did you get everyone to think that you were depressed?
            /////
            OsbSprite l1 = aboveWater.CreateSprite(lyricFont.GetTexture("how did you get everyone").Path, OsbOrigin.BottomCentre, new Vector2(63, 115));

            l1.Fade(272634, 1);
            l1.Move(272634,275988,new Vector2(63, 115),new Vector2(90, 115)); 
            l1.Fade(275701,275988,1,0);
            l1.Scale(272634, 0.18f);
            l1.Fade(288998, 0);
            l1.Color(272634, Color4.DarkSlateBlue);

            //l1Mirror.Rotate(272634, Math.PI);
            l1Mirror.FlipV(272634);
            l1Mirror.Fade(272634, 0.5);
            l1Mirror.Fade(275701,275988, 0.5, 0);
            l1Mirror.ScaleVec(272634, 0.18f, 0.1f);
            l1Mirror.Move(272634,275988,new Vector2(63, 300),new Vector2(85, 300));

            // l1Mirror.ScaleVec(275701,275988, 0.18f, 0.1f,  0.18f, 0.3f);
            l1Mirror.Color(272634, Color4.DarkSlateBlue);


            l1Mirror.Fade(288998, 0);

            OsbSprite l2 = aboveWater.CreateSprite(lyricFont.GetTexture("to think that you were depressed").Path, OsbOrigin.BottomCentre, new Vector2(293, 201));

            l2.Fade(272790, 273034,0, 1);
            l2.Move(272790,275988,new Vector2(293, 201),new Vector2(310, 201));
            l2.Scale(272634, 0.23f);
            l2.Color(272634, Color4.DarkSlateBlue);

            // l2.Move(275701,275988, new Vector2(308, 365), new Vector2(308, 350));
            l2.Fade(275701,275988, 1, 0);
            //l2Mirror.Rotate(272634, Math.PI);
            l2Mirror.FlipV(272634);
            l2Mirror.Fade(272790, 273034,0, 0.5);
            l2Mirror.Move(272790,275988,new Vector2(293, 361),new Vector2(300, 361));

            l2Mirror.ScaleVec(272634, 0.23f, 0.2f);
            // l2Mirror.ScaleVec(275701,275988, 0.23f, 0.2f, 0.23f, 0.5f);
            l2Mirror.Fade(275701,275988, 0.5, 0);
            l2Mirror.Color(272634, Color4.DarkSlateBlue);

            OsbSprite l3 = aboveWater.CreateSprite(lyricFont.GetTexture("tell me if it's not on purpose").Path, OsbOrigin.BottomCentre, new Vector2(-10, 144));
            l3.Move(275900,281253, new Vector2(100, 144), new Vector2(120, 144));
            l3.Fade(275900, 275981,0, 1);
            l3.Fade(281055, 281253, 1, 0);
            l3.Scale(275900, 0.23f);
            l3.Color(275900, Color4.DarkSlateBlue);

            l3Mirror.Move(275900,281253, new Vector2(100, 324), new Vector2(110, 324));
            l3Mirror.FlipV(275900);
            l3Mirror.Fade(275900, 275981,0, 0.5);
            l3Mirror.Fade(281055, 281253, 0.5, 0);
            l3Mirror.ScaleVec(275900, 0.23f, 0.15);
            l3Mirror.Color(275900, Color4.DarkSlateBlue);

            OsbSprite l4 = aboveWater.CreateSprite(lyricFont.GetTexture("why'd you do it again").Path, OsbOrigin.BottomCentre, new Vector2(453, 199));

            l4.Move(278646,281253, new Vector2(453, 199), new Vector2(465, 199));
            l4.Fade(278646, 278753,0, 1);
            l4.Fade(281055, 281253, 1, 0);
            l4.Scale(278646, 0.23f);
            l4.Color(278646, Color4.DarkSlateBlue);

            l4Mirror.Move(278646,281253, new Vector2(453, 340), new Vector2(455, 340));
            l4Mirror.FlipV(278646);
            l4Mirror.Fade(278646, 278753,0, 0.5);
            l4Mirror.Fade(281055, 281253, 0.5, 0);
            l4Mirror.ScaleVec(278646, 0.23f, 0.15);
            l4Mirror.Color(278646, Color4.DarkSlateBlue);

            OsbSprite l5 = aboveWater.CreateSprite(lyricFont.GetTexture("just admit it").Path, OsbOrigin.BottomCentre, new Vector2(-12, 166));

            l5.Move(281346,288998, new Vector2(-12, 166), new Vector2(15, 166));
            l5.Fade(281346, 281527,0, 1);
            l5.Fade(288781, 288998, 1, 0);
            l5.Scale(281346, 0.23f);
            l5.Color(281346, Color4.DarkSlateBlue);

            l5Mirror.Move(281346,288998, new Vector2(-12, 340), new Vector2(0, 340));
            l5Mirror.FlipV(281346);
            l5Mirror.Fade(281346, 281527,0, 0.5);
            l5Mirror.Fade(288781, 288998, 0.5, 0);
            l5Mirror.ScaleVec(281346, 0.23f, 0.15);
            l5Mirror.Color(281346, Color4.DarkSlateBlue);

            OsbSprite l6 = aboveWater.CreateSprite(lyricFont.GetTexture("you don't have a world you're up against").Path, OsbOrigin.BottomCentre, new Vector2(403, 219));

            l6.Move(282768,288998, new Vector2(403, 219), new Vector2(415, 219));
            l6.Fade(282768, 282861,0, 1);
            l6.Fade(288781, 288998, 1, 0);
            l6.Scale(282768, 0.2f);
            l6.Color(282768, Color4.DarkSlateBlue);

            l6Mirror.Move(282768,288998, new Vector2(403, 318), new Vector2(405, 318));
            l6Mirror.FlipV(282768);
            l6Mirror.Fade(282768, 282861,0, 0.5);
            l6Mirror.Fade(288781, 288998, 0.5, 0);
            l6Mirror.ScaleVec(282768, 0.2f, 0.1);
            l6Mirror.Color(282768, Color4.DarkSlateBlue);
        }
    }
}
