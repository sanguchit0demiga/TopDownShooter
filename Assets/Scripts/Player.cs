using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    public float speed = 5f;
    public GameObject bullet;
    public float bulletSpeed;
    public float cadenciaDisparo;
    public int health;
    public HealthBar healthbar;
    public Transform[] spawner;
    private bool shooting;
    private Coroutine shootCoroutine;
    private int ActiveSpawners = 2;
    private Coroutine powerUpShootCoroutine;
    void Start()
    {
        if (healthbar != null)
        {
            healthbar.maxHealth = health;
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }
    }
        private void Update()
    {
     
        Vector3 movimiento = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
        transform.Translate(movimiento, Space.World);

        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            shootCoroutine = StartCoroutine(RafagaDisparo());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shooting = false;
            if (shootCoroutine != null)
                StopCoroutine(shootCoroutine);
        }
    }


    IEnumerator RafagaDisparo()
    {
        while (shooting)
        {
            Shoot();
            yield return new WaitForSeconds(cadenciaDisparo);
        }
    }

    void Shoot()
    {
        for (int i = 0; i < ActiveSpawners && i < spawner.Length; i++)
        {
            Transform punto = spawner[i];
            GameObject bala = Instantiate(bullet, punto.position, punto.rotation);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = punto.forward * bulletSpeed;
            }
            Destroy(bala, 3f);
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.PlayerMovement.Enable();
        controls.PlayerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.PlayerMovement.Disable();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthbar != null)
        {
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }

      
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletEnemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Heart"))
        {
            IncreaseHealth(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Bolt"))
        {
            ActivateExtraSpawner();
            Destroy(other.gameObject);
        }
    }
    void IncreaseHealth(int cantidad)
    {
        health += cantidad;

        if (health > healthbar.maxHealth)
            health = (int)healthbar.maxHealth;

        healthbar.currentHealth = health;
        healthbar.UpdateBar();
    }
   void ActivateExtraSpawner()
    {
        if (powerUpShootCoroutine != null)
        {
            StopCoroutine(powerUpShootCoroutine);
        }

        ActiveSpawners = Mathf.Min(spawner.Length, ActiveSpawners + 2);
        powerUpShootCoroutine = StartCoroutine(DesactivarSpawnersExtraDespuesDeTiempo(7f));
    }
    IEnumerator DesactivarSpawnersExtraDespuesDeTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        ActiveSpawners = 2;
    }
}