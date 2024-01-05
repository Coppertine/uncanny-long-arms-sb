using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Verse : StoryboardObjectGenerator
    {   
        // 96726 - 130817
        public override void Generate()
        {
            // squiglge
            OsbSprite scribbleR = GetLayer("Background").CreateAnimation("sb/scribble/.png", 4, 523, OsbLoopType.LoopForever);
		    OsbSprite scribbleG = GetLayer("Background").CreateAnimation("sb/scribble/.png", 4, 484, OsbLoopType.LoopForever);
		    OsbSprite scribbleB = GetLayer("Background").CreateAnimation("sb/scribble/.png", 4, 384, OsbLoopType.LoopForever);

            scribbleR.Scale(125362, 0.444444f);
            scribbleG.Scale(125362, 0.444444f);
            scribbleG.FlipH(125362);
            scribbleB.Scale(125362, 0.444444f);

            scribbleR.Fade(125362, 0.3);
            scribbleG.Fade(125362, 0.3);
            scribbleB.Fade(125362, 0.3);
            scribbleR.Additive(125362);
            scribbleG.Additive(125362);
            scribbleB.Additive(125362);
            scribbleR.Color(125362, Color4.Red);
            scribbleG.Color(125362, Color4.Green);
            scribbleB.Color(125362, Color4.Blue);

            scribbleR.Fade(129283, 130817, 0.3, 0.1);
            scribbleG.Fade(129283, 130817, 0.3, 0.1);
            scribbleB.Fade(129283, 130817, 0.3, 0.1);

            OsbAnimation light = GetLayer("Background").CreateAnimation("sb/light/.jpg", 12, 83.33333333333333, OsbLoopType.LoopOnce);
            light.Scale(103544, 0.44444f);
            light.Fade(103544, 1);
            light.StartLoopGroup(104567, (124680 - 104567) / 4000);
                light.Fade(0, 2000, 1, 0.8);
                light.Fade(2000, 4000, 0.8, 1);
            light.EndGroup();
            light.Additive(103544);
            light.Fade(124680, 0.2);
            light.Fade(125362, 1);
            light.Fade(130817, 0);
            
		    longHand();

            

            // noise
            OsbAnimation noise = GetLayer("Background").CreateAnimation("sb/noise/.png", 4, 33.333333333, OsbLoopType.LoopForever);
            noise.Fade(129283, 130817, 0, 0.8);
            noise.Additive(129283);
            noise.Fade(130817, 0);
            
            // Discarded idea
            //armTunnel();
            
            OsbAnimation arms = GetLayer("Background").CreateAnimation("sb/arms/.png",8, (130817 - 129794) / 8, OsbLoopType.LoopOnce);
            arms.Scale(129794, 0.44444444f);
            arms.Additive(129794);
            arms.Fade(129794, 1);
            arms.Fade(130817, 0);
            
        }

        void longHand()
        {
            // 113771 - 119226
            OsbAnimation hand = GetLayer("Arm").CreateAnimation("sb/hand/.png", 3, Beatmap.GetTimingPointAt(113771).BeatDuration, OsbLoopType.LoopForever);
            hand.Rotate(113771, Math.PI / 2);
            hand.Fade(113771, 1);
            hand.Scale(113771, 0.2);
            hand.Move(OsbEasing.InOutQuad, 113771, 114453, new Vector2(-200, 240), new Vector2(203.324f, 320));
            hand.Move(OsbEasing.InOutExpo, 117521, 118374, new Vector2(203.324f, 320), new Vector2(860,320));
            hand.Fade(118374, 0);

            OsbAnimation arm = GetLayer("Arm").CreateAnimation("sb/arm/.png", 3, Beatmap.GetTimingPointAt(113771).BeatDuration, OsbLoopType.LoopForever);
            arm.Rotate(113771, Math.PI / 2);
            arm.Fade(113771, 1);
            arm.ScaleVec(113771, 0.2, 0.2);
            arm.Move(OsbEasing.InOutQuad, 113771, 114453, new Vector2(-450, 220), new Vector2(-90,290));
            arm.Move(114453, new Vector2(-70, 270));
            arm.Move(115135, new Vector2(-70, 280));
            arm.Move(115817, new Vector2(-90, 290));
            arm.Move(116499, new Vector2(-70, 270));
            arm.Move(117180, new Vector2(-70, 280));
            arm.ScaleVec(OsbEasing.InOutExpo, 117521, 118374, new Vector2(0.2f, 0.2f), new Vector2(0.2f, 0.95f));
            arm.Move(117862,new Vector2(-90, 290));
            arm.Move(118544,new Vector2(-70, 271));
            arm.ScaleVec(118544, new Vector2(0.2f, 1.16f));
            arm.ScaleVec(118714, 119226, new Vector2(0.2f, 1.16f), new Vector2(6f, 1.16f));
            arm.Fade(119226, 0);
        }

        // 125362 - 
        void armTunnel()
        {
            int tunnelLength = 250;
            int tunnelSpacing = 40;

            // Should feel like you are falling in the "rabbit hole"
            Scene3d scene = new Scene3d();
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.FarClip.Add(125362, 1550);
            camera.FarFade.Add(125362, 800);

            // let's move everything up by like 20 pixels
            camera.PositionX.Add(125362, 0);
            camera.PositionY.Add(125362, 0);
            camera.PositionZ.Add(125362, 20);

            camera.TargetPosition.Add(125362, new Vector3(10,-20,0));
            camera.TargetPosition.Add(127237, new Vector3(10,-20,0));
            camera.TargetPosition.Add(127919, new Vector3(10, 5, -30));
            camera.TargetPosition.Add(128089, new Vector3(10,20,0));
            

            Node3d armTunnelParent = new Node3d();

            armTunnelParent.PositionX.Add(125362, 0);
            armTunnelParent.PositionY.Add(125362, -1500);
            armTunnelParent.PositionZ.Add(125362, 20);


            for (int i = 1; i < tunnelLength; i++)
            {
                Animation3d armPlane = new Animation3d()
                {
                    SpritePath = "sb/arms/.png",
                    FrameCount = 2,
                    FrameDelay = 250
                };

                armPlane.ScaleX.Add(125362, 0.5f);
                armPlane.ScaleY.Add(125362, 0.5f);
                armPlane.ScaleZ.Add(125362, 0.5f);
                armPlane.SpriteRotation.Add(125362, Math.PI / Random(2,16));

                armPlane.PositionX.Add(125362, 0);
                armPlane.PositionY.Add(125362, i * tunnelSpacing);
                armPlane.PositionZ.Add(125362, 0);
                armPlane.SpriteRotation.Add(130817, Math.PI / Random(2, 8));

                armTunnelParent.Add(armPlane);
            }            

            armTunnelParent.PositionY.Add(130817, 120);

            scene.Add(armTunnelParent);
            scene.Generate(camera, GetLayer("Rabbit Hole"), 125362, 130817, Beatmap.GetTimingPointAt(130817).BeatDuration / 16);
        }
    }
}
