using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int enemigosMuertos = 0;
    public int cantidadParaJefe = 10;
    public TextMeshProUGUI enemiesRemainingText;
    public int enemigosRestantes;
    public GameObject jefePrefab;
    public Transform puntoSpawnJefe;

    private bool jefeAparecio = false;

    void Awake()
    {
      
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemigoMuerto()
    {
        enemigosMuertos++;
        enemigosRestantes = Mathf.Max(0, cantidadParaJefe - enemigosMuertos);

        enemiesRemainingText.text = "Enemies Remaining: " + enemigosRestantes;

        Debug.Log("Enemigos muertos: " + enemigosMuertos);

        if (enemigosMuertos >= cantidadParaJefe && !jefeAparecio)
        {
            jefeAparecio = true;
            SpawnJefe();
        }
    }

    void SpawnJefe()
    {
        if (jefePrefab != null && puntoSpawnJefe != null)
        {
            Instantiate(jefePrefab, puntoSpawnJefe.position, Quaternion.identity);
         
        }
        
    }
}
