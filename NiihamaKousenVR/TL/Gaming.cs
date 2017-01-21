using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Timeline;
using System.Windows;
using System.Threading;

namespace NiihamaKousenVR.TL
{
    public class Gaming : TimelineObject
    {
        public override void Start()
        {
            base.Start();

            int timeCount = 0;
            while (GameApp.MainWorld == null)
            {
                timeCount++;
                Thread.Sleep(1);
                if (timeCount > 1000)
                    Application.Current.Shutdown();
            }

            PresentationBase.World = GameApp.MainWorld;
        }
    }
}
