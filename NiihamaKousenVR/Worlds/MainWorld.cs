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
using Plane = MATAPB.Objects.Primitive.Plane;
using System.Windows;
using MATAPB.Gaming.FPS;
using System.Windows.Input;
using System.Numerics;
using MATAPB.PostEffect;
using MATAPB.Gaming;

namespace NiihamaKousenVR.Worlds
{
    public class MainWorld : World
    {
        public MainWorld()
        {
            InitLight();
            Effect = new Fog();

            sky.Tags.AddTag(new ColorTexture(@"Objects\Sky.png"));
            buildingD.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureD.png"), new Lighting(), new HeightToColor() });
            buildingS.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureS.png"), new Lighting(), new HeightToColor() });
            buildingMon.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureMon.png"), new Lighting(), new HeightToColor() });
            bulidingShou.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureShou.png"), new Lighting(), new HeightToColor() });
            buildingZ.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureZ.png"), new Lighting(), new HeightToColor() });
            bulidingLab.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureLab.png"), new Lighting(), new HeightToColor() });
            bulidingC.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureC.png"), new Lighting(), new HeightToColor() });
            bulidingG.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureG.png"), new Lighting(), new HeightToColor() });
            floor.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\地面.png"), new Lighting() });
            kusozako.Tags.AddTag(new Tag[] { new SolidColor(SolidColorOverwriteMode.ColorAndAlpha, new MatColor(1.0, 0.9, 0.9, 0.85)), new Lighting(), new HeightToColor() });
            underGround.Tags.AddTag(new Tag[] { new SolidColor(SolidColorOverwriteMode.ColorAndAlpha, new MatColor(1.0, 1.0, 1.0, 1.0)) });
            underGround.PSRTag.Position = -Vector3.UnitY;
            flagD.Tags.RemoveTag(flagD.CameraTag);
            flagD.Tags.AddTag(new LookCamera() { Scale = new Vector3(0.8f), Position = mineD.MinePosition });
            flagD.Tags.InsertToFirst(hoverTagD);
            flagShou.Tags.RemoveTag(flagShou.CameraTag);
            flagShou.Tags.AddTag(new LookCamera() { Scale = new Vector3(0.8f), Position = mineShou.MinePosition });
            flagShou.Tags.InsertToFirst(hoverTagShou);
            flagMon.Tags.RemoveTag(flagMon.CameraTag);
            flagMon.Tags.AddTag(new LookCamera() { Scale = new Vector3(0.8f), Position = mineMon.MinePosition });
            flagMon.Tags.InsertToFirst(hoverTagMon);

            hoverTagD.Hop();
            hoverTagShou.Hop();
            hoverTagMon.Hop();


            hopupD.PSRTag.Position = mineD.MinePosition;
            hopupD.PSRTag.Scale = new Vector3(1.5f);
            hopupD.PSRTag.Rotation = new Vector3(-0.1f, (float)Math.PI, 0);
            hopupD.Tags.InsertToFirst(hopTagD);
            mineD.MineHit += MineD_MineHit;
            mineD.MineLeave += MineD_MineLeave;

            hopupShou.PSRTag.Position = mineShou.MinePosition;
            hopupShou.PSRTag.Scale = new Vector3(1.5f);
            hopupShou.Tags.InsertToFirst(hopTagShou);
            mineShou.MineHit += MineShow_MineHit;
            mineShou.MineLeave += MineShow_MineLeave;

            hopupMon.PSRTag.Position = mineMon.MinePosition;
            hopupMon.PSRTag.Scale = new Vector3(1.5f);
            hopupMon.PSRTag.Rotation = new Vector3(-0.1f, (float)(Math.PI / 2.0), 0);
            hopupMon.Tags.InsertToFirst(hopTagMon);
            mineMon.MineHit += MineMon_MineHit;
            mineMon.MineLeave += MineMon_MineLeave;

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
            Objects.Add(underGround);
            Objects.Add(hopupD);
            Objects.Add(hopupShou);
            Objects.Add(hopupMon);
            
            OverlayObjects.Add(flagD);
            OverlayObjects.Add(flagShou);
            OverlayObjects.Add(flagMon);

            ActiveCamera = player.PlayerCam;

            Keyboard.KeyInput += Keyboard_KeyInput;
        }

        RenderingCanvas mainCanvas = new RenderingCanvas((int)(PresentationBase.ViewArea.ActualWidth), (int)(PresentationBase.ViewArea.ActualHeight), 2);
        RenderingCanvas mainCanvasPost = new RenderingCanvas((int)(PresentationBase.ViewArea.ActualWidth), (int)(PresentationBase.ViewArea.ActualHeight), 2);
        RenderingCanvas hudCanvas = new RenderingCanvas((int)(PresentationBase.ViewArea.ActualWidth), (int)(PresentationBase.ViewArea.ActualHeight), 2);

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
        Plane underGround = new Plane(1000, 1000, Orientations.plusY);

