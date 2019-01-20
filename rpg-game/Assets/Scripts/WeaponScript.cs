using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float weaponDmg;
    public int attackDelay;

    public bool canAttack = true;

    public List<AttackMixinBase> attackMixins = new List<AttackMixinBase>();

    public void Attack()
    {
        attackMixins.ForEach(attack => {
            attack.Attack();
        });
    }

}
