using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Objects;
using MATAPB.Objects.Tags;

using Keyboard = MATAPB.Input.Keyboard;
using Mouse = MATAPB.Input.Mouse;
using Vector3 = System.Numerics.Vector3;
using System.Windows;
using MATAPB.Gaming.FPS;
using System.Windows.Input;
using System.Numerics;
using MATAPB.PostEffect;

namespace NiihamaKousenVR.Worlds
{
    public class MainWorld : World
    {
        public MainWorld()
        {
            InitLight();
            Effect = new Straight();

            sky.Tags.AddTag(new ColorTexture(@"Objects\Sky.png"));
            buildingD.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureD.png"), new Lighting() });
            buildingS.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureS.png"), new Lighting() });
            buildingMon.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureMon.png"), new Lighting() });
            bulidingShou.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureShou.png"), new Lighting() });
            buildingZ.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureZ.png"), new Lighting() });
            bulidingLab.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureLab.png"), new Lighting() });
            bulidingC.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureC.png"), new Lighting() });
            bulidingG.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureG.png"), new Lighting() });
            floor.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\地面.png"), new Lighting() });
            kusozako.Tags.AddTag(new Tag[] { new SolidColor(SolidColorOverwriteMode.ColorAndAlpha, new MatColor(1.0, 1.0, 1.0, 0.95)), new Lighting() });

            Objects.Add(sky);
            Objects.Add(buildingD);
            Objects.Add(buildingS);
            Objects.Add(buildingMon);
            Objects.Add(bulidingShou);
            Objects.Add(buildingZ);
            Objects.Add(bulidingLab);
            Objects.Add(bulidingC);
            Objects.Add(bulidingG);
            Objects.Add(floor);
            Objects.Add(kusozako);

            ActiveCamera = player.PlayerCam;
        }

        Player player = new Player();

        Object3D sky = new Object3D(@"Objects\Sky.obj");
        Object3D buildingD = new Object3D(@"Objects\BuildingD.obj");
        Object3D buildingS = new Object3D(@"Objects\BuildingS.obj");
        Object3D buildingMon = new Object3D(@"Objects\BuildingMon.obj");
        Object3D bulidingShou = new Object3D(@"Objects\BuildingShou.obj");
        Object3D buildingZ = new Object3D(@"Objects\BuildingZ.obj");
        Object3D bulidingLab = new Object3D(@"Objects\BuildingLab.obj");
        Object3D bulidingC = new Object3D(@"Objects\BuildingC.obj");
        Object3D bulidingG = new Object3D(@"Objects\BuildingG.obj");
        Object3D floor = new Object3D(@"Objects\地面.obj");
        Object3D kusozako = new Object3D(@"Objects\クソザコくん.obj");

        HUDWorld hudWorld = new HUDWorld();
        MiniMapWorld miniMapWorld = new MiniMapWorld();

        public override void Render(RenderingContext context)
        {
            MovePlayer();

            miniMapWorld.map.PSRTag.Position = new Vector3((float)(player.PlayerCam.Eye.X / 100.0), (float)(-player.PlayerCam.Eye.Z / 100.0), 0.0f);
            miniMapWorld.map.PSRTag.Rotation = new Vector3(0, 0, (float)player.angleLR);

            hudWorld.miniMapCanvas.SetCanvas();
            {
                hudWorld.miniMapCanvas.ClearCanvas();
                context.canvas = hudWorld.miniMapCanvas;
                miniMapWorld.Render(context);
            }

            PresentationBase.DefaultCanvas.SetCanvas();
            {
                context.canvas = PresentationBase.DefaultCanvas;
                base.Render(context);
            }

            SwitchToBackbuffer();

            hudWorld.Render(context);
        }

        private void InitLight()
        {
            GlobalLight1.Color = new Vector4(1.1f, 1.1f, 1.1f, 0.0f);
            GlobalLight1.Direction = new Vector4(2.0f, -2.0f, -10.0f, 0);
            GlobalLight1.Ambitent = new Vector4(0.0f, 0.03f, 0.1f, 0);
        }

        double height = 1.2, heightSpeed = 0.0, accelLR = 2.0, accelFB = 2.0, additionalAccelLR = 0.0, additionalAccelFB = 0.0;

        private void MovePlayer()
        {
            Point mouseDelta = Mouse.GetDelta();

            MoveData data = new MoveData()
            {
                deltaAngleLR = mouseDelta.X * 0.3,
                deltaAngleUD = mouseDelta.Y * 0.3
            };

            double accelRate = PresentationBase.TimelengthOfFrame * 2.0;

            double additionalAccelRate = 0.0;
            if (Keyboard.KeyStates[Key.LeftShift])
            {
                additionalAccelRate = PresentationBase.TimelengthOfFrame * 3.0;
            }
            else
            {
                additionalAccelLR = 0.0;
                additionalAccelFB = 0.0;
            }

            if(Mouse.RightButtonDown)
            {
                data.fov = 35.0;
                data.deltaAngleLR *= 0.4;
                data.deltaAngleUD *= 0.4;
            }
            else
            {
                data.fov = 70.0;
            }

            if (Keyboard.KeyStates[Key.D])
            {
                if (Keyboard.KeyStates[Key.A])
                {
                    accelLR = 2.0;
                    additionalAccelLR = 0.0;
                    data.speedLR = 0;
                }
                else
                {
                    if (accelLR < 3.0) accelLR += accelRate;
                    if (additionalAccelLR < 7.0) additionalAccelLR += additionalAccelRate;
                    data.speedLR = -accelLR - additionalAccelLR;
                }
            }
            else if (Keyboard.KeyStates[Key.A])
            {
                if (accelLR < 3.0) accelLR += accelRate;
                if (additionalAccelLR < 7.0) additionalAccelLR += additionalAccelRate;
                data.speedLR = accelLR + additionalAccelLR;
            }
            else
            {
                accelLR = 2.0;
                additionalAccelLR = 0.0;
                data.speedLR = 0;
            }

            if(Keyboard.KeyStates[Key.W])
            {
                if (Keyboard.KeyStates[Key.S])
                {
                    accelFB = 2.0;
                    additionalAccelFB = 0.0;
                    data.speedFB = 0;
                }
                else
                {
                    if (accelFB < 3.0) accelFB += accelRate;
                    if (additionalAccelFB < 7.0) additionalAccelFB += additionalAccelRate;
                    data.speedFB = accelFB + additionalAccelFB;
                }
            }
            else if (Keyboard.KeyStates[Key.S])
            {
                if (accelFB < 3.0) accelFB += accelRate;
                if (additionalAccelFB < 7.0) additionalAccelFB += additionalAccelRate;
                data.speedFB = -accelFB - additionalAccelFB;
            }
            else
            {
                accelFB = 2.0;
                additionalAccelFB = 0.0;
                data.speedFB = 0;
            }

            if (Keyboard.KeyStates[Key.Space])
            {
                if (heightSpeed < 0.1) heightSpeed += 0.002;
            }
            else if (Keyboard.KeyStates[Key.LeftCtrl])
            {
                if (heightSpeed > -0.2) heightSpeed -= 0.003;
            }
            else
            {
                heightSpeed /= 1.1;
            }

            if (Math.Abs(heightSpeed) < 0.0001) heightSpeed = 0.0;

            height += heightSpeed;
            if (height > 30.0) height = 30.0;
            else if (height < 0.5) height = 0.5;

            data.height = height;

            player.Move(data);
        }
    }
}
