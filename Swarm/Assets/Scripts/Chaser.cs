using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {
    public float lowerMovementSpeed;
    public float upperMovementSpeed;
    public float hp = 1f;
    public float damage = 1f;
    public int pointsGiven = 10;

    public GameObject crate;
    public float crateSpawnRate;

    public bool isFatty;

    private float movementSpeed;
    private Transform player;
    private Rigidbody2D rb2d;
    private ParticleSystem particle;
    private bool deathStart;

	// Use this for initialization
	private void Awake()
    {
        movementSpeed = Random.Range(lowerMovementSpeed, upperMovementSpeed);
        rb2d = GetComponent<Rigidbody2D>();
        particle = GetComponent<ParticleSystem>();
        deathStart = false;
	}

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (hp > 0f)
        {
            rb2d.MovePosition(Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime));
        } else if (hp <= 0f && !deathStart)
        {
            deathStart = true;

            if (Random.Range(0f, 1f) <= crateSpawnRate)
            {
                Instantiate(crate, transform.position, transform.rotation);
            }

            if (isFatty)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;

                particle.Play();
            }
            else
            {
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;

                particle.Play();
            }

            Destroy(gameObject, 1.5f);
        }
	}

    public void takeDamage(float dmg)
    {
        hp -= dmg;
        GameObject.Find("Game Manager").GetComponent<GameManager>().score += pointsGiven;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            rb2d.angularVelocity = 0f;
        }
    }
}
