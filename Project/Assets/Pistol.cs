using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public int maxAmmo = 20;
    private int currentAmmo;
    public float reloadTime = 5;
    private bool isReloading = false;
    public GameObject bulletHole;
    public GameObject bulletImpact;
    public float impactForce = 100f;

    private void Start()
    {

        currentAmmo = maxAmmo;

    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {

            return;

        }

        if (currentAmmo <= 0)
        {

            StartCoroutine (Reload());
            return;
        }

        IEnumerator Reload ()
        {
            isReloading = true;
            Debug.Log("Reloading boi");
            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            isReloading = false;
        }

        void shoot()
        {

            currentAmmo--;
            muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {

                    target.Takedamage(damage);

                }

                if (hit.rigidbody != null)
                {

                    hit.rigidbody.AddForce(-hit.normal * impactForce);

                }

                if (hit.transform.tag == "Wall")
                {

                    Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                }

                if (hit.transform.tag == "Ground")
                {

                    Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));

                }

            }

        }

        if (Input.GetButtonDown("Fire1"))
        {

            shoot ();

        }
    }
}
