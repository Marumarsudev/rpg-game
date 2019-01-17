﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public List<OnDeath> deathEvents = new List<OnDeath>();

    public float health;
    public bool calculateHealth = false;

    public void TakeDamage(float dmg)
    {
        if(health > 0)
        {
            health -= dmg;
            if (health <= 0)
                Death();
        }
    }

    private void Death()
    {
        LeanTween.alpha(this.gameObject, 0, 0.5f).setDestroyOnComplete(true);
        deathEvents.ForEach(dE => {
            dE.CallOnDeath();
        });
    }
}