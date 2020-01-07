using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemy : MonoBehaviour
{
    private ArenaSpawn arenaSpawn;
    GameObject arena;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        arena = GameObject.Find("Arène + entrée");
        arenaSpawn = arena.GetComponent<ArenaSpawn>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.health == 0)
        {
            arenaSpawn.onEnemyDeath();
        }
    }
}
