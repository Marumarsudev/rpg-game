using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ActorAttackScript : MonoBehaviour
{
    public WeaponScript _weapon;

    private Transform _actorBody;

    void Start()
    {
        _actorBody = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.GetAttackDirection() != Vector2.zero)
            _actorBody.rotation = Quaternion.Lerp(_actorBody.rotation, Quaternion.LookRotation(Vector3.forward, InputManager.GetAttackDirection()), Time.deltaTime * 10);

        if(_weapon._canAttack && InputManager.GetAttackKeyDown())
        {
            _weapon.Attack();
        }
    }
}
