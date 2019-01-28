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
    private float maxHealth;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        animationManager = GetComponent<AnimationManager>();
        coll = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<EnemySpawner>();
    }

    public void TakeDamage(float dmg)
    {
        if (currentHealth > 0)
        {
            Debug.Log("Took dmg!!!");
            animationManager.SetTrigger("Damage");
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
        spawner.RemoveFromList(this.gameObject);
        body.velocity = Vector2.zero;
        coll.enabled = false;
        LeanTween.alpha(this.gameObject, 0.0f, 2.0f).setOnComplete(() => {Destroy(this.gameObject);});
    }
}
