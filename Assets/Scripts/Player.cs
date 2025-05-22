using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    public float speed = 5f;
    public GameObject bullet;
    public float bulletSpeed;
    public float cadenciaDisparo;
    public float tiempoUltimoDisparo;

    public Transform[] spawner;

    private void Update()
    {
     
        Vector3 movimiento = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
        transform.Translate(movimiento, Space.World);

        if (Input.GetMouseButton(0) && Time.time > tiempoUltimoDisparo + cadenciaDisparo)
        {
            Shoot();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void Shoot()
    {
        foreach (Transform punto in spawner)
        {
            
            GameObject bala = Instantiate(bullet, punto.position, punto.rotation);

         
            Rigidbody rb = bala.GetComponent<Rigidbody>();

           
            rb.velocity = punto.forward * bulletSpeed;

            
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

   
}
