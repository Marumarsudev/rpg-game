using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;

    public List<Transform> spawnPoints = new List<Transform>();

    private List<GameObject> enemies = new List<GameObject>();

    private int enemyID = 0;

    // Start is called before the first frame update
    void Update()
    {
        if(enemies.Count < 4)
        {
            SpawnEnemy();
        }
    }

    public void AddToList(GameObject gO)
    {
        enemies.Add(gO);
    }

    public void RemoveFromList(GameObject gO)
    {
        enemies.Remove(gO);
    }

    private void SpawnEnemy()
    {

        Vector3 spoint = spawnPoints[Mathf.RoundToInt(Random.Range(0, spawnPoints.Count))].position;

        if(!Physics2D.CircleCast(spoint, 1.5f, Vector2.zero))
        {
            GameObject gO = Instantiate(enemy, spoint, Quaternion.identity);
            gO.name = "Enemy " + enemyID.ToString();
            enemyID++;
            enemies.Add(gO);
        }
    }

}
