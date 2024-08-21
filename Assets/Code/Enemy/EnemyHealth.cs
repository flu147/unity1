using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    private EnemySpawner spawner;

    void Start()
    {
        // Find the spawner in the scene
        spawner = FindObjectOfType<EnemySpawner>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a player bullet
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Retrieve the Bullet component to get damage info
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(collision.gameObject); // Destroy the bullet on impact
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Notify the spawner to decrease the enemy count
        if (spawner != null)
        {
            spawner.DecreaseEnemyCount();
        }
        Destroy(gameObject); // Destroy the enemy
    }
}
