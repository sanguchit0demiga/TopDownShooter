using UnityEngine;

public class KamikazeEnemy : Enemy
{
    private Transform playerTransform;

    protected new void Start()
    {
        base.Start();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
       
    }
    void Update()
    {

        if (playerTransform != null)
        {
        
            Vector3 targetPos = playerTransform.position;
            targetPos.y = 0f;

            Vector3 myPos = transform.position;
            myPos.y = 0f;

            Vector3 direction = (targetPos - myPos).normalized;
            transform.position += direction * speed * 1.5f * Time.deltaTime;

           
            Vector3 pos = transform.position;
            pos.y = 0f;
            transform.position = pos;
        }
        else
        {
            base.Move(); 
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<Player>().TakeDamage(2);
            Die();
        }

       
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}