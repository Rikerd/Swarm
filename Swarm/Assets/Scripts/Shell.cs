using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {
    public float movementSpeed;
    public float damage = 1f;
    public float activeTimer = 1f;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.velocity = transform.up * movementSpeed;
    }

    private void Update()
    {
        if (activeTimer > 0f)
        {
            activeTimer -= Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Chaser>().takeDamage(damage);
            
        }
    }
}
