using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    private Transform _heldWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _heldWeapon = transform.GetChild(1);
        Instantiate(weapons[Random.Range(0,weapons.Count)], _heldWeapon.position, Quaternion.identity, _heldWeapon);
    }
}
