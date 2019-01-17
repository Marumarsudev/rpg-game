﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMoveScript : MonoBehaviour
{
    [SerializeField]
    private float _actorSpeed = 1;

    private Rigidbody2D _actorBody;

    // Start is called before the first frame update
    void Start()
    {
        _actorBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _actorBody.velocity = InputManager.GetMoveDirection() * _actorSpeed;
    }
}
