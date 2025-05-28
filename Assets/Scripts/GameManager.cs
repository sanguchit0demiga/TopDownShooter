using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int enemigosMuertos = 0;
    public int cantidadParaJefe = 10;
    public TextMeshProUGUI enemiesRemainingText;
    public int enemigosRestantes;
    public GameObject jefePrefab;
    public Transform puntoSpawnJefe;
    public EnemySpawner enemySpawner;

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

    public void EnemigoMuerto(Enemy enemigo)
    {
        if (enemigo.esJefe)
        {
            Win();
            return;
        }

        enemigosMuertos++;
        enemigosRestantes = Mathf.Max(0, cantidadParaJefe - enemigosMuertos);

        if (enemiesRemainingText != null)
        {
            enemiesRemainingText.text = "Enemies Remaining: " + enemigosRestantes;
        }

        Debug.Log("Enemigos muertos: " + enemigosMuertos);

        if (enemigosMuertos >= cantidadParaJefe && !jefeAparecio)
        {
            jefeAparecio = true;

            if (enemySpawner != null)
            {
                enemySpawner.DetenerSpawner();
            }

            SpawnJefe();
        }
    }

    void SpawnJefe()
    {
        if (jefePrefab != null && puntoSpawnJefe != null)
        {
            GameObject jefe = Instantiate(jefePrefab, puntoSpawnJefe.position, Quaternion.identity);
            Enemy enemigoJefe = jefe.GetComponent<Enemy>();
            if (enemigoJefe != null)
            {
                enemigoJefe.esJefe = true;
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Menu 1");
    }

    public void Win()
    {
        SceneManager.LoadScene("Menu 2");
    }
}
