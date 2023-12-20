using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    public Rigidbody2D r;

// Start is called before the first frame update
void Start()
{
    r = GetComponent<Rigidbody2D>();
}

void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.CompareTag("Player"))
    {

        Invoke("drop_platform", 0.5f);
        Destroy(gameObject, 2f);
        PlatformManager.Instance.Invoke("SpawnPlatform", 5f);
    }
}
void drop_platform()
{
    r.isKinematic = false;
}
}
