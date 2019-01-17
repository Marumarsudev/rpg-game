using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public virtual void CallOnDeath()
    {
        Debug.Log("Base call on death.");
    }
}
