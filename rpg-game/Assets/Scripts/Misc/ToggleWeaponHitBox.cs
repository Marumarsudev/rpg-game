using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWeaponHitBox : MonoBehaviour
{
    private WeaponBase weapon;

    void Awake()
    {
        weapon = GetComponentInChildren<WeaponBase>();
    }

    public void SetWeaponHitBoxOn()
    {
        weapon.EnableHitBox();
    }

    public void SetWeaponHitBoxOff()
    {
        weapon.DisableHitBox();
    }
}
