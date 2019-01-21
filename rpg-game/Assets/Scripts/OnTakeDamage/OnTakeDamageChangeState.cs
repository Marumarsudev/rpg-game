using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTakeDamageChangeState : OnTakeDamage
{
    private EnemyScript _enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        _enemyScript = GetComponent<EnemyScript>();
    }

    public override void CallOnTakeDamage()
    {
        _enemyScript.ChangeStateTo(States.defending);
    }
}
