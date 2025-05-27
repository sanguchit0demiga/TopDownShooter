using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float speed = 2f;

   public void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
    }

   
    public virtual void Move()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playerbullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
