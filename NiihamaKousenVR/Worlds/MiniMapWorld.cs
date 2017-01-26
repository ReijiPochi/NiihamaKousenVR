using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Objects.Tags;
using MATAPB.Objects;
using System.Numerics;
using Plane = MATAPB.Objects.Primitive.Plane;

namespace NiihamaKousenVR.Worlds
{
    public class MiniMapWorld : World
    {
        public MiniMapWorld()
        {
            map.PSRTag.Order = PSROrder.SPR;
            map.Tags.AddTag(new ColorTexture(@"Objects\map.png"));
            bg.PSRTag.Position = new System.Numerics.Vector3(0, 0, -1);
            player.Tags.AddTag(new ColorTexture(@"Objects\PlayerSymbol.png"));
            player.PSRTag.Rotation = new Vector3(0, (float)(-Math.PI / 2.0), 0);

            Objects.Add(map);
            Objects.Add(bg);
            OverlayObjects.Add(player);

            ActiveCamera = cam;
        }

        //public Picture map = new Picture(@"Objects\map.png");
        public Object3D map = new Object3D(@"Objects\地図.obj");
        Picture bg = new Picture(@"Objects\BG.png", 20);
        Plane player = new Plane(5, 5, Orientations.plusY);

        public double Zoom
        {
            set
            {
                cam.CameraHeight = value;
                cam.CameraWidth = value;
                player.PSRTag.Scale = new Vector3((float)(value * 0.02));
            }
        }

        public CameraOrthographic cam = new CameraOrthographic()
        {
            Eye = Vector3.UnitY,
            Up = Vector3.UnitZ,
            CameraHeight = 70.0,
            CameraWidth = 70.0,
        };
    }
}
