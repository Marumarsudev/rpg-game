using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMngr : MonoBehaviour
{

    private KeyCode _moveKeyUp = KeyCode.W;
    private KeyCode _moveKeyDown = KeyCode.S;
    private KeyCode _moveKeyLeft = KeyCode.A;
    private KeyCode _moveKeyRight = KeyCode.D;

    private KeyCode _attackKeyUp = KeyCode.UpArrow;
    private KeyCode _attackKeyDown = KeyCode.DownArrow;
    private KeyCode _attackKeyLeft = KeyCode.LeftArrow;
    private KeyCode _attackKeyRight = KeyCode.RightArrow;

    private KeyCode _attackKey = KeyCode.Space;

    private Vector2 _moveDirection = new Vector2();
    private Vector2 _attackDirection = new Vector2();

    public bool GetAttackKey()
    {
        return Input.GetKey(_attackKey);
    }

    public bool GetAttackKeyDown()
    {
        return Input.GetKeyDown(_attackKey);
    }

    public Vector2 GetMoveDirection()
    {
        _moveDirection = Vector2.zero;

        if(Input.GetKey(_moveKeyUp))
        {
            _moveDirection.y = 1;
        }
        else if(Input.GetKey(_moveKeyDown))
        {
            _moveDirection.y = -1;
        }

        if(Input.GetKey(_moveKeyRight))
        {
            _moveDirection.x = 1;
        }
        else if(Input.GetKey(_moveKeyLeft))
        {
            _moveDirection.x = -1;
        }

        return _moveDirection.normalized;
    }

    public Vector2 GetAttackDirection()
    {
        _attackDirection = Vector2.zero;

        if(Input.GetKey(_attackKeyUp))
        {
            _attackDirection.y = 1;
        }
        else if(Input.GetKey(_attackKeyDown))
        {
            _attackDirection.y = -1;
        }

        if(Input.GetKey(_attackKeyRight))
        {
            _attackDirection.x = 1;
        }
        else if(Input.GetKey(_attackKeyLeft))
        {
            _attackDirection.x = -1;
        }
        
        return _attackDirection.normalized;
    }
}
