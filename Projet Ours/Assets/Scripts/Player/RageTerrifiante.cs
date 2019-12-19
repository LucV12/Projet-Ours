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
    PlayerController pc;


    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        pc = nounours.GetComponent<PlayerController>();
        
    }
    public IEnumerator RageActive3 ()
    {
        terrorPos = nounours.transform;

        StartCoroutine(pc.ColorChangeRage());

        yield return new WaitForSeconds(1.1f);

        FindObjectOfType<AudioManager>().Play("TerrorRage");

        Collider2D[] enemyToTerrify = Physics2D.OverlapCircleAll(terrorPos.position, terrorRange, whatIsEnemies);
        for (int i = 0; i < enemyToTerrify.Length; i++)
        {
            enemyToTerrify[i].GetComponent<Enemy>().repulsed = true;
            enemyToTerrify[i].GetComponent<Enemy>().enemy.velocity = (enemyToTerrify[i].GetComponent<Enemy>().transform.position - pc.transform.position).normalized * terrorSpeed;
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
        Gizmos.DrawWireSphere(pc.transform.position, terrorRange);
    }
}
