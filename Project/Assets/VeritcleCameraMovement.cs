using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeritcleCameraMovement : MonoBehaviour
{
    public float speedH = 2.0f;
    public float yaw = 0.0f;
    public float speedV = 2.0f;
    public float pitch = 0.0f;
    public float limitedpitch = 0.0f;

    // Update is called once per frame
    void Update()
    {

        pitch -= speedV * Input.GetAxis("Mouse Y");
        yaw += speedH * Input.GetAxis("Mouse X");

        limitedpitch = Mathf.Clamp(pitch, -89, 78);

        transform.eulerAngles = new Vector3(limitedpitch, yaw, 0);
    }
}
