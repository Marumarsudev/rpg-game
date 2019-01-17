using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Random r = new Random();

    public Transform enemy;

    private void Start()
    {
        Instantiate(enemy, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
    }
}
