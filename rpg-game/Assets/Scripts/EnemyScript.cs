using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private WeaponScript _weapon;

    private HealthScript health;

    private Rigidbody2D _player;
    private Rigidbody2D _body;
    private Vector3 _currentPos;
    private Transform _transform;

    private List<GameObject> _otherEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponentInChildren<WeaponScript>();
        _transform = GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthScript>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void CheckOtherEnemies()
    {
        _otherEnemies.Clear();
        foreach(EnemyScript e in FindObjectsOfType<EnemyScript>())
        {
            if(e.gameObject != this.gameObject)
            {
                _otherEnemies.Add(e.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_player)
        {
            _body.velocity = Vector2.zero;
            _otherEnemies.ForEach(oE => {
                if(oE)
                {
                    if(Vector2.Distance(_body.position, oE.transform.position) <= 1f)
                    {
                        _body.velocity += (Vector2.ClampMagnitude(((Vector2)oE.transform.position - _body.position), 1) * -1f);
                    }
                }
            });
            if(health.health > 0 && Vector2.Distance(_player.position, _body.position) > 0.3f)
            {
                _body.velocity += Vector2.ClampMagnitude((_player.position - _body.position), 1) * 1.5f;
                _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.ClampMagnitude((_player.position - _body.position), 1)), Time.deltaTime * 50);
            }
            if(Vector2.Distance(_player.position, _body.position) <= _weapon.weaponRange + 0.1f)
            {
                if(_weapon.canAttack && _weapon)
                {
                    _weapon.Attack();
                }
            }
        }
        else
        {
            _body.velocity = Vector2.zero;
        }
    }
}
