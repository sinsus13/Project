using UnityEngine;

public class Rifle : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    public bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject bulletHoleGraphic, muzzleFlash;
    public ParticleSystem dirt;
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        if(bulletsLeft == 0)
        {

            Reload();

        }

    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            
        }
        
    }
    void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {


            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Target>().Takedamage(damage);

            //Graphics
            if (rayHit.collider.CompareTag("Wall"))
            {

                GameObject WallHole = Instantiate(bulletHoleGraphic, rayHit.point + new Vector3(0.01f, 0.01f, 0.01f), Quaternion.LookRotation(rayHit.normal));
                Destroy(WallHole, 3f);
                ParticleSystem WallDirt = Instantiate(dirt, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                Destroy(WallDirt, 1f);

            }

            if (rayHit.collider.CompareTag("Ground"))
            {

                ParticleSystem GroundDirt = Instantiate(dirt, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                Destroy(GroundDirt, 1f);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

        muzzleFlash.SetActive(true);
        //Invoke("RemoveMuzzleFlash", timeBetweenShots);

    }
    void RemoveMuzzleFlash()
    {

        muzzleFlash.SetActive(false);

    }
    void ResetShot()
    {
        readyToShoot = true;
        
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        
    }
}
