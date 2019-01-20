using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfNotMobile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SystemInfo.deviceType);
        if(SystemInfo.deviceType != DeviceType.Handheld)
        {
            this.enabled = false;
        }
    }
}
