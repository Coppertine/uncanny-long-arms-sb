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
    public class Inlaws : StoryboardObjectGenerator
    {
        // Fun fact, this was a demo that got showcased on a livestream around 2 years ago.
        // It was slightly different of course.


        // idea: REALISTIC (to a degree) ocean sundown, with the background being slowly infected
        // Layers should be as follows from back to front
        /* 
            Pixel / gradient background
            scribble R/G/B
            Sun
            Mirrored Lyrics (for water reflection)
            particles??
            dimmed pixel (water)
            Lyrics
        */
        // Because of this, I can't exactly get lyrics from the same fonts cross-scripts.
        // 266328 - 324453
        StoryboardLayer backgroundBelowMirrorLayer, backgroundAfterMirrorLayer, foregroundLayer;
        StoryboardSegment gradientSkyLayer, cloudsLayer, scribblesLayer, sunLayer, mirrorLayer, waterLayer, lyricsLayer, foregroundScribblesLayer, noiseLayer;

        public override void Generate()
        {
            OsbSprite voiceSun = GetLayer("Background").CreateSprite("sb/light.png");
            voiceSun.Scale(266978,0.2);
            voiceSun.Fade(OsbEasing.OutExpo,266978, 267180, 0, 0.7);
            voiceSun.Fade(267180,267409,0.7,0.4);
            voiceSun.Fade(OsbEasing.OutExpo,267409,267521, 0.4, 0.64);
            voiceSun.Fade(267521, 267700, 0.64, 0.4);
            voiceSun.Fade(OsbEasing.OutExpo,267700,267891, 0.4,0.72);
            voiceSun.Fade(267891,268013,0.72,0.4);
            voiceSun.Fade(OsbEasing.OutExpo,268013,268203, 0.4,0.6);
            voiceSun.Fade(268203,268339,0.6,0.4);
            voiceSun.Fade(OsbEasing.OutExpo,268339,268544, 0.4,0.8);
            voiceSun.Fade(268544,268750,0.8,0.6);
            voiceSun.Fade(OsbEasing.OutExpo,268750,268884, 0.6,0.75);
            voiceSun.Fade(268884,269225,0.75,0.6);
            voiceSun.Fade(269225,269369,0.75,0.6);
            voiceSun.Fade(OsbEasing.OutExpo,269369,269566, 0.6,0.85);
            voiceSun.Fade(269566,269907,0.85,0.6);
            voiceSun.Fade(269907,270248,0.75,0.6);
            voiceSun.Fade(270248,270419,0.6,0.4);
            voiceSun.Fade(OsbEasing.OutExpo,270419,270589,0.4,0.8);
            voiceSun.Fade(270589,270930,0.8,0.6);
            voiceSun.Fade(270930,271271,0.75,0.5);
            voiceSun.Fade(271271,271612,0.6,0.4);
            voiceSun.Fade(271612,271953,0.7,0.4);
            voiceSun.Scale(271953,272294,0.5,0.2);



            OsbAnimation noise = GetLayer("Foreground").CreateAnimation("sb/noise/.png", 4, 33.333333333, OsbLoopType.LoopForever);
            noise.Fade(266978, 267180, 0, 0.8);
            noise.Additive(266978);
            noise.Fade(272634, 0);


		    // Segment time!!
            backgroundBelowMirrorLayer = GetLayer("Background - Before Mirror Lyrics");
            backgroundAfterMirrorLayer = GetLayer("Background - After Mirror Lyrics");
            foregroundLayer = GetLayer("Foreground");

            gradientSkyLayer = backgroundBelowMirrorLayer.CreateSegment();
            cloudsLayer = backgroundBelowMirrorLayer.CreateSegment();
            scribblesLayer = backgroundBelowMirrorLayer.CreateSegment();
            sunLayer = backgroundBelowMirrorLayer.CreateSegment();
            mirrorLayer = backgroundBelowMirrorLayer.CreateSegment();
            waterLayer = backgroundAfterMirrorLayer.CreateSegment();

            // FontTexture line1Texture = lyricFont.GetTexture("How did you get everyone to think that you were depressed?");
            OsbSprite skybg = gradientSkyLayer.CreateSprite("sb/sky.png", OsbOrigin.BottomCentre, new Vector2(320, 280));
            skybg.Scale(272634, 0.444444f);
            skybg.Fade(272634, 1);
            skybg.Fade(310305, 310816, 1, 0);
            double t = 400;
            skybg.Color(288998, 310816, Color4.White, Color4.Red);

            // Dimmer flip to make an illusion of water, idea taken from Partyu's Lowermost Revolt
            OsbSprite skybgflip = gradientSkyLayer.CreateSprite("sb/sky.png", OsbOrigin.TopCentre, new Vector2(320, 280));
            skybgflip.Fade(272634, 0.8);
            skybgflip.Fade(310305, 310816, 0.8, 0);
            skybgflip.FlipV(272634);
            // skybgflip.Fade(283544, 283544 + t, 1, 0.8);
            skybgflip.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.5);
            skybgflip.Color(288998, 310816, Color4.White, Color4.PaleVioletRed);
            skybgflip.Scale(272634, 0.444444f);


            // Clouds?
            for(int cloudi = 0; cloudi <= 20; cloudi++)
            {
                float startX = Random(-110, 618);
                float endX = startX + Random(30, 250);
                CreateCloud(272634, 310816, $"{Random(0,4)}.png", startX, endX);
            }
            // Scribbles
            OsbSprite scribbleR = scribblesLayer.CreateAnimation("sb/scribble/.png", 4, 285, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, new Vector2(320, 280));
		    OsbSprite scribbleG = scribblesLayer.CreateAnimation("sb/scribble/.png", 4, 162.81383, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, new Vector2(320, 280));
		    OsbSprite scribbleB = scribblesLayer.CreateAnimation("sb/scribble/.png", 4, 351.16381, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, new Vector2(320, 280));

            scribbleR.Fade(OsbEasing.InExpo,288998 - 200, 288998, 0, 1);
            scribbleG.Fade(OsbEasing.InExpo,288998 - 200, 288998, 0, 1);
            scribbleB.Fade(OsbEasing.InExpo,288998 - 200, 288998, 0, 1);
            scribbleR.Additive(288998);
            scribbleG.Additive(288998);
            scribbleB.Additive(288998);
            scribbleR.Color(288998, Color4.Red);
            scribbleG.Color(288998, Color4.Green);
            scribbleB.Color(288998, Color4.Blue);
            scribbleR.Scale(288998, 0.444444f);
            scribbleG.Scale(288998, 0.444444f);
            scribbleB.Scale(288998, 0.444444f);
            
  
            scribbleR.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.5);
            scribbleG.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.5);
            scribbleB.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.5);

            scribbleR.Fade(310305, 310816, 0.5, 0);
            scribbleG.Fade(310305, 310816, 0.5, 0);
            scribbleB.Fade(310305, 310816, 0.5, 0);


            //Mirror
             // Scribbles
            OsbSprite scribbleRMirror = mirrorLayer.CreateAnimation("sb/scribble/b.png", 4, 285, OsbLoopType.LoopForever, OsbOrigin.TopCentre, new Vector2(320, 280));
		    OsbSprite scribbleGMirror = mirrorLayer.CreateAnimation("sb/scribble/b.png", 4, 162.81383, OsbLoopType.LoopForever, OsbOrigin.TopCentre, new Vector2(320, 280));
		    OsbSprite scribbleBMirror = mirrorLayer.CreateAnimation("sb/scribble/b.png", 4, 351.16381, OsbLoopType.LoopForever, OsbOrigin.TopCentre, new Vector2(320, 280));

            scribbleRMirror.Fade(OsbEasing.InExpo, 288998 - 200, 288998, 0, 1);
            scribbleGMirror.Fade(OsbEasing.InExpo, 288998 - 200, 288998, 0, 1);
            scribbleBMirror.Fade(OsbEasing.InExpo, 288998 - 200, 288998, 0, 1);
            scribbleRMirror.Additive(288998);
            scribbleGMirror.Additive(288998);
            scribbleBMirror.Additive(288998);
            scribbleRMirror.FlipV(288998);
            scribbleGMirror.FlipV(288998);
            scribbleBMirror.FlipV(288998);
            scribbleRMirror.Color(288998, Color4.Red);
            scribbleGMirror.Color(288998, Color4.Green);
            scribbleBMirror.Color(288998, Color4.Blue);
            scribbleRMirror.Scale(288998, 0.444444f);
            scribbleGMirror.Scale(288998, 0.444444f);
            scribbleBMirror.Scale(288998, 0.444444f);
            
            scribbleRMirror.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.2);
            scribbleGMirror.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.2);
            scribbleBMirror.Fade(OsbEasing.OutExpo, 288998, 288998 + t, 1, 0.2);

            scribbleRMirror.Fade(310305, 310816, 0.2, 0);
            scribbleGMirror.Fade(310305, 310816, 0.2, 0);
            scribbleBMirror.Fade(310305, 310816, 0.2, 0);

            OsbSprite sun = sunLayer.CreateSprite("sb/light.png");
            sun.Fade(272634, 273146, 1, 0.8);
            sun.MoveY(272634, 240); // JUST ON THE CATION THAT LAZER WILL IGNORE THIS AND SET Y to 0 
            sun.Fade(310305, 310816, 0.8, 0);
            sun.Scale(OsbEasing.OutExpo, 272634,273069,1, 0.15);
            sun.Scale(278089,278600,0.2, 0.15);
            sun.Scale(283544,284566,0.2, 0.15);
            sun.Scale(310305, 310816, 0.15, 0);
            sun.MoveY(310305, 310816, 240, 280);
            sun.Additive(272634);
            

        }

        private void CreateCloud(int startTime, int endTime, string filePath, float startX, float endX)
        {
            OsbSprite regularCloud = cloudsLayer.CreateSprite("sb/cloud/" + filePath);
            OsbSprite mirrorCloud = cloudsLayer.CreateSprite("sb/cloud/b" + filePath);
            
            double scale = Random(0.05, 0.2);
            bool flipH = Random(1.0f) > 0.5;
            Log(scale);
            float posY = 240;
            float mirrorY = 240;
            if(scale > 0.001 && scale <= 0.1)
            {
                posY = 190;
                mirrorY = 333;
            }

            if(scale > 0.1 && scale <= 0.15)
            {
                posY = 168;
                mirrorY = 358;
            }

            if(scale > 0.15 && scale < 0.3)
            {
                posY = 68;
                mirrorY = 428;
            }
            // regular cloud
            regularCloud.Move(startTime, endTime, new Vector2(startX, posY),new Vector2(endX, posY));
            if (flipH) regularCloud.FlipH(startTime);
            regularCloud.Scale(startTime, scale);
            regularCloud.Fade(startTime, 1);
            regularCloud.Fade(endTime - 200, endTime, 1, 0);

            // mirror cloud
            mirrorCloud.Move(startTime, endTime, new Vector2(startX, mirrorY),new Vector2(endX, mirrorY));
            mirrorCloud.Fade(startTime, 0.4);
            if (flipH) mirrorCloud.FlipH(startTime);
            mirrorCloud.FlipV(startTime);
            mirrorCloud.Scale(startTime, scale);
            mirrorCloud.Fade(endTime - 200, endTime, 0.4, 0);
        }
    }
}
