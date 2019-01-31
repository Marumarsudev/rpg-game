using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Dependencies
    private EnemySpawner spawner;

    //Components
    private AnimationManager animationManager;
    private Rigidbody2D body;
    private Collider2D coll;

    [SerializeField]
    private float maxHealth = 0;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }

    private List<DamageColor> sprites = new List<DamageColor>();

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animationManager = GetComponent<AnimationManager>();
        coll = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<EnemySpawner>();

        sprites.Add(GetComponent<DamageColor>());

        foreach(Transform child in transform)
        {
            if(child.GetComponent<DamageColor>())
            {
                sprites.Add(child.GetComponent<DamageColor>());
            }
        }

    }

    private void DamageColor()
    {
        sprites.ForEach(gO => {
            gO.DamageAnim();
        });
    }

    public void TakeDamage(float dmg)
    {
        if (currentHealth > 0)
        {
            //animationManager.SetTrigger("Damage");
            DamageColor();
            currentHealth -= dmg;
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
        this.gameObject.tag = "Dead";
        body.velocity = Vector2.zero;
        coll.enabled = false;
        LeanTween.alpha(this.gameObject, 0.0f, 2.0f).setOnComplete(() => {
            if(GetComponent<EnemyAI>())
            {
                spawner.RemoveFromList(this.gameObject);
            }
            
            Destroy(this.gameObject);
            });
    }
}
