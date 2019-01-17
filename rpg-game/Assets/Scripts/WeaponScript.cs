using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float weaponDmg;
    public int swingTime_ms;

    public bool _canAttack = true;

    private BoxCollider2D _hitBox;
    private Transform _weaponTransform;

    public async void Attack()
    {
        _canAttack = false;
        _hitBox.enabled = true;
        _weaponTransform.LeanRotateAroundLocal(Vector3.forward, 60, (float)swingTime_ms / 1000);
        await Task.Delay(swingTime_ms);
        _weaponTransform.LeanRotateAroundLocal(Vector3.forward, -60, (float)swingTime_ms / 1000);
        await Task.Delay(swingTime_ms);
        _hitBox.enabled = false;
        _canAttack = true;
    }

    void Start()
    {
        _hitBox = this.gameObject.GetComponent<BoxCollider2D>();
        Debug.Log(_hitBox);
        _hitBox.enabled = false;
        _weaponTransform = this.gameObject.GetComponent<Transform>();
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.GetComponent<HealthScript>())
        {
            col.gameObject.GetComponent<HealthScript>().TakeDamage(weaponDmg);
        }
    }
}
