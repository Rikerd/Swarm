using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 6f;
    public GameObject bullet;
    public GameObject shot;
    public float spawnDistance = 0.5f;
    public float setMachineFireTimer;
    public float setShotgunFireTimer;
    public float hp = 3f;
    public float setInvincibilityTimer = 0.3f;
    public int shotgunAmmo;
    public float shotgunAngle = 30f;
    public float thrust = 4f;
    public string setWeapon;

    private Rigidbody2D rb2d;
    private float machineFireTimer;
    private float shotgunFireTimer;
    private Vector3 bulletSpawnPos;
    private Vector3 mousePos;
    private float invincibilityTimer;
    private bool invincible;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        machineFireTimer = 0f;
        shotgunFireTimer = 0f;
        invincible = false;
        invincibilityTimer = 0f;
        sprite = GetComponent<SpriteRenderer>();
        setWeapon = "Machine Gun";
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();

        if (machineFireTimer > 0f)
        {
            machineFireTimer -= Time.deltaTime;
        }

        if (shotgunFireTimer > 0f)
        {
            shotgunFireTimer -= Time.deltaTime;
        }

        if (shotgunAmmo <= 0)
        {
            setWeapon = "Machine Gun";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (setWeapon == "Shotgun")
            {
                setWeapon = "Machine Gun";
            }
            else if (setWeapon == "Machine Gun" && shotgunAmmo > 0)
            {
                setWeapon = "Shotgun";
            }
        }

        if (Input.GetAxis("Fire") != 0 && hp > 0f)
        {
            bulletSpawnPos = transform.position + (transform.up * spawnDistance);

            if (setWeapon == "Machine Gun" && machineFireTimer <= 0f)
            {
                Instantiate(bullet, bulletSpawnPos, transform.rotation);
                machineFireTimer = setMachineFireTimer;
            }

            if (setWeapon == "Shotgun" && shotgunFireTimer <= 0f && shotgunAmmo > 0)
            {
                Instantiate(shot, bulletSpawnPos, transform.rotation);

                GameObject shot1 = Instantiate(shot, bulletSpawnPos, transform.rotation);
                shot1.transform.Rotate(Vector3.forward * shotgunAngle);

                GameObject shot2 = Instantiate(shot, bulletSpawnPos, transform.rotation);
                shot2.transform.Rotate(Vector3.forward * -shotgunAngle);

                rb2d.AddForce(-transform.up * thrust * 300);

                shotgunAmmo--;

                shotgunFireTimer = setShotgunFireTimer;
            }
        }

        if (invincibilityTimer > 0f)
        {
            sprite.enabled = !sprite.enabled;
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            sprite.enabled = true;
            invincible = false;
        }

        if (hp <= 0)
        {
            invincibilityTimer = 0f;
            sprite.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * movementSpeed;
    }

    private void FaceMouse()
    {
        if (Time.timeScale == 1)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        }
    }

    public void takeDamage(float dmg)
    {
        if (!invincible)
        {
            Camera.main.GetComponent<ScreenShake>().StartShake();
            hp -= dmg;
            invincible = true;
            invincibilityTimer = setInvincibilityTimer;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rb2d.velocity = Vector3.zero;
        }
    }
}
