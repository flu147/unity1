using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;   // Speed of the bullet
    public float damage = 10f; // Damage dealt by the bullet
    public float lifetime = 5f; // Lifetime of the bullet in seconds

    private Vector3 direction; // Direction of the bullet

    void Start()
    {
        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized; // Normalize the direction
    }

    void Update()
    {
        // Move the bullet in the set direction
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits an enemy or other target
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the bullet upon hitting an enemy
            Destroy(gameObject);
        }
        // Optionally, destroy the bullet if it hits anything else or add other conditions here
    }
}
