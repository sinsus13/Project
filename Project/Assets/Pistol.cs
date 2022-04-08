using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public int maxAmmo = 20;
    private int currentAmmo;
    public float reloadTime = 5;
    private bool isReloading = false;
    public GameObject bulletHole;
    public GameObject bulletImpact;
    public float impactForce = 100f;

    public bool AddBulletSpread = true;
    public Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    public Transform BulletSpawnPoint;
    public Transform BulletTrailSpawnPoint;
    public float ShootDelay = 0.5f;
    public LayerMask Mask;
    public Animator Animator;
    public float LastShootTime;
    public bool Shoot = false;
    public void Awake()
    {

        Animator = GetComponent<Animator>();

    }
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
            Animator.SetBool("Reload", true);
            isReloading = true;
            Debug.Log("Reloading boi");
            yield return new WaitForSeconds(reloadTime);
            Animator.SetBool("Reload", false);
            currentAmmo = maxAmmo;
            isReloading = false;
        }

        

        if (Input.GetMouseButtonDown(0))
        {

            shoot ();
            Animator.SetBool("IsShooting", true);
        }

        if (Input.GetMouseButtonDown(1))
        {

            Animator.SetBool("Aim", true);
            AddBulletSpread = false;
        }

        if (Input.GetMouseButtonUp(1))
        {

            Animator.SetBool("Aim", false);
            AddBulletSpread = true;
        }

    }

    IEnumerator ShootingBool()
    {

        yield return new WaitForSeconds(ShootDelay);
        Shoot = false;
        Animator.SetBool("IsShooting", false);
    }

    private Vector3 GetDirection()
    {

        Vector3 direction = transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                    Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                    Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                    Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
                    );

            direction.Normalize();
        }
        return direction;
    }

    public void shoot()
    {


        if (LastShootTime + ShootDelay < Time.time)
        {

            currentAmmo--;
            muzzleFlash.Play();
            Shoot = true;
            StartCoroutine(ShootingBool());
            Vector3 direction = GetDirection();
            RaycastHit hit;
            if (Physics.Raycast(BulletSpawnPoint.position, direction, out hit, float.MaxValue, Mask))
            {


                LastShootTime = Time.time;

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

                    Instantiate(bulletHole, hit.point + new Vector3(0.001f, 0.001f, 0.001f), Quaternion.LookRotation(hit.normal));

                }

                if (hit.transform.tag == "Ground")
                {

                    Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));

                }

            }
        }
    }

    

    public IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {

        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {


            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;

        }
        Trail.transform.position = hit.point;

        Destroy(Trail.gameObject, Trail.time);

    }
}
