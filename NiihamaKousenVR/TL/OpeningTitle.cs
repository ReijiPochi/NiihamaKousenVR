using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Timeline;
using NiihamaKousenVR.Worlds;

namespace NiihamaKousenVR.TL
{
    public class OpeningTitle : TimelineObject
    {
        public OpeningTitle()
        {
        }

        public override void Start()
        {
            base.Start();

            PresentationBase.World = GameApp.OpeningWorld;

            GameApp.OpeningWorld.logo.ColorTextureTag.Opacity.Value = 1.0;
        }

        public override void End()
        {
            base.End();

            GameApp.OpeningWorld.logo.ColorTextureTag.Opacity.Value = 0.0;
        }
    }
}
