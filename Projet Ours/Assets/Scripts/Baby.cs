using System.Collections;
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

    GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        canMove = true;
        baby = GetComponent<Rigidbody2D>();
        nounours = GameObject.FindGameObjectWithTag("Player");
        player = nounours.transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Vector2.Distance(transform.position, player.position) < aggroPosition && isAttacking == false && canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
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
        canMove = false;
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);

        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnnemies);
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            playerToDamage[i].GetComponent<PlayerController>().health -= damage;
            Debug.Log("Player damaged !");
        }

        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        canMove = true;
    }
}
