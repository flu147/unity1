using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float damage = 10f;
    public float fireRate = 0.5f;
    public float aimSlowdownFactor = 0.7f;  // 30% time slowdown when aiming
    public Camera playerCamera;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float nextFireTime = 0f;

    void Update()
    {
        AimAndShoot();
    }

    void AimAndShoot()
    {
        bool isAiming = Input.GetMouseButton(1); // Right mouse button to aim

        // Slow down time when aiming
        Time.timeScale = isAiming ? aimSlowdownFactor : 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // Adjust physics update rate to match time scale

        if (Time.time >= nextFireTime && Input.GetMouseButton(0))  // Left mouse button to fire
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Calculate shoot direction
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        bulletScript.SetDirection(ray.direction);
    }
}
