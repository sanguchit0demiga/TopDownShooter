using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private void Start()
    {
      
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
       
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}