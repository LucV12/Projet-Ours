using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemy : MonoBehaviour
{
    private ArenaSpawn arenaSpawn;
    GameObject arena;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        arena = GameObject.Find("Arène + entrée(Clone)");
        arenaSpawn = arena.GetComponent<ArenaSpawn>();
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ennemi life " + enemy.health);
        /*if (enemy == null)
        {
            arenaSpawn.onEnemyDeath();
        }*/
    }


    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.1f);
        //enemy = this.gameObject.GetComponent<Enemy>();
    }

    public void ArenaDeath()
    {
        arenaSpawn.onEnemyDeath();
    }

}
