using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DefaultSwordAttack : AttackMixinBase
{
    public override async void Attack()
    {
        _weaponScript.canAttack = false;
        //_hitBox.enabled = true;
        ToggleHitbox(true);
        LeanTween.rotateLocal(this.gameObject, new Vector3(0, 0, 25), 0.1f).setEaseOutBack();
        await Task.Delay(100);
        //_hitBox.enabled = false;
        ToggleHitbox(false);
        await Task.Delay(_weaponScript.attackDelay / 2 - 50);
        LeanTween.rotateLocal(this.gameObject, new Vector3(-50, 0, -15), 0.1f).setEaseInCirc();
        await Task.Delay(_weaponScript.attackDelay);
        _weaponScript.canAttack = true;
    }

    private void ToggleHitbox(bool t)
    {
        if(_hitBox)
        {
            _hitBox.enabled = t;
        }
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.GetComponent<HealthScript>())
        {
            col.gameObject.GetComponent<HealthScript>().TakeDamage(_weaponScript.weaponDmg);
        }
    }
}
