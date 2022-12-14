using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int CurrentMagazineCapacity = 5;
    public int maxMagazineCapacity = 5;

    private bool isPlayerShooting = false;
    private bool isPlayerReload = false;

    private float reloadTime = 2f;

    private Animator animator;
    private Transform bulletSpawnTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletSpawnTransform = GameObject.FindGameObjectWithTag("Bullet Spawn").transform;  //Spawn point of the bullet
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPlayerReload)
        {
            isPlayerShooting = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isPlayerReload)
        {
            StartCoroutine(Reload());
        }

    }

    private void FixedUpdate()
    {
        animator.SetBool("IsShooting", isPlayerShooting);

        if (isPlayerShooting && CurrentMagazineCapacity > 0)
        {
            Shoot();
        }
        if (!isPlayerReload && CurrentMagazineCapacity <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        CurrentMagazineCapacity -= 1;
        isPlayerShooting = false;
        BulletSystem.instance.RefreshUI(CurrentMagazineCapacity);
        Vector3 bulletSpawnPoint = bulletSpawnTransform.position;
        Instantiate(bulletPrefab, bulletSpawnPoint, Quaternion.identity);
    }

    private IEnumerator Reload()
    {
        isPlayerReload = true;
        BulletSystem.instance.RefreshUI(0);
        yield return new WaitForSeconds(reloadTime);
        CurrentMagazineCapacity = maxMagazineCapacity;
        BulletSystem.instance.RefreshUI(CurrentMagazineCapacity);
        isPlayerReload = false;
    }
}
