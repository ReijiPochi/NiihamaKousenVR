using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Objects;
using MATAPB.Objects.Tags;

using Keyboard = MATAPB.Input.Keyboard;
using Vector3 = System.Numerics.Vector3;
using System.Windows;
using MATAPB.Gaming.FPS;
using System.Windows.Input;
using System.Numerics;

namespace NiihamaKousenVR.Worlds
{
    public class MainWorld : World
    {
        public MainWorld()
        {
            InitLight();

            sky.Tags.AddTag(new ColorTexture(@"Objects\Sky.png"));
            buildingD.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureD.png"), new Lighting() });
            buildingS.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureS.png"), new Lighting() });
            buildingMon.Tags.AddTag(new Tag[] { new ColorTexture(@"Objects\TextureMon.png"), new Lighting() });

            Objects.Add(sky);
            Objects.Add(buildingD);
            Objects.Add(buildingS);
            Objects.Add(buildingMon);

            ActiveCamera = player.PlayerCam;
        }

        Player player = new Player();

        Object3D sky = new Object3D(@"Objects\Sky.obj");
        Object3D buildingD = new Object3D(@"Objects\BuildingD.obj");
        Object3D buildingS = new Object3D(@"Objects\BuildingS.obj");
        Object3D buildingMon = new Object3D(@"Objects\BuildingMon.obj");

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

                hudWorld.Render(context);
            }
        }

        private void InitLight()
        {
            GlobalLight1.Color = new Vector4(1.1f, 1.1f, 1.1f, 0.0f);
            GlobalLight1.Direction = new Vector4(2.0f, -2.0f, -10.0f, 0);
            GlobalLight1.Ambitent = new Vector4(0.0f, 0.03f, 0.1f, 0);
        }

        private void MovePlayer()
        {
            Point mouseDelta = MATAPB.Input.Mouse.GetDelta();

            MoveData data = new MoveData()
            {
                deltaAngleLR = mouseDelta.X * 0.3,
                deltaAngleUD = mouseDelta.Y * 0.3
            };

            if (Keyboard.KeyStates[Key.D])
            {
                if (Keyboard.KeyStates[Key.A])
                    data.speedLR = 0;
                else
                    data.speedLR = -3;
            }
            else if (Keyboard.KeyStates[Key.A]) data.speedLR = 3;
            else data.speedLR = 0;

            if (Keyboard.KeyStates[Key.W])
            {
                if (Keyboard.KeyStates[Key.S])
                    data.speedFB = 0;
                else
                    data.speedFB = 3;
            }
            else if (Keyboard.KeyStates[Key.S]) data.speedFB = -3;
            else data.speedFB = 0;

            if (Keyboard.KeyStates[Key.LeftShift])
            {
                data.speedFB *= 2.0;
                data.speedLR *= 2.0;
            }

            if (Keyboard.KeyStates[Key.Space])
            {
                data.height = 2.5;
            }
            else if (Keyboard.KeyStates[Key.LeftCtrl])
            {
                data.height = 0.9;
            }
            else
            {
                data.height = 1.6;
            }

            player.Move(data);
        }
    }
}
