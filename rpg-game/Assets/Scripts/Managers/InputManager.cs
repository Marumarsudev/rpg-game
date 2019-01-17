using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static KeyCode _moveKeyUp = KeyCode.W;
    private static KeyCode _moveKeyDown = KeyCode.S;
    private static KeyCode _moveKeyLeft = KeyCode.A;
    private static KeyCode _moveKeyRight = KeyCode.D;

    private static KeyCode _attackKeyUp = KeyCode.UpArrow;
    private static KeyCode _attackKeyDown = KeyCode.DownArrow;
    private static KeyCode _attackKeyLeft = KeyCode.LeftArrow;
    private static KeyCode _attackKeyRight = KeyCode.RightArrow;

    private static KeyCode _attackKey = KeyCode.Space;

    private static Vector2 _moveDirection = new Vector2();
    private static Vector2 _attackDirection = new Vector2();

    public static bool GetAttackKey()
    {
        return Input.GetKey(_attackKey);
    }

    public static bool GetAttackKeyDown()
    {
        return Input.GetKeyDown(_attackKey);
    }

    public static Vector2 GetMoveDirection()
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

    public static Vector2 GetAttackDirection()
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
