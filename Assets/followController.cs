using Assets;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class followController : MonoBehaviour {
    [SerializeField]
    FlatSphereTerrainFactory _globeFactory;
    [SerializeField]
    Transform _globeTransform;
    [SerializeField]
    public Transform trackingSpace = null;

    private Material materialColored;

    float currentRotation = 0;


    // Use this for initialization
    void Start() {
        //create a new material
        materialColored = new Material(Shader.Find("Diffuse"));
        materialColored.color = new Color(0.5f, 1, 1);
    }
    // Update is called once per frame
    void Update() {
        Matrix4x4 localToWorld = trackingSpace.localToWorldMatrix;
        Quaternion orientation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        Vector3 worldOrientation = localToWorld.MultiplyVector(orientation * Vector3.forward);
        
        Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 worldStartPoint = localToWorld.MultiplyPoint(position);
        Vector3 maxPoint = worldStartPoint + worldOrientation * 500;

        float distance = 0.065f + (transform.localScale.x * 1000);

        transform.localPosition = Vector3.MoveTowards(worldStartPoint + worldOrientation*0.01f, maxPoint, distance);
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            currentRotation += 1.3f;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            currentRotation -= 1.3f;
        }

        Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        rotation *= Quaternion.Euler(0, currentRotation, 0);
        transform.localRotation = rotation;

        OVRInput.Update(); // Call before checking the input
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp)) {
            if(transform.localScale.x < 0.0004f)
                transform.localScale += new Vector3(0.000002f, 0.000002f, 0.000002f);
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown)) {
            if (transform.localScale.x > 0.0001f) {
                transform.localScale -= new Vector3(0.000002f, 0.000002f, 0.000002f);
            }
        }


        

        if (RayPointer.hasGuessed) {
            GameObject pinpoint = GameObject.Find("pinpoint");

            var instance = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            instance.transform.position = _globeTransform.TransformPoint(Conversions.GeoToWorldGlobePosition(RayPointer.pinpointLocation, _globeFactory.Radius));
            instance.transform.localScale = Vector3.one * .008f;
            instance.transform.LookAt(_globeTransform);
            instance.transform.localRotation *= Quaternion.Euler(90, 0, 0);
            instance.transform.SetParent(transform);
            instance.name = "pinpoint";

            Renderer rend = instance.GetComponent<Renderer>();
            rend.material.color = new Color(20, 0, 255);
            if (pinpoint != null)
            {
                Destroy(pinpoint);
            }

            GameObject result_pinpoint = GameObject.Find("result_pinpoint");

            var result_instance = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            Vector2d currentlocation = new Vector2d(GeoguessVRGame.currentLocation.Lat, GeoguessVRGame.currentLocation.Long);
            result_instance.transform.position = _globeTransform.TransformPoint(Conversions.GeoToWorldGlobePosition(currentlocation, _globeFactory.Radius));
            result_instance.transform.localScale = Vector3.one * .008f;
            result_instance.transform.LookAt(_globeTransform);
            result_instance.transform.localRotation *= Quaternion.Euler(90, 0, 0);            
            result_instance.transform.SetParent(transform);
            result_instance.name = "result_pinpoint";
            
            Renderer rend1 = result_instance.GetComponent<Renderer>();
            rend1.material.color = new Color(0, 255, 20);
            if (result_pinpoint != null)
            {
                Destroy(result_pinpoint);
            }
        }
        else
        {
            GameObject pinpoint = GameObject.Find("result_pinpoint");
            if (pinpoint != null)
            {
                Destroy(pinpoint);
            }
        }
    }

    public Vector3 Interp(Vector3 st, Vector3 en, Vector3 ctrl, float t)
    {
        float d = 1f - t;
        return d * d * st + 2f * d * t * ctrl + t * t * en;
    }

    public Vector3 Velocity(Vector3 st, Vector3 en, Vector3 ctrl, float t)
    {
        return (2f * st - 4f * ctrl + 2f * en) * t + 2f * ctrl - 2f * st;
    }


}
