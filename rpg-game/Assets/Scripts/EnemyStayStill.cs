using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStayStill : MonoBehaviour
{
    private Transform _body;
    private Vector3 _currentPos;

    // Start is called before the first frame update
    void Start()
    {
        _body = this.gameObject.GetComponent<Transform>();
        _currentPos = _body.position;
    }

    // Update is called once per frame
    void Update()
    {
        _body.position = _currentPos;
    }
}
