using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class GeoguessVRGame : MonoBehaviour
    {
        void Start()
        {
            GMAPS.UpdateToGivenPosition(
                GMAPS.GetNewStreetViewCoordinates());
            
        }

        

        // Update is called once per frame
        void Update()
        {
            OVRInput.Update(); // Call before checking the input

            
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                GMAPS.UpdateToGivenPosition(
                 GMAPS.GetNewStreetViewCoordinates());
            }

        }

    }
}