using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Random r = new Random();

    private Transform _player;

    public EnemyScript enemy;

    private List<EnemyScript> SpawnedEnemies = new List<EnemyScript>();

    private Vector3 _spawnPoint;

    private void Start()
    {
        _player = FindObjectOfType<ActorMoveScript>().transform;
        for(int i = 0; i < 5; i++)
        {
            _spawnPoint = new Vector3(_player.position.x + Random.Range(-5, 5), _player.position.y + Random.Range(-5, 5), 0);
            SpawnedEnemies.Add(Instantiate(enemy, _spawnPoint, Quaternion.identity));
        }

        SpawnedEnemies.ForEach(e => 
        {
            e.CheckOtherEnemies();
        });
    }

    public void SpawnEnemy(EnemyScript e)
    {
        SpawnedEnemies.Remove(e);
        _spawnPoint = new Vector3(_player.position.x + Random.Range(-5, 5), _player.position.y + Random.Range(-5, 5), 0);
        SpawnedEnemies.Add(Instantiate(enemy, _spawnPoint, Quaternion.identity));
        SpawnedEnemies.ForEach(enemy => 
        {
            enemy.CheckOtherEnemies();
        });
    }
}
