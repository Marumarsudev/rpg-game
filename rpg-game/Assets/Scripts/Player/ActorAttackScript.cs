using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ActorAttackScript : MonoBehaviour
{
    private WeaponScript _weapon;

    private Transform _actorBody;

    private InputMngr InputManager;

    void Start()
    {
        InputManager = FindObjectOfType<InputMngr>();
        _weapon = GetComponentInChildren<WeaponScript>();
        _actorBody = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.GetAttackDirection() != Vector2.zero)
        {
            _actorBody.rotation = Quaternion.Lerp(_actorBody.rotation, Quaternion.LookRotation(Vector3.forward, InputManager.GetAttackDirection()), Time.deltaTime * 50);
        }

        if(_weapon.canAttack && InputManager.GetAttackKey())
        {
            _weapon.Attack();
        }
    }
}
