using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Storyboarding3d;
using StorybrewScripts;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using StorybrewCommon.Animations;

namespace StorybrewScripts
{
    public class Afterlife : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    // 174453 - 266363 

            OsbSprite pixelBG = GetLayer("Background").CreateSprite("sb/p.png");
            pixelBG.ScaleVec(174453, new Vector2(854, 480));
            // Secretariat
            pixelBG.Color(174453, "#A7BCD6");
            pixelBG.Fade(174453, 266328, 0.5,0.5);

            OsbSprite gradient = GetLayer("Background").CreateSprite("sb/gradient.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            gradient.ScaleVec(174453, 0.44444f, 0.222222f);
            gradient.Fade(174453, 1);
            gradient.Fade(266328, 0);
            gradient.Color(174453,"#1b324e"); 

            OsbSprite lightBG = GetLayer("Background").CreateSprite("sb/light.png");
            lightBG.Scale(174453, 0.2);
            // Watersocket
            lightBG.Color(174453, "#4FA504");
            int duration = 6000;
            lightBG.StartLoopGroup(174453, (266328-174453) / duration);
                lightBG.Fade(0, duration / 2, 0.5, 0.8);
                lightBG.Fade(duration / 2, duration, 0.8, 0.5);
            lightBG.EndGroup();
            // lightBG.Fade(174453, 266363, 0.5,0.5);
            lightBG.Additive(174453);

            // OsbAnimation waves = GetLayer("Background").CreateAnimation("sb/water/.jpg", 29, 100, OsbLoopType.LoopForever);
            // waves.Scale(174453, 0.4444444f);
            // waves.Additive(174453);
            // waves.Color(174453, Color4.White);
            // waves.Color(264453, 265305, Color4.Red, Color4.White);
            // waves.Fade(174453, 266363, 0.5, 0.5);
            Generate3DWaves();
            
            // transition flashlight
            OsbSprite flashlight = GetLayer("Foreground").CreateSprite("sb/flashlight.png");
            flashlight.Fade(174453 , 1);
            flashlight.Fade(174453 + 1500, 174453 + 2000, 1, 0);
            flashlight.Scale(174453, 174453 + 2000, 0.444, 8);
        
            OsbAnimation noise = GetLayer("Foreground").CreateAnimation("sb/noise/.png", 4, 33.3333, OsbLoopType.LoopForever);
            noise.Fade(242464, 264623, 0, 1);
            noise.Fade(266328, 0);
            noise.Additive(242464);

            OsbSprite vig = GetLayer("Foreground").CreateSprite("sb/vigw.png");
            vig.Fade(175305, 174453 + 2000, 0, 0.8);
            vig.Color(175305, Color4.Black);
            vig.Fade(223544,0.5);
            vig.Fade(254566, 264453, 0.5, 1);
            vig.Fade(264453, 266328, 0.2, 0.9);
            vig.Color(264453,264964, Color4.Red, Color4.Black);

            vig.Fade(266328, 0);
            
        }

        void Generate3DWaves() {
            Scene3d scene = new Scene3d();
            PerspectiveCamera camera = new PerspectiveCamera();
            scene.Root.PositionX.Add(174453,0);
            scene.Root.PositionY.Add(174453,0);
            scene.Root.PositionZ.Add(174453,0);
            camera.PositionZ.Add(174453, -100);
            camera.PositionX.Add(174453, 0);
            camera.PositionY.Add(174453, 0);

            Animation3d waves = new Animation3d() {
                SpritePath = "sb/water/.jpg",
                FrameCount = 29,
                FrameDelay = 100,
                LoopType = OsbLoopType.LoopForever,
                Additive = true
            };
            waves.PositionX.Add(174453, 0);
            waves.PositionY.Add(174453, 0);
            waves.PositionZ.Add(174453, 20);

            camera.TargetPosition.Add(174453, new Vector3(0, 0, 0));
            // camera.TargetPosition.Add(174453 + 10000, new Vector3(10,10,2));
            for(int t = 174453 + 5034; t < 264453; t += 10502) {
                camera.TargetPosition.Add(t, new Vector3(Random(-20.0f,20.0f), Random(-20.0f,20.0f), Random(-10,10)));
                camera.PositionZ.Add(t, Random(-50, -100.0f));
            }
            camera.PositionZ.Add(266328, -150);
            camera.TargetPosition.Add(266328, new Vector3(0,0,0));
            
                        
            scene.Add(waves);

            scene.Generate(camera, GetLayer("Foreground"), 174453, 266363, 10502 / 4);
        }
    }

}
