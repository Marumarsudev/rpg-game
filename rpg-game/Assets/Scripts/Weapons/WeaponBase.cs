using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    //Components
    private Collider2D hitBox;

    //Properties
    [Tooltip("0: idle\n1: attack\n2: move\n3: defend_enter\n4: defend_cycle")]
    public AnimationClip[] weaponAnimations;
    public float damage;
    public float range;

    // Start is called before the first frame update
    void Awake()
    {
        hitBox = GetComponent<Collider2D>();
        hitBox.enabled = false;
    }

    public void EnableHitBox()
    {
        hitBox.enabled = true;
    }

    public void DisableHitBox()
    {
        hitBox.enabled = false;
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<Health>())
        {
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
