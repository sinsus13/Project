using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraMovement : MonoBehaviour
{
    public float speedH = 2.0f;
    public float yaw = 0.0f;
    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {

        yaw += speedH * Input.GetAxis("Mouse X");


        rb.transform.eulerAngles = new Vector3(0, yaw, 0);
    }

}