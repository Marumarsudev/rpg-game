using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackMixinBase : MonoBehaviour
{
    protected WeaponScript _weaponScript;
    protected Collider2D _hitBox;
    protected Transform _weaponTransform;
    protected Transform _weilderTransform;
    protected Collider2D _weilderCollider;

    public virtual void Attack()
    {
        Debug.Log("Attack Mixin Base");
    }

    protected void Start()
    {
        if(GetComponentInParent<ActorMoveScript>())
        {
            _weilderTransform = GetComponentInParent<ActorMoveScript>().transform;
        }
        else if(GetComponentInParent<EnemyScript>())
        {
            _weilderTransform = GetComponentInParent<EnemyScript>().transform;
        }
        _weilderCollider = GetComponentInParent<Collider2D>();
        _weaponScript = this.gameObject.GetComponent<WeaponScript>();
        if(this.gameObject.GetComponent<Collider2D>())
        {
            _hitBox = this.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(_weilderCollider, _hitBox);
            _hitBox.enabled = false;
        }
        _weaponTransform = this.gameObject.GetComponent<Transform>();
    }
}
