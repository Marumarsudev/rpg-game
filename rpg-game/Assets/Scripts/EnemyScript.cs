using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    attacking,
    defending,
    thinking
}

public class EnemyScript : MonoBehaviour
{
    private WeaponScript _weapon;

    private HealthScript health;

    private ActorAttackScript _player;

    private Rigidbody2D _playerBody;
    private Rigidbody2D _body;
    private Vector3 _currentPos;
    private Transform _transform;

    public States state = States.thinking;
    public int stateMagnitude;

    private int _attacksDone;
    public int doAttacks = 3;

    private float _distFromP;
    private float _timeInState = 0.0f;

    private int _otherEnemyAttack = 0;

    private List<EnemyScript> _otherEnemies = new List<EnemyScript>();

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponentInChildren<WeaponScript>();
        _transform = GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthScript>();
        _playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _player = _playerBody.GetComponent<ActorAttackScript>();

        InvokeRepeating("UpdateAction", 0.0f, 0.1f);
    }

    public void ChangeStateTo(States st)
    {
        state = st;
        _timeInState = 0;
        _attacksDone = 0;
    }

    public void CheckOtherEnemies()
    {
        _otherEnemies.Clear();
        foreach(EnemyScript e in FindObjectsOfType<EnemyScript>())
        {
            if(e.gameObject != this.gameObject)
            {
                _otherEnemies.Add(e);
            }
        }
    }

    private void CheckOtherEnemyAttacks()
    {
        _otherEnemyAttack = 0;
        _otherEnemies.ForEach(e => {
            if(e.state == States.attacking)
            {
                _otherEnemyAttack++;
            }
        });
    }

    private void MoveRegardingToPlayer(float speed)
    {
        _body.velocity = Vector2.ClampMagnitude((_playerBody.position - _body.position), 1) * speed;
        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.ClampMagnitude((_playerBody.position - _body.position), 1)), Time.deltaTime * 100);
    }

    // Update is called once per frame

    void UpdateAction()
    {
        if(_player && health.health > 0)
        {
            _timeInState += 0.1f;
            _distFromP = Vector2.Distance(_playerBody.position, _body.position);
            switch(state)
            {
                case States.attacking:
                    if (_attacksDone < doAttacks && _timeInState < 5)
                    {
                        if(_distFromP >= _weapon.weaponRange)
                        {
                            MoveRegardingToPlayer(3f);
                        }
                        else if(_weapon.canAttack && _weapon)
                        {
                            _body.velocity = Vector2.zero;
                            _weapon.Attack();
                            _attacksDone++;
                            _timeInState = 0;
                        }
                    }
                    else
                    {
                        _attacksDone = 0;
                        _timeInState = 0;
                        state = States.defending;
                    }
                break;
                case States.defending:
                    if(_distFromP <= Random.Range(1.0f, 1.5f))
                    {
                        MoveRegardingToPlayer(-4f);
                    }
                    else
                    {
                        _timeInState = 0;
                        state = States.thinking;
                    }
                break;
                case States.thinking:
                    if(_distFromP >= Random.Range(2.0f, 2.5f))
                    {
                        MoveRegardingToPlayer(1f);
                    }
                    else if(_distFromP <= _weapon.weaponRange || _timeInState > Random.Range(0.5f, 2f))
                    {
                        CheckOtherEnemies();
                        CheckOtherEnemyAttacks();
                        if(_otherEnemyAttack < 2)
                        {
                            _timeInState = 0;
                            state = States.attacking;
                        }
                    }
                    else
                    {
                        _body.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                    }
                break;
            }
        }
        else
        {
            _body.velocity = Vector2.zero;
        }
    }
}
