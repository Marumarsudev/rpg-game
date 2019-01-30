using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States
{
    Attacking,
    Defending,
    MovingToPlayer,
    AtPlayer,
    Wander
}

[System.Serializable]
public struct timeUntilAttack
{
    public float min;
    public float max;
}

public class EnemyAI : MonoBehaviour
{
    //Dependencies
    private AStarPathing pathing;
    private Rigidbody2D target;
    private WeaponBase weapon;

    //Components
    private Health health;
    private AnimationManager animationManager;
    private Rigidbody2D body;
    private Transform bodyTransform;

    //Properties
    [SerializeField]
    public timeUntilAttack waitUntilAttack;
    private float waitedUntilAttack = 0.0f;
    public float maxRangeFromPlayer;
    public float atPlayerLeaveRangeMult;
    public float speed;
    public float attackSpeedMult;
    public float doAttackRangeDist;
    public float attackDelayAfterReachedPlayer;
    private States state = States.MovingToPlayer;
    private List<Vector2> currentPath = new List<Vector2>();

    public float enemyUpdateRate = 0.05f;
    private float enemyUpdateTime = 0.00f;

    public LayerMask mask;
    public LayerMask enemyMask;

    private Vector2 avoidOtherEnemies;


    void Start()
    {
        pathing = FindObjectOfType<AStarPathing>();
        if(GameObject.FindGameObjectWithTag("Player"))
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        animationManager = GetComponent<AnimationManager>();
        body = GetComponent<Rigidbody2D>();
        bodyTransform = GetComponent<Transform>();
        health = GetComponent<Health>();
        weapon = GetComponentInChildren<WeaponBase>();
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case States.AtPlayer:
                waitedUntilAttack += Time.deltaTime;
            break;

            case States.Attacking:
            if(body.velocity.magnitude > speed * attackSpeedMult)
            {
                body.velocity = body.velocity.normalized * speed * attackSpeedMult;
            }
            break;
            
            default:
            if(body.velocity.magnitude > speed)
            {
                body.velocity = body.velocity.normalized * speed;
            }
            break;
        }
        animationManager.SetFloat("speed", Mathf.Clamp01(Mathf.Abs(body.velocity.x) + Mathf.Abs(body.velocity.y)));
    }

    private void Update()
    {
        if(target == null)
        {
            if(GameObject.FindGameObjectWithTag("Player"))
            {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            else if(GameObject.FindGameObjectWithTag("Enemy"))
            {

                foreach(GameObject gO in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if(gO != this.gameObject)
                    {
                        target = gO.GetComponent<Rigidbody2D>();
                        break;
                    }
                }
            }
        }

        if(health.CurrentHealth > 0 && target != null && enemyUpdateTime >= enemyUpdateRate)
        {
            enemyUpdateTime = 0.0f;
            UpdateBehaviour();
        }

        enemyUpdateTime += Time.deltaTime;
    }

    void UpdateBehaviour()
    {
        Vector2 heading = target.position - body.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;

        float distanceFromPlayer = (target.position - body.position).sqrMagnitude;
        float maxRange = maxRangeFromPlayer * maxRangeFromPlayer;

        avoidOtherEnemies = Vector2.zero;

        if (target.tag == "Player")
        {
            foreach (var enemy in Physics2D.CircleCastAll(transform.position, 3f, Vector2.zero, 0.0f, enemyMask))
            {
                if(enemy.transform.gameObject != transform.gameObject /*&& enemy.transform.GetComponent<EnemyAI>().state == States.Attacking*/)
                {
                    Vector2 hd = (Vector2)enemy.transform.position - body.position;
                    float dist = hd.magnitude;
                    Vector2 dir = hd / dist;

                    Vector2 hdp = target.position - (Vector2)enemy.transform.position;
                    float enemyDistFromPlayer = hdp.sqrMagnitude;
                    Vector2 dirp = hdp / enemyDistFromPlayer;

                    if(enemy.transform.GetComponent<Rigidbody2D>() && enemyDistFromPlayer < distanceFromPlayer)
                    {
                        enemy.transform.GetComponent<Rigidbody2D>().AddForce((-dirp + dir) * 25);
                    }
                }
            }
        }

        switch(state)
        {
            case States.MovingToPlayer:

            if(distanceFromPlayer < maxRange)
            {
                if(attackDelayAfterReachedPlayer == 0.0f)
                {
                    state = States.Attacking;
                }
                else
                {
                    waitedUntilAttack = Mathf.Abs(waitUntilAttack.min - attackDelayAfterReachedPlayer);
                    state = States.AtPlayer;
                }
            }
            else if(currentPath.Count == 0 || Vector2.Distance(currentPath[currentPath.Count - 1], target.position) > 2.0f)
            {
                currentPath.Clear();
                if(Physics2D.Raycast(transform.position, direction, distance, mask))
                {
                    currentPath = pathing.GetRoute(body.position, target.position);
                }
                else
                {
                    currentPath.Add(target.position);
                }

                MoveAlongPath();
            }
            else
            {
                MoveAlongPath();
            }
            break;

            case States.AtPlayer:

            body.velocity = Vector2.zero;
            LookAtTarget(target.position);

            if(waitedUntilAttack >= Random.Range(waitUntilAttack.min, waitUntilAttack.max))
            {
                waitedUntilAttack = 0.0f;
                state = States.Attacking;
            }
            else if(distanceFromPlayer > maxRange * atPlayerLeaveRangeMult)
            {
                waitedUntilAttack = 0.0f;
                state = States.MovingToPlayer;
            }
            break;

            case States.Attacking:
                try
                {
                if(Physics2D.Raycast(body.position + direction, direction, distanceFromPlayer).transform.gameObject == target.gameObject)
                {
                    LookAtTarget(target.position);
                    body.AddForce(direction * speed * attackSpeedMult, ForceMode2D.Impulse);
                    if(distanceFromPlayer <= (weapon.range + doAttackRangeDist) * (weapon.range + doAttackRangeDist))
                    {
                        animationManager.SetTrigger("Attack");
                        state = States.AtPlayer;
                    }
                }
                else
                {
                    state = States.AtPlayer;
                }
                }
                catch
                {
                    Debug.Log("Target went missing");
                }
            break;

            case States.Defending:
            break;
        }
    }

    private void LookAtTarget(Vector2 target)
    {
        Vector2 direction = target - body.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), enemyUpdateRate);
        LeanTween.rotateZ(this.gameObject, angle - 90, 0.3f);

    }

        void MoveAlongPath()
    {
        if(currentPath.Count > 0)
        {

            Vector2 currentTarget = currentPath[0];

            if (currentPath.Count >= 2)
            {
                Vector2 h1 = currentTarget - target.position;
                Vector2 h2 = currentPath[1] - target.position;

                if (h1.sqrMagnitude > h2.sqrMagnitude)
                {
                    currentTarget = currentPath[1];
                }
            }
            Vector2 heading = currentTarget - body.position;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance;

            LookAtTarget(currentTarget);

            body.AddForce(direction.normalized * speed, ForceMode2D.Impulse);

            if(heading.sqrMagnitude < 0.4f * 0.4f)
            {
                currentPath.Remove(currentPath[0]);
            }
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }
}
