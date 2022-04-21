using UnityEngine;

public class Sniper : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        void shoot()
        {
            muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {

                    target.Takedamage(damage);

                }

            }

        }

        if (Input.GetButtonDown("Fire1"))
        {

            shoot();

        }
    }
}
