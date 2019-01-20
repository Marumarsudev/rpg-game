using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DefaultDaggerAttack : AttackMixinBase
{
    public FlyingDagger thrownDagger;

    public override async void Attack()
    {
        _weaponScript.canAttack = false;
        LeanTween.moveLocal(this.gameObject, new Vector3(-0.1f,0.2f,0), 0.1f);
        LeanTween.rotateLocal(this.gameObject, new Vector3(0,0,180), 0.1f);
        await Task.Delay(100);
        gameObject.SetActive(false);
        FlyingDagger clone = Instantiate(thrownDagger, _weilderTransform.position, _weilderTransform.rotation);
        clone.damage = _weaponScript.weaponDmg;
        clone.dir = _weilderTransform.up;
        await Task.Delay(_weaponScript.attackDelay);
        LeanTween.rotateLocal(this.gameObject, new Vector3(0,0,0), 0f);
        LeanTween.moveLocal(this.gameObject, new Vector3(0,0,0), 0f);
        gameObject.SetActive(true);
        _weaponScript.canAttack = true;
    }
}
