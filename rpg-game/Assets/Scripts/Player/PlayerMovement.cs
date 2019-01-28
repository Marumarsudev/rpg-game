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
    private WeaponBase weapon;

    //Components
    private Rigidbody2D body;
    private AnimationManager animationManager;

    //Properties
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float dashMaxSpeed = 0.0f;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    private float currentDashSpeed = 1.0f;

    private LTDescr turnTween;

    // Start is called before the first frame update
    void Awake()
    {
        InputManager = FindObjectOfType<InputManager>();
        body = GetComponent<Rigidbody2D>();
        animationManager = GetComponent<AnimationManager>();
        health = GetComponent<Health>();
        hpText = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Text>();
        weapon = GetComponentInChildren<WeaponBase>();
    }

    void FixedUpdate()
    {
        Move(moveDirection, speed);
        Turn(lookDirection);
        DashSpeedDecrease(0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "Hitpoints: " + health.CurrentHealth.ToString() + "/" + health.MaxHealth.ToString();

        if(health.CurrentHealth > 0)
        {
            moveDirection = InputManager.GetMovement();
            lookDirection = Vector2.ClampMagnitude((Vector2)InputManager.GetMousePosition() - body.position, 1.0f);
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        GetInputs();

        animationManager.SetFloat("speed", Mathf.Clamp01(Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y)));
    }

    private void GetInputs()
    {
        if(health.CurrentHealth > 0)
        {
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

            if (InputManager.GetDashDown() && currentDashSpeed <= 1.0f)
            {
                currentDashSpeed = dashMaxSpeed;
            }
        }
    }

    private void DashSpeedDecrease(float dec)
    {
        currentDashSpeed -= dec;
        if(currentDashSpeed < 1)
            currentDashSpeed = 1;
    }

    private void Move(Vector2 dir, float speed)
    {
        body.velocity = dir * speed * currentDashSpeed;
    }

    private void Turn(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if(animationManager.GetFloat("Attacking") > 0.5f)
        {
            //transform.rotation = transform.rotation;
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), 0.2f);
            // transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
