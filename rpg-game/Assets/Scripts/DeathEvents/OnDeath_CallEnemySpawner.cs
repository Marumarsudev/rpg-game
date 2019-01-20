using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath_CallEnemySpawner : OnDeath
{
    public override void CallOnDeath()
    {
        GameObject.FindObjectOfType<EnemySpawner>().SpawnEnemy(this.GetComponent<EnemyScript>());
    }
}
