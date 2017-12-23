using Assets;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayPointer : MonoBehaviour
{
    [SerializeField]
    FlatSphereTerrainFactory _globeFactory;
    [SerializeField]
    Transform _globeTransform;
    [SerializeField]
    public LineRenderer lineRenderer = null;
    [SerializeField]
    public OVRInput.Button joyPadClickButton = OVRInput.Button.PrimaryIndexTrigger;
    [SerializeField]
    public Transform trackingSpace = null;

    Image fillImg;

    int time, a;
    float x;
    public bool count;
    public string timeDisp;
    public static Vector2d pinpointLocation = new Vector2d(0, 0);
    public static bool hasGuessed = false;
    // Use this for initialization
    void Start()
    {
        time = 2;
        count = false;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update(); // Call before checking the input

        if (!hasGuessed && GeoguessVRGame._gameStarted) {
            Ray selectionRay = UpdateCastRayIfPossible();

            RaycastHit[] hits;
            hits = Physics.RaycastAll(selectionRay);
            for (int i = 0; i < hits.Length; i++)
            {
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    RaycastHit hit = hits[i];
                    GameObject pinpoint = GameObject.Find("pinpoint");
                    if (hit.collider.gameObject.name != "pinpoint")
                    {
                        if (pinpointLocation.x == 0 && pinpointLocation.y == 0)
                        {
                            Debug.Log("reset");
                            Vector3 cameraRelative = _globeTransform.InverseTransformPoint(hit.point);
                            pinpointLocation = Conversions.GeoFromGlobePosition(cameraRelative, _globeFactory.Radius);
                            //Debug.Log(pinpointLocation);
                        }
                        else
                        {
                            Debug.Log("draw");


                            var instance = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                            instance.transform.position = _globeTransform.TransformPoint(Conversions.GeoToWorldGlobePosition(pinpointLocation, _globeFactory.Radius));
                            instance.transform.localScale = Vector3.one * .01f;
                            instance.transform.SetParent(transform);
                            instance.name = "pinpoint";

                            Renderer rend = instance.GetComponent<Renderer>();
                            rend.material.color = new Color(255, 255, 255);

                            if (pinpoint != null)
                            {
                                Destroy(pinpoint);
                            }
                        }

                        GeoguessVRGame._scoreBackground.SetActive(true);

                        GeoguessVRGame._confirmLocationImage.SetActive(true);
                        lineRenderer.GetComponent<Renderer>().enabled = false;
                        GeoguessVRGame._confirmLocationText.SetActive(true);
                        count = true;
                        if (count)
                        {
                            fillImg = GeoguessVRGame._confirmLocationImage.GetComponent<Image>(); ;
                            timeDisp = time.ToString();
                            x += Time.deltaTime;
                            fillImg.fillAmount = x / time;
                            a = (int)x;
                            print(a);
                            switch (a)
                            {
                                case 0: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 2 second to confirm location"; break;
                                case 1: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 1 seconds to confirm location"; break;
                                //case 2: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 1 seconds to confirm location"; break;
                                //case 3: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 2 seconds to confirm location"; break;
                                //case 4: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 1 seconds to confirm location"; break;
                                //case 5: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 5 seconds to confirm location"; break;
                                //case 6: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 4 seconds to confirm location"; break;
                                //case 7: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 3 seconds to confirm location"; break;
                                //case 8: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 2 seconds to confirm location"; break;
                                //case 9: GeoguessVRGame._confirmLocationText.GetComponent<Text>().text = "Hold for 1 seconds to confirm location"; break;

                                default:
                                    ShowGuessScore();
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    pinpointLocation = new Vector2d(0, 0);
                    x = 0;
                    a = 0;
                    GeoguessVRGame._confirmLocationText.SetActive(false);
                    GeoguessVRGame._confirmLocationImage.SetActive(false);

                    lineRenderer.GetComponent<Renderer>().enabled = true;
                    GeoguessVRGame._scoreBackground.SetActive(false);

                    RaycastHit hit = hits[i];
                    GameObject pinpoint = GameObject.Find("pinpoint");

                    Vector3 cameraRelative = _globeTransform.InverseTransformPoint(hit.point);
                    Vector2d long_lat = Conversions.GeoFromGlobePosition(cameraRelative, _globeFactory.Radius);
                    //Debug.Log(long_lat);

                    Quaternion orientation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                    Vector3 localStartPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                    Matrix4x4 localToWorld = trackingSpace.localToWorldMatrix;

                    Vector3 worldStartPoint = localToWorld.MultiplyPoint(localStartPoint);

                    lineRenderer.SetPosition(0, worldStartPoint);
                    lineRenderer.SetPosition(1, hit.point);
                    lineRenderer.endWidth = .001f;

                    if (hit.collider.gameObject.name != "pinpoint")
                    {
                        var instance = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                        instance.transform.position = hit.point;
                        instance.transform.localScale = Vector3.one * 0.003f;
                        instance.transform.SetParent(transform);
                        instance.name = "pinpoint";

                        Renderer rend = instance.GetComponent<Renderer>();
                        rend.material.color = new Color(255, 255, 255);

                        if (pinpoint != null)
                        {
                            Destroy(pinpoint);
                        }
                    }
                }
            }

            if (hits.Length == 0)
            {
                GameObject pinpoint = GameObject.Find("pinpoint");

                if (pinpoint != null)
                {
                    Destroy(pinpoint);
                }
            }
        }        
    }

    void ShowGuessScore() {
        hasGuessed = true;
        count = false;

        GeoguessVRGame.madeGuess(DistanceTo(pinpointLocation.x, pinpointLocation.y, GeoguessVRGame.currentLocation.Lat, GeoguessVRGame.currentLocation.Long));




        /*GeoguessVRGame. _scoreText.SetActive(true);
          string text = @"U koos voor" + "\r\n"  + pinpointLocation.x.ToString("F2") + "° N " + pinpointLocation.y.ToString("F2") + "° W \r\n" +
                "De locatie was: " + GeoguessVRGame.currentLocation.Name.ToString() + "\r\n" +
                "De afstand is: " + DistanceTo(pinpointLocation.x, pinpointLocation.y, GeoguessVRGame.currentLocation.Lat, GeoguessVRGame.currentLocation.Long).ToString("F1") + "km";
        GeoguessVRGame._scoreText.GetComponent<Text>().text = text;*/
    }

    public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        switch (unit)
        {
            case 'K': //Kilometers -> default
                return dist * 1.609344;
            case 'N': //Nautical Miles 
                return dist * 0.8684;
            case 'M': //Miles
                return dist;
        }

        return dist;
    }

    Ray UpdateCastRayIfPossible()
    {
        if (trackingSpace != null)
        {
            Quaternion orientation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            Vector3 localStartPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Matrix4x4 localToWorld = trackingSpace.localToWorldMatrix;
            Vector3 worldStartPoint = localToWorld.MultiplyPoint(localStartPoint);
            Vector3 worldOrientation = localToWorld.MultiplyVector(orientation * Vector3.forward);

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, worldStartPoint);
                lineRenderer.SetPosition(1, worldStartPoint + worldOrientation * 500);
            }

            return new Ray(worldStartPoint, worldOrientation);
        }
        return new Ray();
    }
}
