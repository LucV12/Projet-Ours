using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 5f;
    public float spawnDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", spawnTime, spawnDelay);
    }

    void spawn()
    {
        Instantiate(enemy, gameObject.transform.position, Quaternion.identity); ;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
