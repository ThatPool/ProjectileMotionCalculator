using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomInOut : MonoBehaviour
{
    public float MouseZoomSpeed = 15.0f;
    public float TouchZoomSpeed = 0.1f;
    public float ZoomMinBound = 15f;
    public float ZoomMaxBound = 60f;
    
    private Camera cam;
    //private Camera noPostCam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        //noPostCam = this.transform.GetChild(0).GetComponent<Camera>();
        cam.fieldOfView = ZoomMaxBound;
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {

                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {

            float scroll = -Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }



        if (cam.fieldOfView < ZoomMinBound)
        {
            cam.fieldOfView = ZoomMinBound;
        }
        else
        if (cam.fieldOfView > ZoomMaxBound)
        {
            cam.fieldOfView = ZoomMaxBound;
        }

        //noPostCam.fieldOfView = cam.fieldOfView;
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {

        cam.fieldOfView += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }

    /*public void ResetZoom()
    {
        LeanTween.value(cam.gameObject, cam.fieldOfView, ZoomMaxBound, 0.3f).setOnUpdate((float flt) => {
            cam.fieldOfView = flt;
        });
    }*/
}
