﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Rigidbody2D kamikaze;
    Transform player;

    GameObject nounours;
    public Animator animator;

    public float normalSpeed;
    public float chargeSpeed;
    public float chargePosition;
    public float aggroRange;

    public GameObject blood;
    GameObject camera;
    public GameObject Flak2Sang;

    bool isDoingCharge;
    bool canMove;
    bool executed;
    bool isRepusled;
    bool canDash;

    public Transform explosionPos;
    public float explosionRange;
    public float explosionDelay;
    public LayerMask whatIsEnnemies;
    Vector3 targetChargePos;


    // Start is called before the first frame update
    void Start()
    {
        isDoingCharge = false;
        canMove = true;
        kamikaze = GetComponent<Rigidbody2D>();
        nounours = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.Find("Main Camera");
        isRepusled = GetComponent<Enemy>().repulsed;
    }

    // Update is called once per frame
    void Update()
    {
        player = nounours.transform;
        executed = GetComponent<Enemy>().executed;

        if (executed == false && isRepusled == false)
        {
            if (Vector2.Distance(transform.position, player.position) > chargePosition && isDoingCharge == false && canMove == true)
            {
                if (Vector2.Distance(transform.position, player.position) < aggroRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, normalSpeed * Time.deltaTime);
                    animator.SetBool("isMoving", true);
                }
                
            }
            else if (Vector2.Distance(transform.position, player.position) <= chargePosition)
            {
                if (isDoingCharge == false)
                {                    
                    isDoingCharge = true;                    
                    targetChargePos = player.position;
                }                
            }

            if (isDoingCharge == true && canMove == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetChargePos, chargeSpeed * Time.deltaTime);
                animator.SetBool("isGlissing", true);
                canDash = true;
            }

            if (transform.position == targetChargePos && canDash == true)
            {
                StartCoroutine(Explosion());
            }
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionPos.position, explosionRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            canMove = false;
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        animator.SetBool("isBooming", true);
        animator.SetBool("isGlissing", false);
        FindObjectOfType<AudioManager>().PlayThis("Explosion");
        canMove = false;
        yield return new WaitForSeconds(explosionDelay);
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(explosionPos.position, explosionRange, whatIsEnnemies);
        for (int i = 0; i < playerToDamage.Length; i++)          
            
        {
            playerToDamage[i].GetComponent<PlayerController>().health -= 2;
        }

        Instantiate(blood, transform.position, Quaternion.identity);
        Instantiate(Flak2Sang, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
