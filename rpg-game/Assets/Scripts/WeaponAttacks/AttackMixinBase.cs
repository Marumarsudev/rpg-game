using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackMixinBase : MonoBehaviour
{
    protected WeaponScript _weaponScript;
    protected BoxCollider2D _hitBox;
    protected Transform _weaponTransform;
    protected Transform _weilderTransform;

    public virtual void Attack()
    {
        Debug.Log("Attack Mixin Base");
    }

    protected void Start()
    {
        _weilderTransform = GetComponentInParent<ActorMoveScript>().transform;
        _weaponScript = this.gameObject.GetComponent<WeaponScript>();
        if(this.gameObject.GetComponent<BoxCollider2D>())
        {
            _hitBox = this.gameObject.GetComponent<BoxCollider2D>();
            _hitBox.enabled = false;
        }
        _weaponTransform = this.gameObject.GetComponent<Transform>();
    }
}
