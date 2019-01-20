using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomWeapon : MonoBehaviour
{

    public List<GameObject> weapons = new List<GameObject>();
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(weapons[Random.Range(0,weapons.Count)], parent.position, Quaternion.identity, parent);
    }
}
