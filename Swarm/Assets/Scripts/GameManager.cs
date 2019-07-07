using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject playerUI;
    public Text playerHPText;
    public Text scoreText;
    public Text ammoText;
    public Text emptyText;

    public GameObject gameOverMenu;
    public Text gameOverScoreText;

    public GameObject pauseMenu;

    public int score;

    public Camera mainCamera;

    private PlayerController player;
    private GameObject[] bulletObjects;
    private GameObject[] enemyObjects;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameOverMenu.SetActive(false);
        emptyText.enabled = false;
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        playerHPText.text = "HP: " + player.hp;

        scoreText.text = "Score: " + updateScoreText(score);

        ammoText.text = updateAmmoText();

        if (player.shotgunAmmo <= 0)
        {
            emptyText.enabled = true;
        } else
        {
            emptyText.enabled = false;
        }

        if (player.hp <= 0)
        {
            bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");
            for (int i = 0; i < bulletObjects.Length; i++)
            {
                Destroy(bulletObjects[i]);
            }

            enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                enemyObjects[i].GetComponent<Chaser>().hp = 0;
            }

            playerUI.SetActive(false);
            gameOverMenu.SetActive(true);

            gameOverScoreText.text = updateScoreText(score);
        }
	}

    private string updateScoreText(int s)
    {
        string result = "";
        int length = s.ToString().Length;

        for (int i = length; i < 7; i++)
        {
            result += "0";
        }

        result += score;
        return result;
    }

    private string updateAmmoText()
    {
        string result = "";
        
        if (player.setWeapon == "Machine Gun")
        {
            result = "Machine Gun: ∞";
        } else if (player.setWeapon == "Shotgun")
        {
            result = "Shotgun: " + player.shotgunAmmo;
        }

        return result;
    }

    public void Pause()
    {
        if (pauseMenu.activeInHierarchy == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
