using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;

    public List<Transform> spawnPoints = new List<Transform>();

    private HashSet<GameObject> enemies = new HashSet<GameObject>();

    private Text waveText;

    private int spawnAmount = 1;

    private bool spawning = false;

    private int enemyID = 0;

    void Awake()
    {
        waveText = GameObject.FindGameObjectWithTag("WaveText").GetComponent<Text>();
        LeanTween.alphaText(waveText.rectTransform, 0, 0f);
    }

    // Start is called before the first frame update
    void Update()
    {
        if(enemies.Count == 0 && !spawning)
        {
            spawning = true;
            waveText.text = "Wave " + spawnAmount.ToString();
            LeanTween.alphaText(waveText.rectTransform, 1, 1f).setOnComplete(() => {
                LeanTween.alphaText(waveText.rectTransform, 0, 1f).setDelay(0.5f);
            });
            SpawnWave();
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

    private async void SpawnWave()
    {
        while(enemies.Count < spawnAmount)
        {
            Vector3 spoint = spawnPoints[Mathf.RoundToInt(Random.Range(0, spawnPoints.Count))].position;

            if(!Physics2D.CircleCast(spoint, 1f, Vector2.zero))
            {
                GameObject gO = Instantiate(enemy, spoint, Quaternion.identity);
                gO.name = "Enemy " + enemyID.ToString();
                enemyID++;
                enemies.Add(gO);
            }

            await Task.Delay(250);
        }
        spawnAmount++;
        spawning = false;
    }

}
