using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public float minSpawnTimer;
    public float maxSpawnTimer;
    public GameObject[] enemies;
    public float rateDifference;
    public float[] spawnAmountList;

    private float spawnTimer;
    private PlayerController player;

    private bool useHorizontalEdge;
    private bool usePositiveEdge;
    private float chosenEdgePosition;
    private float otherEdgePosition;
    private float xVP;
    private float yVP;
    private int enemySelected;

    // Use this for initialization
    private void Awake()
    {
        spawnTimer = 0f;
	}

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawnTimer <= 0f && player.hp > 0f)
        {
            useHorizontalEdge = (Random.Range(0, 2) == 0);
            usePositiveEdge = (Random.Range(0, 2) == 0);
            chosenEdgePosition = Random.Range(0f, 1f);

            otherEdgePosition = usePositiveEdge ? 1f : 0f;
            xVP = useHorizontalEdge ? chosenEdgePosition : otherEdgePosition;
            yVP = useHorizontalEdge ? otherEdgePosition : chosenEdgePosition;

            if (useHorizontalEdge)
            {
                yVP += usePositiveEdge ? 0.3f : -0.3f;
            } else
            {
                xVP += usePositiveEdge ? 0.3f : -0.3f;
            }

            Vector3 posWS = Camera.main.ViewportToWorldPoint(new Vector3(xVP, yVP, 0f));
            posWS.z = 0;

            if (Random.Range(0f, 1f) > rateDifference)
            {
                enemySelected = 0;
            } else
            {
                enemySelected = 1;
            }
            
            for (int i = 0; i < spawnAmountList[enemySelected]; i++)
            {
                Instantiate(enemies[enemySelected], posWS + new Vector3(Random.Range(0f, 1.5f), Random.Range(0f, 1.5f), 0), Quaternion.identity);
            }

            spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
        } else if (spawnTimer > 0f)
        {
            spawnTimer -= Time.deltaTime;
        }
	}
}
