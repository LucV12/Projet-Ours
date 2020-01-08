﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    Rigidbody2D baby;
    Transform player;
    bool isAttacking;
    bool canMove;
    public float moveSpeed;
    public float aggroPosition;
    public Transform attackPosition;
    public float attackRange;
    public float attackDuration;
    public float attackDelay;
    public LayerMask whatIsEnnemies;
    public float damage;
    public Animator animator;
    bool isRepusled;

    bool executed;

    GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        canMove = true;
        baby = GetComponent<Rigidbody2D>();
        nounours = GameObject.FindGameObjectWithTag("Player");
        player = nounours.transform;
        isRepusled = GetComponent<Enemy>().repulsed;
        executed = GetComponent<Enemy>().executed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsMoving", false);

        if (executed == false && isRepusled == false)
        {
            if (Vector2.Distance(transform.position, player.position) < aggroPosition && isAttacking == false && canMove == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                animator.SetBool("IsMoving", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    IEnumerator Attack()
    {
        animator.SetBool("IsAttacking", true);
        canMove = false;
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);

        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnnemies);
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            if (nounours.GetComponent<PlayerController>().canLooseLife == true)
            {
                playerToDamage[i].GetComponent<PlayerController>().health -= damage;
                Debug.Log("Player damaged !");
                nounours.GetComponent<PlayerController>().HitByEnemy();
            }
        }

        yield return new WaitForSeconds(attackDelay);
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
        canMove = true;
    }
}
