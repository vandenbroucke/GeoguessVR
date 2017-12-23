using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class GMAPS
    {
        private const string API_KEY = "AIzaSyCRbK7pxxqNMDRmAWHHTeAyCdGT3bsOFiI";
        private static System.Random rnd = new System.Random();
        
        public static string getAPIURL(int siz, Location Loc, ViewAngle ViewAng)
        {
            return string.Format("maps.googleapis.com/maps/api/streetview?size={0}x{1}&location={2},{3}&heading={4}&pitch={5}&key={6}",
                siz,
                siz,
                Loc.Lat,
                Loc.Long,
                ViewAng.Heading,
                ViewAng.Pitch,
                API_KEY);
        }

     
        
        public static Location GetNewStreetViewCoordinates()
        {
            return Challenges[Random.Range(0,Challenges.Length-1)];
        }

        public class Location
        {
            public float Lat { get; set; }
            public float Long { get; set; }
            public string Name { get; set; }
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

        private static Location[] Challenges = new Location[] {
            new Location(){ Lat=51.2086317f,    Long=3.2242202f,    Name="BRUGES"},
            new Location(){ Lat=51.2184941f,    Long=2.9223244f,    Name="OSTEND" },
            new Location(){ Lat=51.5006867f,    Long =-0.1220094f,  Name="LONDON"},
            new Location(){ Lat=-34.6831994f,   Long=20.2342264f,   Name="SOUTH AFRICA"},
            new Location(){ Lat=40.4191773f,    Long=-3.6924593f,   Name="MADRID" },
            new Location(){ Lat=-21.8157096f,   Long=15.1873049f,   Name="NAMIBIE"},
            new Location(){ Lat=65.7353317f,    Long=-23.2051738f,  Name="ICELAND"},
            new Location(){ Lat=-18.1147732f,   Long=-65.7683255f,  Name="CHILI"},
            new Location(){ Lat=50.8216683f,   Long= 4.3948574f,  Name="BRUSSELS"},
            new Location(){ Lat=44.045413f,   Long= 3.3018454f,  Name="FRANCE"},
            new Location(){ Lat=41.8908217f,   Long= 12.4902783f,  Name="ROME"},
            //new Location(){ Lat=60.1657412f,   Long= 60.1657412f,  Name="HELSINKI"},
            new Location(){ Lat=-26.3795f,   Long= 153.101f,  Name="QUEENSLAND"},
            new Location(){ Lat=22.8547975f,   Long= 106.7239896f,  Name="CHINA"},
            new Location(){ Lat=55.7523464f,   Long= 37.6227359f,  Name="MOSCOW"},
            new Location(){ Lat=43.8055235f,   Long= 15.9634348f,  Name="CROATIA"},
            new Location(){ Lat=-0.141804f,   Long= 37.31496f,  Name="CROATIA"}
          
        };
    }
}