using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStayStill : MonoBehaviour
{

    private Vector2 stayHere;

    // Start is called before the first frame update
    void Start()
    {
        stayHere = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = stayHere;
    }
}
