using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    public int ammo;
    public float activeTimer;

    private void Update()
    {
        if (activeTimer > 0f)
        {
            activeTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().shotgunAmmo += ammo;
            Destroy(gameObject);
        }
    }
}
