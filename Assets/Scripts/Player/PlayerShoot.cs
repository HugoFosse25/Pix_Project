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
        bulletSpawnTransform = GameObject.FindGameObjectWithTag("Bullet Spawn").transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPlayerShooting = true;
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
            Vector3 playerPosition = bulletSpawnTransform.position;
            Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
            CurrentMagazineCapacity -= 1;
            isPlayerShooting = false;
    }

    private IEnumerator Reload()
    {
        isPlayerReload = true;
        yield return new WaitForSeconds(reloadTime);
        CurrentMagazineCapacity = maxMagazineCapacity;
        isPlayerReload = false;
    }
}
