using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float Force = 500f;
    public float Jump = 500f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
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

    }

    void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.gameObject.CompareTag("Ground"))
        {

            Floored = true;

        }

    }

}
