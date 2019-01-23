using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent weaponHitBoxOff;
    public UnityEvent weaponHitBoxOn;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(weaponHitBoxOn == null)
            weaponHitBoxOn = new UnityEvent();
        if(weaponHitBoxOff == null)
            weaponHitBoxOff = new UnityEvent();
    }

    public void ToggleWeaponHitBoxOff()
    {

    }

    public void ToggleWeaponHitBoxOn()
    {

    }
}
