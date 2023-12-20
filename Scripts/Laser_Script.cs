using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Script : MonoBehaviour
{
    public float fireSpeed=80f;
    private Transform player;
    private Vector2 target;
    [SerializeField]
    Rigidbody2D rbl;

    void Start()
    {
        rbl = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (transform.position.x < player.position.x)
        {
            rbl.velocity = new Vector2(fireSpeed, 0);
        }
        else
        {
            rbl.velocity = new Vector2(-fireSpeed, 0);
        }
        Destroy(gameObject, 1f);
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            Destroy(gameObject);
        else if (!col.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
