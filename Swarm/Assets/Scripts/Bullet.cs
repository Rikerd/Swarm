using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float movementSpeed;
    public float damage = 1f;

    private Rigidbody2D rb2d;
    private bool hit;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        hit = false;
    }

    private void Start()
    {
        rb2d.velocity = transform.up * movementSpeed;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !hit)
        {
            hit = true;
            collision.gameObject.GetComponent<Chaser>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
