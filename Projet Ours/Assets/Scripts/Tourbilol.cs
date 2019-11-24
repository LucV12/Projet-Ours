using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbilol : MonoBehaviour
{

    float tourbiRange = 2f;
    public LayerMask whatIsEnemies;
    float tourbiDelay = 5f;
    GameObject nounours;
    Transform roarPos;

    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        roarPos = nounours.transform;
    }

    public IEnumerator TourbilolActivable ()
    {
        Collider2D[] enemyToPush = Physics2D.OverlapCircleAll(roarPos.position, tourbiRange, whatIsEnemies);
        for (int i = 0; i < enemyToPush.Length; i++)
        {
            enemyToPush[i].GetComponent<Enemy>().TakeDamage(1);
            StartCoroutine(enemyToPush[i].GetComponent<Enemy>().Repulsed());
            Debug.Log("Tourbilol !!!");
        }
        yield return new WaitForSeconds(tourbiDelay);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GetComponent<PlayerController>().transform.position, GetComponent<PlayerController>().tourbiRange);
    }
}
