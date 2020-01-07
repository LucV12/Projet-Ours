using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemy;
    public float spawnTime;
    public int[] enemyCounts = new int[3];
    private int enemyCounter;
    private bool mustStartNewWave;
    private int waveIndex;
    GameObject gameManager;
    GameManager GM;
    public GameObject[] lootAvaible = new GameObject[4];
    public GameObject spawnLoot;
    GameObject currentLoot;
    int lootIndex;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        mustStartNewWave = true;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();
        lootIndex = UnityEngine.Random.Range (0, lootAvaible.Length);
        currentLoot = lootAvaible[lootIndex];
    }
    
    void GenerateWave(int enemyCount)
    {

        for (int i = 0; i < enemyCount; i++)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            int enemyIndex = UnityEngine.Random.Range(0, enemy.Length);
            /*Enemy nmi*/GameObject goTmp = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);//.GetComponent<Enemy>() as Enemy;
            //nmi.transform.parent = this.transform;
            //nmi.init(this);
            goTmp.transform.parent = transform;
            goTmp.GetComponent<Enemy>().init(this);
        }
        enemyCounter = enemyCount;
        if (waveIndex < 3)
            waveIndex++;
        else
            waveIndex = 2;
    }


    public void onEnemyDeath()
    {
        enemyCounter--;
        if (enemyCounter == 0 && waveIndex < enemyCounts.Length)
        {
            StartCoroutine(waveDelay());
        }
        else
        {
            SpawnLoot();
        }
    }

    private IEnumerator waveDelay()
    {
        yield return new WaitForSeconds(3);
        mustStartNewWave = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustStartNewWave == true)
        {
            GenerateWave(enemyCounts[waveIndex]);
            mustStartNewWave = false;
        }

    }

    public void SpawnLoot()
    {
        Instantiate(currentLoot, spawnLoot.transform);
    }
}
