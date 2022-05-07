using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pistol : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;

    void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {

            Shoot();

        }

    }

    void Shoot()
    {

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if(target !=null)
            {

                target.Takedamage(damage);

            }
        }

    }
}