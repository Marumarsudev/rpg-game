using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    //Components
    private WeaponBase weapon;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<WeaponBase>();

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        if(weapon)
        {
            animatorOverrideController["character_idle"] = weapon.weaponAnimations[0];
            animatorOverrideController["character_attack"] = weapon.weaponAnimations[1];
            animatorOverrideController["character_move"] = weapon.weaponAnimations[2];
            animatorOverrideController["character_defend_enter"] = weapon.weaponAnimations[3];
            animatorOverrideController["character_defend_cycle"] = weapon.weaponAnimations[4];
        }
    }

    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void ResetTrigger(string name)
    {
        animator.ResetTrigger(name);
    }

    public void SetBool(string name, bool set)
    {
        animator.SetBool(name, set);
    }

    public bool GetBool(string name) => animator.GetBool(name);

    public void SetFloat(string name, float set)
    {
        animator.SetFloat(name, set);
    }
}
