using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Rigidbody2D kamikaze;
    public Transform player;

    public float normalSpeed;
    public float chargeSpeed;
    public float chargePosition;

    bool isDoingCharge;
    bool canMove;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > chargePosition && isDoingCharge == false && canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, normalSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) <= chargePosition)
        {
            if (isDoingCharge == false)
            {
                isDoingCharge = true;
                targetChargePos = player.position;
            }
            else if (isDoingCharge == true && canMove == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetChargePos, chargeSpeed * Time.deltaTime);
                if (transform.position == targetChargePos)
                {
                    StartCoroutine(Explosion());
                }
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
            kamikaze.position = Vector2.zero;
            canMove = false;
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(explosionPos.position, explosionRange, whatIsEnnemies);
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            playerToDamage[i].GetComponent<PlayerController>().health = 0;
        }
        Destroy(gameObject);
    }
}
