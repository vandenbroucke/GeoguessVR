using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets
{
    public class TextureHelper
    {
        public static Texture2D GetTexture(GMAPS.Sizes siz, GMAPS.Location loc, GMAPS.ViewAngle va)
        {
            System.Console.WriteLine(va.Heading);
            Texture2D tex = new Texture2D(siz.X, siz.Y, TextureFormat.DXT1, false);
            WWW www = new WWW(GMAPS.getAPIURL(siz, loc, va));

            while (!www.isDone) { }

            www.LoadImageIntoTexture(tex);
            return tex;
        }
        public static void UpdateSkybox(SkyboxTexture sbTexture)
        {
            Material m = new Material(Shader.Find("RenderFX/Skybox"));
            m.SetTexture("_FrontTex", sbTexture.Front);
            m.SetTexture("_BackTex", sbTexture.Back);
            m.SetTexture("_LeftTex", sbTexture.Left);
            m.SetTexture("_RightTex", sbTexture.Right);
            m.SetTexture("_UpTex", sbTexture.Up);
            m.SetTexture("_DownTex", sbTexture.Down);
            RenderSettings.skybox = m;
        }
    }
}


