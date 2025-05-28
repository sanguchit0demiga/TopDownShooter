using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float speed = 2f;
    public PowerUps dropConfig;
    public bool isDead = false;
    private Renderer rend;
    private Color originalColor;
    public bool esJefe = false;


    public void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }


    protected virtual void Update()
    {
        Move();
    }


    public virtual void Move()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; 

        currentHealth -= damage;

        if (rend != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            isDead = true;  
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f); 
        rend.material.color = originalColor;
    }
    public void Die()
    {
        TryDropItem();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemigoMuerto(this);  
        }
        else
        {
            Debug.LogWarning("GameManager.Instance es null");
        }

        Destroy(gameObject);
    }
    void TryDropItem()
    {
        if (dropConfig == null)
        {
            Debug.LogWarning("dropConfig NO está asignado");
            return;
        }
        if (dropConfig.posiblesDrops == null || dropConfig.posiblesDrops.Length == 0)
        {
            Debug.LogWarning("No hay posiblesDrops en dropConfig");
            return;
        }

        Debug.Log("Intentando dropear ítems...");

        foreach (var drop in dropConfig.posiblesDrops)
        {
            if (drop.objeto == null)
            {
                Debug.LogWarning("Objeto en drop es null");
                continue;
            }

            float randomValue = Random.Range(0f, 1f);
            Debug.Log($"Probabilidad para {drop.objeto.name}: {drop.probabilidad}, random: {randomValue}");

            if (randomValue <= drop.probabilidad)
            {
                Instantiate(drop.objeto, transform.position, Quaternion.identity);
                Debug.Log($"{drop.objeto.name} dropeado!");
            }
            else
            {
                Debug.Log($"{drop.objeto.name} NO dropeado.");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}















