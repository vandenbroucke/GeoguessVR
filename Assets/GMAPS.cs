using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class GMAPS
    {
        private const string API_KEY = "AIzaSyCRbK7pxxqNMDRmAWHHTeAyCdGT3bsOFiI";
        private static System.Random rnd = new System.Random();
        private static Location[] Challenges = new Location[] {
            new Location(){ Lat=51.2086317f,Long=3.2242202f},//BRUGES
            new Location(){ Lat=51.2184941f, Long=2.9223244f },//OSTEND
            new Location(){ Lat=51.5006867f, Long = -0.1220094f},//LONDON, Big Ben
            new Location(){Lat= -34.6831994f,Long=20.2342264f} //South Africa
        };



        public static string getAPIURL(Sizes siz, Location Loc, ViewAngle ViewAng)
        {
            return string.Format("maps.googleapis.com/maps/api/streetview?size={0}x{1}&location={2},{3}&heading={4}&pitch={5}&key={6}",
                siz.X,
                siz.Y,
                Loc.Lat,
                Loc.Long,
                ViewAng.Heading,
                ViewAng.Pitch,
                API_KEY);
        }

        public static void UpdateToGivenPosition(Location loc)
        {
            Sizes defaultSizes = new Sizes() { X = 640, Y = 640 };
            SkyboxTexture sTexture = new SkyboxTexture()
            {
                Up = TextureHelper.GetTexture(defaultSizes, loc, ViewAngle.DefaultUp),
                Down = TextureHelper.GetTexture(defaultSizes, loc, ViewAngle.DefaultDown),
                Front = TextureHelper.GetTexture(defaultSizes, loc, ViewAngle.DefaultFront),
                Back = TextureHelper.GetTexture(defaultSizes, loc, ViewAngle.DefaultBack),
                Left = TextureHelper.GetTexture(defaultSizes, loc, ViewAngle.DefaultLeft),
                Right = TextureHelper.GetTexture(defaultSizes, loc, ViewAngle.DefaultRight)
            };
            TextureHelper.UpdateSkybox(sTexture);
        }
        
        public static Location GetNewStreetViewCoordinates()
        {
           
            return Challenges[Random.Range(0,Challenges.Length-1)];
        
        }

        public class Location
        {
            public float Lat { get; set; }
            public float Long { get; set; }
        }
        public class Sizes
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        public class ViewAngle
        {
            public float Heading { get; set; }
            public float Pitch { get; set; }
            public static ViewAngle DefaultUp = new ViewAngle() { Heading = 0, Pitch = 90 };
            public static ViewAngle DefaultDown = new ViewAngle() { Heading = 0, Pitch = -90 };
            public static ViewAngle DefaultLeft = new ViewAngle() { Heading = 90, Pitch = 0 };
            public static ViewAngle DefaultFront = new ViewAngle() { Heading = 0, Pitch = 0 };
            public static ViewAngle DefaultRight = new ViewAngle() { Heading = 270, Pitch = 0 };
            public static ViewAngle DefaultBack = new ViewAngle() { Heading = 180, Pitch = 0 };
        }


    }
}