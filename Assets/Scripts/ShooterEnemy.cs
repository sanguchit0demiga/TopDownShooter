using UnityEngine;
using UnityEngine.Rendering;

public class ShooterEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate;
    private float fireTimer;
    
    private Transform player;
    public float bulletSpeed = 10f;
    protected new void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        base.Start();
        fireTimer = fireRate;
    }

    void Update()
    {
        if (player == null) return;

        
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

       
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Shoot(direction); 
            fireTimer = fireRate;
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void Shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        bullet.tag = "BulletEnemy";

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        Destroy(bullet, 5f);
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
  