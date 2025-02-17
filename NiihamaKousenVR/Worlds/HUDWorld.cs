﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MATAPB;
using MATAPB.Objects.Tags;
using System.Numerics;
using MATAPB.Objects;

namespace NiihamaKousenVR.Worlds
{
    public class HUDWorld : World
    {
        public HUDWorld()
        {
            ActiveCamera = customCam;

            miniMap.PSRTag.Position = new Vector3((float)(-customCam.CameraWidth / 2 + 0.2), (float)(-customCam.CameraHeight / 2 + 0.2), 0);
            miniMap.PSRTag.Scale = new Vector3(0.3f);
            miniMap.PSRTag.Rotation = new Vector3(0.0f, 0.15f, 0.0f);

            miniMapCanvas = new RenderingCanvas(miniMapTex);
            ColorTexture miniMapTexTag = new ColorTexture(miniMapTex);
            miniMapTexTag.Opacity.Value = 0.85;
            miniMap.Tags.AddTag(miniMapTexTag);

            scoreBg.Tags.AddTag(new SolidColor(SolidColorOverwriteMode.ColorAndAlpha, new MatColor(0.4, 0.1, 0.1, 0.1)));
            scoreBg.PSRTag.Position = new Vector3((float)(customCam.CameraWidth / 2 - 0.2), (float)(-customCam.CameraHeight / 2 + 0.1), 0.0f);
            scoreBg.PSRTag.Rotation = new Vector3(0.0f, -0.15f, 0.0f);
            //scoreBg.CameraTag.UseCustomCamera = true;
            //scoreBg.CameraTag.CustomCamera = customCam;

            score.TextAndFont.Font = "ＤＦＰ綜藝体W5";
            score.TextValue = "電子棟へ向かえ！";
            score.FontSize = 50;
            score.PSRTag.Position = scoreBg.PSRTag.Position;
            score.PSRTag.Rotation = scoreBg.PSRTag.Rotation;
            score.PSRTag.Scale = new Vector3(0.5f);
            score.CameraTag.UseCustomCamera = true;
            score.CameraTag.CustomCamera = customCam;

            //centerCircle.CountOfPoints = 20;
            //centerCircle.LineTag.Thickness = 0.01;

            howTo.PSRTag.Position = new Vector3((float)(customCam.CameraWidth / 2 - 0.12), (float)(-customCam.CameraHeight / 2 + 0.25), 0.0f);
            howTo.PSRTag.Rotation = new Vector3(0.0f, -0.15f, 0.0f);

            kintama.Tags.InsertToFirst(kintamaHop);
            kintamaHop.Hop();

            Objects.Add(miniMap);
            Objects.Add(scoreBg);
            Objects.Add(kintama);
            //Objects.Add(centerCircle);
            OverlayObjects.Add(score);
            OverlayObjects.Add(howTo);
            OverlayObjects.Add(pinki);
        }

        Texture miniMapTex = new Texture(400, 400);
        public RenderingCanvas miniMapCanvas;
        Picture miniMap = new Picture(1, 1);

        public Text score = new Text(500, 200, new MatColor(1, 1, 1, 1));
        MATAPB.Objects.Primitive.Plane scoreBg = new MATAPB.Objects.Primitive.Plane(0.25, 0.1, Orientations.plusZ);

        public Picture kintama = new Picture(@"Objects\酔い止めりんご.png", 0.2) { Visible = false };
        public Picture pinki = new Picture(@"Objects\ピンキー.jpg", 4) { Visible = false };

        Picture howTo = new Picture(@"Objects\HowTo.png", 0.5);

        Hopup kintamaHop = new SetumeiHopup() { MaxPosition = Vector3.Zero, HoverAnimation = HoverAnimations.Wave, WaveRate = 1, WaveHeight = 0.005 };

        //Circle centerCircle = new Circle() { Radius = 0.05, Color = new MatColor(1.0f, 1.0f, 0.7f, 0.0f) };

        //public CameraOrthographic cam = new CameraOrthographic()
        //{
        //    CameraHeight = PresentationBase.ViewArea.ActualHeight / 1000.0,
        //    CameraWidth = PresentationBase.ViewArea.ActualWidth / 1000.0
        //};

        CameraPerspective customCam = new CameraPerspective()
        {
            Eye = Vector3.UnitZ,
            Target = Vector3.Zero,
            Up = Vector3.UnitY,
            Mode = CameraPerspectiveMode.UseWidthHeight,
            CameraHeight = PresentationBase.ViewArea.ActualHeight / 1000.0,
            CameraWidth = PresentationBase.ViewArea.ActualWidth / 1000.0
        };

        public override void Render(RenderingContext context)
        {
            //customCam.CameraUpdate(context);

            base.Render(context);
        }
    }
}
