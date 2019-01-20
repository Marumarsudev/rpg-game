using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DefaultDaggerAttack : AttackMixinBase
{
    public FlyingDagger thrownDagger;

    private float ltAttackDelay;

    public override async void Attack()
    {
        if(_weaponScript)
        {
            ltAttackDelay = (float)_weaponScript.attackDelay / 1000;
            _weaponScript.canAttack = false;
            LeanTween.moveLocal(this.gameObject, new Vector3(-0.1f,0.2f,0), 0.1f);
            LeanTween.rotateLocal(this.gameObject, new Vector3(0,0,180), 0.1f);
            await Task.Delay(100);
            gameObject.SetActive(false);
            FlyingDagger clone = Instantiate(thrownDagger, _weilderTransform.position, _weilderTransform.rotation);
            clone.weilderCollider = _weilderCollider;
            clone.damage = _weaponScript.weaponDmg;
            clone.dir = _weilderTransform.up;
            transform.localScale = new Vector3(0,0,0);
            transform.localPosition = new Vector3(-0.1f, 0, 0);
            LeanTween.rotateLocal(this.gameObject, new Vector3(0,0,0), 0f);
            gameObject.SetActive(true);
            LeanTween.moveLocal(this.gameObject, new Vector3(0,0,0), ltAttackDelay);
            LeanTween.scale(this.gameObject, new Vector3(1,1,1), ltAttackDelay);
            await Task.Delay(_weaponScript.attackDelay);
            _weaponScript.canAttack = true;
        }
    }
}
