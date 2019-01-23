﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Managers
    private InputManager InputManager;

    //Components
    private Rigidbody2D body;
    private AnimationManager animationManager;
    //private Animator animator;

    //Properties
    [SerializeField]
    private float speed = 1000.0f;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    // Start is called before the first frame update
    void Awake()
    {
        InputManager = FindObjectOfType<InputManager>();
        body = GetComponent<Rigidbody2D>();
        animationManager = GetComponent<AnimationManager>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = InputManager.GetMovement();
        lookDirection = Vector2.ClampMagnitude((Vector2)InputManager.GetMousePosition() - body.position, 1.0f);

        if(InputManager.GetAttack())
        {
            animationManager.SetTrigger("Attack");
        }
        else
        {
            animationManager.ResetTrigger("Attack");
        }

        animationManager.SetFloat("speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y));
    }

    void FixedUpdate()
    {
        Move(moveDirection, speed);
        Turn(lookDirection);
    }

    private void Move(Vector2 dir, float speed)
    {
        body.velocity = dir * speed * Time.deltaTime;
    }

    private void Turn(Vector2 dir)
    {
        body.transform.up = lookDirection;
    }
}