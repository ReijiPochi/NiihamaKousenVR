using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB.Objects.Tags;
using System.Numerics;

namespace NiihamaKousenVR
{
    public class SetumeiHopup : Hopup
    {
        public SetumeiHopup()
        {
            HopupAnimation = HopupAnimations.PopLiner;
            HoverAnimation = HoverAnimations.Wave;
            WaveRate = 0.4;
            WaveHeight = 0.02;
            HopupTime = 0.3;
            MaxPosition = new Vector3(0, 0.5f, 0);
            CloseAnimation = CloseAnimations.DepopLiner;
        }
    }
}
