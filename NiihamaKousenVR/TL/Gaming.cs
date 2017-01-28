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
                PresentationBase.AnimationClock.Pause();
                timeCount++;
                Thread.Sleep(1);
                if (timeCount > 2000)
                    Application.Current.Dispatcher.Invoke(() => { Application.Current.Shutdown(); });
            }

            if (!PresentationBase.AnimationClock.IsCounting)
                PresentationBase.AnimationClock.Start();

            PresentationBase.World = GameApp.MainWorld;
            Application.Current.Dispatcher.Invoke(() => { MATAPB.Input.Mouse.Active = true; });
        }
    }
}
