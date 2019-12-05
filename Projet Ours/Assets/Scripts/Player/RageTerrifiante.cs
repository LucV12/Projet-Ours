using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageTerrifiante : MonoBehaviour
{
    Transform terrorPos;
    public float terrorRange;
    public LayerMask whatIsEnemies;
    public float terrorSpeed;
    public float terrorTime;
    GameObject nounours;

    public IEnumerator RageActive3 ()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        terrorPos = nounours.transform;

        StartCoroutine(GetComponent<PlayerController>().ColorChangeRage());

        Collider2D[] enemyToTerrify = Physics2D.OverlapCircleAll(terrorPos.position, terrorRange, whatIsEnemies);
        for (int i = 0; i < enemyToTerrify.Length; i++)
        {
            enemyToTerrify[i].GetComponent<Enemy>().repulsed = true;
            enemyToTerrify[i].GetComponent<Enemy>().enemy.velocity = (enemyToTerrify[i].GetComponent<Enemy>().transform.position - GetComponent<PlayerController>().transform.position).normalized * terrorSpeed;
        }
        yield return new WaitForSeconds(terrorTime);
        for (int i = 0; i < enemyToTerrify.Length; i++)
        {
            enemyToTerrify[i].GetComponent<Enemy>().repulsed = false;
            enemyToTerrify[i].GetComponent<Enemy>().enemy.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(GetComponent<PlayerController>().transform.position, terrorRange);
    }
}
