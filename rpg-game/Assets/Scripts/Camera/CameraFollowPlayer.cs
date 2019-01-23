using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    [Range(0.01f, 1.0f)]
    public float smoothing = 0.5f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, smoothing);
    }
}
