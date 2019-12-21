using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbilol : MonoBehaviour
{

    float tourbiRange = 1f;
    LayerMask whatIsEnemies;
    float tourbiDelay = 2f;
    GameObject nounours;
    Transform tourbiPos;
    Animator animator;

    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        tourbiPos = nounours.transform;
        whatIsEnemies = LayerMask.GetMask("Enemy");
        animator = nounours.GetComponent<PlayerController>().animator;
    }

    public IEnumerator TourbilolActivable()
    {
        animator.SetBool("IsTourbing", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsTourbing", false);

        Collider2D[] enemyToPush = Physics2D.OverlapCircleAll(tourbiPos.position, tourbiRange, whatIsEnemies);
        for (int i = 0; i < enemyToPush.Length; i++)
        {
            enemyToPush[i].GetComponent<Enemy>().TakeDamage(1);
            StartCoroutine(enemyToPush[i].GetComponent<Enemy>().Repulsed());
            Debug.Log("Tourbilol !!!");
        }
        yield return new WaitForSeconds(tourbiDelay);
        animator.SetBool("IsTourbing", false);


    }

    private void OnDrawGizmosSelected()
    {
        /*Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(tourbiPos.position, tourbiRange);*/
    }
}
