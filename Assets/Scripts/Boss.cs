using UnityEngine;

public class Boss : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;
    private float shootTimer;

    protected override void Update()
    {
        base.Update();

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direccionAlJugador = player.transform.position - transform.position;
            direccionAlJugador.y = 0; 
            transform.rotation = Quaternion.LookRotation(direccionAlJugador);
        }

      
      
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

}
