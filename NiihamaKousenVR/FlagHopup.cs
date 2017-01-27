using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using MATAPB.Objects.Tags;

namespace NiihamaKousenVR
{
    public class FlagHopup : Hopup
    {
        public FlagHopup()
        {
            MaxPosition = Vector3.Zero;
            HoverAnimation = HoverAnimations.Wave;
            WaveRate = 0.8;
            WaveHeight = 0.02;
        }
    }
}
