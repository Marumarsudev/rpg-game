using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Dependencies
    private AnimationManager animationManager;
    
    public float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        animationManager = GetComponent<AnimationManager>();
    }

    public void TakeDamage(float dmg)
    {
        if (currentHealth > 0)
        {
            animationManager.SetTrigger("Damage");
            currentHealth -= dmg;
            Debug.Log(this.name + " Took " + dmg.ToString() + " and has " + currentHealth.ToString() + "/" + maxHealth.ToString() + " HP left!");
            if(currentHealth <= 0)
            {
                Death();
            }
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        LeanTween.alpha(this.gameObject, 0.0f, 2.0f).setOnComplete(() => {Destroy(this.gameObject);});
    }
}
