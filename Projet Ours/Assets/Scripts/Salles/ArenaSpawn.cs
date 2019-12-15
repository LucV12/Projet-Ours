using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemy;
    public float spawnTime;
    public int[] enemyCounts;
    private int enemyCounter;
    private bool mustStartNewWave;
    private int waveIndex;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        mustStartNewWave = true;
    }
    
    void GenerateWave(int enemyCount)
    {

        for (int i = 0; i < enemyCount; i++)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            int enemyIndex = UnityEngine.Random.Range(0, enemy.Length);
            Enemy nmi = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation).GetComponent<Enemy>();
            nmi.init(this);
        }
        enemyCounter = enemyCount;
        waveIndex++;
    }


    public void onEnemyDeath()
    {
        enemyCounter--;
        if (enemyCounter == 0 && waveIndex < enemyCounts.Length)
        {
            StartCoroutine(waveDelay());
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
        if (mustStartNewWave)
        {
            GenerateWave(enemyCounts[waveIndex]);
            mustStartNewWave = false;
        }

    }
}
