using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HealthScript : MonoBehaviour
{
    public List<OnDeath> deathEvents = new List<OnDeath>();

    public List<OnTakeDamage> takedamageEvents = new List<OnTakeDamage>();

    public float health;
    public bool calculateHealth = false;

    private Color origColor;

    private void Start()
    {
        SpriteRenderer sR = this.gameObject.GetComponent<SpriteRenderer>();
        origColor = new Color(sR.color.r, sR.color.g, sR.color.b);
    }

    private async void DamageAnim()
    {
        try
        {
        LeanTween.color(this.gameObject, new Color(255, 0, 0), 0.1f);
        await Task.Delay(100);
        LeanTween.color(this.gameObject, origColor, 0.1f);
        }
        catch{}
    }

    public void TakeDamage(float dmg)
    {
        if(health > 0)
        {
            takedamageEvents.ForEach(tDE => {
                tDE.CallOnTakeDamage();
            });
            DamageAnim();
            health -= dmg;
            if (health <= 0)
                Death();
        }
    }

    private void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        LeanTween.alpha(this.gameObject, 0, 0.5f).setDestroyOnComplete(true);
        deathEvents.ForEach(dE => {
            dE.CallOnDeath();
        });
    }
}
