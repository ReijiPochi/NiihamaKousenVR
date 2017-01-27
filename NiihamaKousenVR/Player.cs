using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB.Gaming.FPS;
using MATAPB;

using Vector3 = System.Numerics.Vector3;

namespace NiihamaKousenVR
{
    public class Player : PlayerBase
    {
        public Player()
        {
            PlayerCam = MainCam;

            SpeedFBDelta = 30.0;
            SpeedLRDelta = 30.0;
            HeightDelta = 0.5;

            angleUD = 1.6;
            angleLR = 0.5;
        }

        bool respawnAnimation = true;

        CameraPerspective MainCam = new CameraPerspective()
        {
            Eye = new Vector3(-89.5f, 150.0f, 47.33f),
            Up = Vector3.UnitY,
            FieldOfView = 80
        };

        public override void Draw(RenderingContext context)
        {
            base.Draw(context);
        }

        public override void Move(MoveData data)
        {
            if (respawnAnimation)
            {
                HeightDelta *= 1.03;

                if (angleUD >= 0.001)
                {
                    angleUD /= 1.015;
                    angleUD -= 0.003;
                }

                if (angleLR >= 0.001)
                {
                    angleLR /= 1.02;
                    angleLR -= 0.001;
                }

                if (angleUD <= 0.001)
                {
                    angleUD = 0.0;
                    angleLR = 0.0;
                    HeightDelta = 10.0;
                    respawnAnimation = false;
                    GameApp.MainWorld.showHUD = true;
                }
            }

            base.Move(data);
        }
    }
}
