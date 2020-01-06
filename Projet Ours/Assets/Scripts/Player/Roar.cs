using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : MonoBehaviour
{

    float roarRange = 1f;
    float roarDelay = 2f;
    LayerMask whatIsEnemies;
    Transform roarPos;
    GameObject nounours;
    LayerMask whatIsBullets;
    Animator animator;

    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        roarPos = nounours.transform;
        whatIsEnemies = LayerMask.GetMask("Enemy");
        whatIsBullets = LayerMask.GetMask("Projectile");
        animator = nounours.GetComponent<PlayerController>().animator;
    }

    public void RoarActivable()  //Fonction du Rugissement
    {
        StartCoroutine(RoarAnimation());
        Collider2D[] bulletsToDestroy = Physics2D.OverlapCircleAll(roarPos.position, roarRange, whatIsBullets);
        for (int y = 0; y < bulletsToDestroy.Length; y++)
        {
            bulletsToDestroy[y].GetComponent<Projectile>().DestroyProjectile();
        }

        Collider2D[] enemyToPush = Physics2D.OverlapCircleAll(roarPos.position, roarRange, whatIsEnemies);
        for (int i = 0; i < enemyToPush.Length; i++)
        {
            if (enemyToPush[i].CompareTag("Enemy"))
            {
                StartCoroutine(enemyToPush[i].GetComponent<Enemy>().Repulsed());
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(roarPos.position, roarRange);
    }

    private IEnumerator RoarAnimation()
    {
        animator.SetBool("IsRoaring", true);
        nounours.GetComponent<PlayerController>().canMove = false;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("IsRoaring", false);
        nounours.GetComponent<PlayerController>().canMove = true;
    }
}
