using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public float limitedpitch = 0.0f;

    // Update is called once per frame
    void Update()
    {

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedV * Input.GetAxis("Mouse Y");

        limitedpitch = Mathf.Clamp(pitch, -89, 78);

        rb.transform.eulerAngles = new Vector3(-limitedpitch, yaw, 0);
    }

}