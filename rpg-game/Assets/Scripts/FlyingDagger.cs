using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDagger : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage;

    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyDagger", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.back * -1000 * Time.deltaTime);
        this.transform.Translate(dir * Time.deltaTime * speed, Space.World);
    }

    private void DestroyDagger()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.GetComponent<HealthScript>())
        {
            DestroyDagger();
            col.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
        }
    }
}