        Picture hopupD = new Picture(@"Objects\スライド1.PNG");
        Picture hopupShou = new Picture(@"Objects\スライド2.PNG");
        Picture hopupMon = new Picture(@"Objects\スライド3.PNG");
        Picture flagD = new Picture(@"Objects\Flag.png");
        Picture flagShou = new Picture(@"Objects\Flag.png");
        Picture flagMon = new Picture(@"Objects\Flag.png");

        SetumeiHopup hopTagD = new SetumeiHopup();
        FlagHopup hoverTagD = new FlagHopup();
        SetumeiHopup hopTagShou = new SetumeiHopup();
        FlagHopup hoverTagShou = new FlagHopup();
        SetumeiHopup hopTagMon = new SetumeiHopup();
        FlagHopup hoverTagMon = new FlagHopup();

        Mine mineD = new Mine() { MinePosition = new Vector3(-89.5f, 0.5f, 56.73f), MineRadius = 4.0, Hysteresis = 1.0 };
        Mine mineShou = new Mine() { MinePosition = new Vector3(-44.9f, 0.5f, -82.0f), MineRadius = 4.0, Hysteresis = 1.0 };
        Mine mineMon = new Mine() { MinePosition = new Vector3(-123.5f, 0.5f, -68.5f), MineRadius = 4.0, Hysteresis = 1.0 };

        HUDWorld hudWorld = new HUDWorld();
        MiniMapWorld miniMapWorld = new MiniMapWorld();

        public bool showHUD = false;

        private void Keyboard_KeyInput(Key key)
        {
            if (key == Key.K)
            {
                hudWorld.kintama.Visible = !hudWorld.kintama.Visible;
            }

            if (key == Key.P)
            {
                hudWorld.pinki.Visible = true;
            }

            if (key == Key.R)
            {
                player.Respawn();
            }
        }

        private void MineD_MineLeave()
        {
            hopTagD.Close();
            hudWorld.score.TextValue = "守衛室へ向かえ！";
        }

        private void MineD_MineHit()
        {
            hopTagD.Hop();
            hudWorld.score.TextValue = "電子制御工学科へ\nようこそ！！";
        }

        private void MineShow_MineLeave()
        {
            hopTagShou.Close();
            hudWorld.score.TextValue = "電子棟へ向かえ！";
        }

        private void MineShow_MineHit()
        {
            hopTagShou.Hop();
            hudWorld.score.TextValue = "保健室に\n菰田先生がいるぞ！！";
        }

        private void MineMon_MineLeave()
        {
            hopTagMon.Close();
            hudWorld.score.TextValue = "尚友会館へ向かえ！";
        }

        private void MineMon_MineHit()
        {
            hopTagMon.Hop();
            hudWorld.score.TextValue = "新居浜高専に\n入学、しよう！！";
        }

        public override void Render(RenderingContext context)
        {
            MovePlayer();

            mineD.TargetPosition = player.PlayerCam.Eye;
            mineShou.TargetPosition = player.PlayerCam.Eye;
            mineMon.TargetPosition = player.PlayerCam.Eye;

            miniMapWorld.map.PSRTag.Position = new Vector3((float)(-player.PlayerCam.Eye.X), 0.0f, (float)(-player.PlayerCam.Eye.Z));
            miniMapWorld.map.PSRTag.Rotation = new Vector3(0, (float)player.angleLR, 0);
            miniMapWorld.Zoom = player.PlayerCam.Eye.Y * 1.8 + 50.0;

            hudWorld.miniMapCanvas.SetCanvas();
            {
                hudWorld.miniMapCanvas.ClearCanvas();
                context.canvas = hudWorld.miniMapCanvas;
                miniMapWorld.Render(context);
            }

            mainCanvas.SetCanvas();
            {
                mainCanvas.ClearCanvas();
                context.canvas = mainCanvas;
                base.Render(context);
            }

            //SwitchAndResolveToBackbuffer(mainCanvas);
            //mainCanvasPost.SetCanvas();
            PresentationBase.GraphicsDevice.ImmediateContext.OutputMerger.SetTargets(mainCanvasPost.renderTarget);
            Effect.Apply(mainCanvas);
            mainCanvasPost.Resolve();

            if (showHUD)
            {
                hudCanvas.SetCanvas();
                context.canvas = hudCanvas;
                hudWorld.Render(context);

                SwitchAndResolveToBackbuffer(hudCanvas);
            }

            if (hudWorld.pinki.Visible)
                hudWorld.pinki.Visible = false;
        }

        private void InitLight()
        {
            GlobalLight1.Color = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
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
