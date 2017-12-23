using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets;

namespace Assets
{
    public class TextureHelper:MonoBehaviour
    {
        public static int DEFAULT_STREETVIEW_RES = 640;
        public static Material cachedSkyboxMaterial;
        public static void cacheTextureSkybox(GMAPS.Location loc)
        {
            Texture2D texUp = new Texture2D(DEFAULT_STREETVIEW_RES, DEFAULT_STREETVIEW_RES, TextureFormat.DXT1, false);
            Texture2D texDown = new Texture2D(DEFAULT_STREETVIEW_RES, DEFAULT_STREETVIEW_RES, TextureFormat.DXT1, false);
            Texture2D texFront = new Texture2D(DEFAULT_STREETVIEW_RES, DEFAULT_STREETVIEW_RES, TextureFormat.DXT1, false);
            Texture2D texBack = new Texture2D(DEFAULT_STREETVIEW_RES, DEFAULT_STREETVIEW_RES, TextureFormat.DXT1, false);
            Texture2D texLeft = new Texture2D(DEFAULT_STREETVIEW_RES, DEFAULT_STREETVIEW_RES, TextureFormat.DXT1, false);
            Texture2D texRight = new Texture2D(DEFAULT_STREETVIEW_RES, DEFAULT_STREETVIEW_RES, TextureFormat.DXT1, false);

            WWW wwwUp = new WWW(GMAPS.getAPIURL(DEFAULT_STREETVIEW_RES, loc, GMAPS.ViewAngle.DefaultUp));
            WWW wwwDown = new WWW(GMAPS.getAPIURL(DEFAULT_STREETVIEW_RES, loc, GMAPS.ViewAngle.DefaultDown));
            WWW wwwFront = new WWW(GMAPS.getAPIURL(DEFAULT_STREETVIEW_RES, loc, GMAPS.ViewAngle.DefaultFront));
            WWW wwwBack = new WWW(GMAPS.getAPIURL(DEFAULT_STREETVIEW_RES, loc, GMAPS.ViewAngle.DefaultBack));
            WWW wwwLeft = new WWW(GMAPS.getAPIURL(DEFAULT_STREETVIEW_RES, loc, GMAPS.ViewAngle.DefaultLeft));
            WWW wwwRight = new WWW(GMAPS.getAPIURL(DEFAULT_STREETVIEW_RES, loc, GMAPS.ViewAngle.DefaultRight));

            while (!wwwUp.isDone || !wwwDown.isDone || !wwwFront.isDone || !wwwBack.isDone || !wwwLeft.isDone || !wwwRight.isDone) { }

            wwwUp.LoadImageIntoTexture(texUp);
            wwwDown.LoadImageIntoTexture(texDown);
            wwwFront.LoadImageIntoTexture(texFront);
            wwwBack.LoadImageIntoTexture(texBack);
            wwwLeft.LoadImageIntoTexture(texLeft);
            wwwRight.LoadImageIntoTexture(texRight);

            Material m = new Material(Shader.Find("RenderFX/Skybox"));
            m.SetTexture("_FrontTex", texFront);
            m.SetTexture("_BackTex", texBack);
            m.SetTexture("_LeftTex", texLeft);
            m.SetTexture("_RightTex", texRight);
            m.SetTexture("_UpTex", texUp);
            m.SetTexture("_DownTex", texDown);

            TextureHelper.cachedSkyboxMaterial = m;
        }
        public static void UpdateSkybox()
        {            
            RenderSettings.skybox = cachedSkyboxMaterial;
        }
    }
}


