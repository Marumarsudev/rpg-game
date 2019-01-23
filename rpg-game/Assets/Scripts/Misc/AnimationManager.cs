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

        animatorOverrideController["characteridle"] = weapon.weaponAnimations[0];
        animatorOverrideController["characterattack"] = weapon.weaponAnimations[1];
        animatorOverrideController["charactermove"] = weapon.weaponAnimations[2];
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

    public void SetFloat(string name, float set)
    {
        animator.SetFloat(name, set);
    }
}
