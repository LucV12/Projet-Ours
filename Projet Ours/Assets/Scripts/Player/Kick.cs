using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    float kickPower = 2f;
    float kickPushTime = 2f;
    LayerMask whatIsEnemies;
    float attackRange = 0.5f;
    Transform attackPos;
    GameObject nounours;
    Animator animator;



    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        attackPos = nounours.transform;
        whatIsEnemies = LayerMask.GetMask("Enemy");
        animator = nounours.GetComponent<PlayerController>().animator;
    }


    public IEnumerator KickActivable()  //Fonction du coup de pied
    {
        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);

        animator.SetFloat("Horizontal", aim.x);
        animator.SetFloat("Vertical", aim.y);
        animator.SetBool("IsKicking", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsKicking", false);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        if (enemiesToDamage.Length > 0 && enemiesToDamage[0].gameObject.tag == "Enemy")
        {
            Debug.Log("Kicked!");
            enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(1);
            enemiesToDamage[0].GetComponent<Enemy>().enemy.velocity = aim * kickPower;
            enemiesToDamage[0].GetComponent<Enemy>().isKicked = true;
            yield return new WaitForSeconds(kickPushTime);            
            enemiesToDamage[0].GetComponent<Enemy>().enemy.velocity = Vector2.zero;
            enemiesToDamage[0].GetComponent<Enemy>().isKicked = false;
            animator.SetBool("IsKicking", false);
        }

        

    }
}

