using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float RotationSensitivity = 8f;

    //public float MinXAxis = 0;
    //public float MaxXAxis = 45;

    //public float MinYAxis = -90;
    //public float MaxYAxis = 90;
    public bool allowUpDownCamMove = false;
    public Vector2 ClampXAxis = new Vector2(-15, 45);
    //public Vector2 ClampYAxis = new Vector2(-90, 90);
    private float initXAngle;

    float Yaxis;
    float Xaxis;
    Vector3 targetRotation = new Vector3(0, 0);
    bool moving = false;

    private void OnEnable()
    {
        Yaxis = this.transform.eulerAngles.y;
        initXAngle = this.transform.eulerAngles.x;
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 1)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }

        if (moving)
        {
            //Yaxis = Mathf.Clamp(Yaxis + Input.GetAxis("Mouse X") * RotationSensitivity, ClampYAxis.x, ClampYAxis.y);
            Xaxis = allowUpDownCamMove? Mathf.Clamp(Xaxis - (Input.GetAxis("Mouse Y") * RotationSensitivity), ClampXAxis.x, ClampXAxis.y) : initXAngle;
            Yaxis = Yaxis + Input.GetAxis("Mouse X") * RotationSensitivity;
            //Xaxis = Xaxis - Input.GetAxis("Mouse Y") * RotationSensitivity;

            //Mathf.Clamp(Xaxis, MinXAxis, MaxXAxis);
            targetRotation = new Vector3(Xaxis, Yaxis);

            transform.eulerAngles = targetRotation;

        }
        SyncTextMeshPro();
    }

    private void SyncTextMeshPro()
    {
        foreach (var item in FindObjectsOfType<TextMeshPro>())
        {
            item.transform.eulerAngles = transform.eulerAngles;
        }
    }

    /*public void ResetCamRotation()
    {
        print(Vector3.Distance(targetRotation, new Vector3(0, 0)));
        Yaxis = 0;
        Xaxis = 0;
        //LeanTween.rotate(this.gameObject, new Vector3(0, 0), 1);
        LeanTween.rotate(this.gameObject, new Vector3(0, 0), 0.3f);
    }*/
}
