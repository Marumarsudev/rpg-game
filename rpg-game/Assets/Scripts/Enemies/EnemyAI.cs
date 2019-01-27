using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States
{
    Attacking,
    Defending,
    Thinking
}

public class EnemyAI : MonoBehaviour
{
    //Dependencies
    private AStarPathing pathing;
    private Rigidbody2D player;

    //Components
    private AnimationManager animationManager;
    private Rigidbody2D body;
    private Transform bodyTransform;

    //Properties
    public float speed;
    private States states = States.Thinking;
    private List<Vector2> currentPath = new List<Vector2>();
    public LayerMask mask;
    public LayerMask enemyMask;
    private Vector2 moveFromEnemy;
    private float atPlayerChangeDir = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        pathing = FindObjectOfType<AStarPathing>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        animationManager = GetComponent<AnimationManager>();
        body = GetComponent<Rigidbody2D>();
        bodyTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(body.velocity.magnitude > speed)
        {
            body.velocity = body.velocity.normalized * speed;
        }
    }

    private void Update()
    {
        UpdateBehaviour();
    }


    void MoveAlongPath()
    {
        if(currentPath.Count > 0)
        {

            Vector2 currentTarget = currentPath[0];

            for (int i = 1; i < currentPath.Count - 1; i++)
            {
                Vector2 h1 = currentTarget - body.position;
                Vector2 h2 = currentPath[i] - body.position;

                if(h1.sqrMagnitude > h2.sqrMagnitude)
                {
                    currentTarget = currentPath[i];
                }
            }

            Vector2 heading = currentTarget - body.position;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance;
            if(moveFromEnemy != Vector2.zero)
            {
                body.AddForce(moveFromEnemy.normalized * speed);
            }
            else
            {
                body.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
            }
            

            if(heading.sqrMagnitude < 0.1f * 0.1f)
            {
                currentPath.Remove(currentPath[0]);
            }
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    void UpdateBehaviour()
    {
        Vector2 heading = player.position - body.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;

        float distanceFromPlayer = (player.position - body.position).sqrMagnitude;
        float maxRange = 3.0f * 3.0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), 0.1f);

        moveFromEnemy = Vector2.zero;

        foreach (var enemy in Physics2D.CircleCastAll(transform.position, 1f, Vector2.up, 1f, enemyMask))
        {
            if(enemy.collider.gameObject != this.gameObject)
            {
                Debug.Log(enemy.transform.name);
                Vector2 hd = (Vector2)enemy.transform.position - body.position;
                float dist = hd.magnitude;
                Vector2 dir = hd / dist;
                moveFromEnemy = -dir;
            }
        }

        switch(states)
        {
            case States.Thinking:

            if(distanceFromPlayer < maxRange)
            {
                if(atPlayerChangeDir == 0.0f)
                {
                    body.velocity = Vector2.zero;
                    Vector2 xtradir = (Vector2)transform.right;
                    if (Random.value > 0.5)
                        xtradir = -(Vector2)transform.right;
                    body.AddForce(xtradir * (speed / 2), ForceMode2D.Impulse);
                    atPlayerChangeDir += Time.deltaTime;
                }
                else if(atPlayerChangeDir >= 2.5f)
                {
                    atPlayerChangeDir = 0;
                }
                else
                {
                    atPlayerChangeDir += Time.deltaTime;
                }
            }
            else if(currentPath.Count == 0 || Vector2.Distance(currentPath[currentPath.Count - 1], player.position) > 2.0f)
            {
                currentPath.Clear();
                if(Physics2D.Raycast(transform.position, direction, distance, mask))
                {
                    currentPath = pathing.GetRoute(body.position, player.position);
                }
                else
                {
                    currentPath.Add(player.position);
                }

                MoveAlongPath();
            }
            else
            {
                MoveAlongPath();
            }
            break;

            case States.Attacking:
            break;

            case States.Defending:
            break;
        }
    }
}
