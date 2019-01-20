using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStayStill : MonoBehaviour
{
    private HealthScript health;

    private Rigidbody2D _player;
    private Rigidbody2D _body;
    private Vector3 _currentPos;

    // Start is called before the first frame update
    void Start()
    {
        _body = this.gameObject.GetComponent<Rigidbody2D>();
        health = this.gameObject.GetComponent<HealthScript>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health > 0 && Vector2.Distance(_player.position, _body.position) > 0.5)
        {
            _body.velocity = Vector2.ClampMagnitude((_player.position - _body.position), 1) * 1.5f;
        }
        else
        {
            _body.velocity = Vector3.zero;
        }

    }
}
