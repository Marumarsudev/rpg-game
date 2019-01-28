using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Managers
    private InputManager InputManager;

    //Dependencies
    private Health health;
    private Text hpText;

    //Components
    private Rigidbody2D body;
    private AnimationManager animationManager;

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
        health = GetComponent<Health>();
        hpText = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "Hitpoints: " + health.CurrentHealth.ToString() + "/" + health.MaxHealth.ToString();

        moveDirection = InputManager.GetMovement();
        lookDirection = Vector2.ClampMagnitude((Vector2)InputManager.GetMousePosition() - body.position, 1.0f);

        if(InputManager.GetAttack())
        {
            animationManager.SetTrigger("Attack");
        }
        else if (InputManager.GetAttackUp())
        {
            animationManager.ResetTrigger("Attack");
        }

        if(InputManager.GetDefend() && !animationManager.GetBool("Defending"))
        {
            animationManager.SetBool("Defending", true);
            animationManager.SetTrigger("Defend");
        }
        else if(InputManager.GetDefendUp())
        {
            animationManager.ResetTrigger("Defend");
            animationManager.SetBool("Defending", false);
        }

        animationManager.SetFloat("speed", Mathf.Clamp01(Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y)));
    }

    void FixedUpdate()
    {
        Move(moveDirection, speed);
        Turn(lookDirection);
    }

    private void Move(Vector2 dir, float speed)
    {
        body.velocity = dir * speed;
    }

    private void Turn(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
