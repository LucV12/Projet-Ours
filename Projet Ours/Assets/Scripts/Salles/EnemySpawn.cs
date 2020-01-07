using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemy;
    public float spawnTime;
    public int enemyCount;
    Enemy enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawn", spawnTime);
    }

    void spawn()
    { 
        for (int i = 0; i < enemyCount; i ++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int enemyIndex = Random.Range(0, enemy.Length);
            GameObject enemyCreation = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            enemyCreation.transform.parent = this.transform;
        }
        
    }

}
