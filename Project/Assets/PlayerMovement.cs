using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float Force = 500f;
    public float Jump = 500f;
    public float Rotate = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public bool Floored = true;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("a") || (Input.GetKey("left")))
        {

            rb.AddRelativeForce(0, 0, -Force * Time.deltaTime, ForceMode.VelocityChange);

        }

        if (Input.GetKey("d") || (Input.GetKey("right")))
        {

            rb.AddRelativeForce(0, 0, Force * Time.deltaTime, ForceMode.VelocityChange);

        }

        if (Input.GetKey("w") || (Input.GetKey("up")))
        {

            rb.AddRelativeForce(-Force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        }

        if (Input.GetKey("s") || (Input.GetKey("down")))
        {

            rb.AddRelativeForce(Force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        }

        if (Input.GetKeyDown("space") && Floored)
        {


            rb.AddRelativeForce(new Vector3(0, Jump, 0), ForceMode.Impulse);
            Floored = false;

        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown("space"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedV * Input.GetAxis("Mouse Y");
        rb.transform.eulerAngles = new Vector3(0, yaw, -pitch);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {

            Floored = true;

        }

    }

}
