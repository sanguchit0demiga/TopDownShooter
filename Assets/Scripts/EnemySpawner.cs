using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public Transform[] spawnPoints;
    public float tiempoEntreSpawns = 3f;

    private float timer;
    private bool CanSpawn = true;

    void Update()
    {
        if (!CanSpawn) return;

        timer += Time.deltaTime;
        if (timer >= tiempoEntreSpawns)
        {
            SpawnearEnemigos();
            timer = 0f;
        }
    }

    void SpawnearEnemigos()
    {
        if (enemyPrefabs.Count == 0 || spawnPoints.Length < 2) return;

       
        int index1 = Random.Range(0, spawnPoints.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPoints.Length);
        } while (index2 == index1);

       
        int enemy1 = Random.Range(0, enemyPrefabs.Count);
        int enemy2 = Random.Range(0, enemyPrefabs.Count);

        Instantiate(enemyPrefabs[enemy1], spawnPoints[index1].position, Quaternion.identity);
        Instantiate(enemyPrefabs[enemy2], spawnPoints[index2].position, Quaternion.identity);
    }

    public void DetenerSpawner()
    {
        CanSpawn = false;
    }
}
