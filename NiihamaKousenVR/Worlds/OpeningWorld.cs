﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Objects;

namespace NiihamaKousenVR.Worlds
{
    public class OpeningWorld : World
    {
        public OpeningWorld()
        {
            ActiveCamera = cam;

            logo.ColorTextureTag.Opacity.ActualValue = 0.0;
            logo.ColorTextureTag.Opacity.Mode = AnimationMode.Liner;
            logo.ColorTextureTag.Opacity.Delta = 0.04;
            logo.ColorTextureTag.Opacity.Threshold = 0.04;

            Objects.Add(logo);
        }

        public CameraOrthographic cam = new CameraOrthographic()
        {
            CameraHeight = PresentationBase.ViewArea.ActualHeight / 1000.0,
            CameraWidth = PresentationBase.ViewArea.ActualWidth / 1000.0
        };

        public override void Render(RenderingContext context)
        {
            PresentationBase.SetAndClearBackBuffer();
            base.Render(context);
        }

        public Picture logo = new Picture(@"Objects\ロゴ.png", 0.5);
    }
}
