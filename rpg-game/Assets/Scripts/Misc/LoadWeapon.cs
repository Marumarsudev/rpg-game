using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeapon : MonoBehaviour
{

    public List<GameObject> weapons = new List<GameObject>();

    private string[] weaponStrings = {"spear", "sword"};

    void Awake()
    {
        Transform parent = null;

        foreach (Transform child in transform)
        {
            if (child.name == "RightHand")
            {
                parent = child;
            }
        }
        //GameObject weap = weapons[Random.Range(0, weapons.Count)];
        string weapStr = weaponStrings[Random.Range(0, weaponStrings.Length)];
        GameObject weap = Resources.Load<GameObject>("Weapons/" + weapStr);
        if(parent != null)
        {
            GameObject weapon = Instantiate(weap, weap.transform.position + parent.position, Quaternion.identity, parent) as GameObject;
        }
        else
        {
            Debug.LogError("Parent is null");
        }
        
    }
}
