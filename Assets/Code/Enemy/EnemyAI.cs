using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float shootingRange = 10f;
    public float fireRate = 1f;
    public float damage = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform playerTransform;  // Cache the Transform
    private float nextFireTime = 0f;

    void Start()
    {
        // Find the player GameObject using the tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player GameObject with tag 'Player' not found!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();

            if (Vector3.Distance(transform.position, playerTransform.position) <= shootingRange)
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Calculate shoot direction
        Vector3 shootDirection = (playerTransform.position - firePoint.position).normalized;

        // Set bullet direction
        bulletScript.SetDirection(shootDirection);
    }
}
