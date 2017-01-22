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
        }

        CameraPerspective MainCam = new CameraPerspective()
        {
            Eye = Vector3.UnitY,
            Up = Vector3.UnitY,
            FieldOfView = 120
        };
    }
}
