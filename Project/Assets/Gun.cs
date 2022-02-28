using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera tpsCam;

    // Update is called once per frame
    void Update()
    {
        void shoot()
        {

            RaycastHit hit;
            if (Physics.Raycast(tpsCam.transform.position, tpsCam.transform.forward, out hit, range))
            {

                Debug.Log(hit.transform.name);

            }

        }

        if (Input.GetButtonDown("Fire1"))
        {

            shoot ();

        }
    }
}
