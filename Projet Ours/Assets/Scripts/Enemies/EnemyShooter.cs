using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float aggroRange;
    public float seeDistance;

    private float timeBtwShots;
    public float startTimeBtwshots;

    public GameObject projectile;
    public Transform player;
    bool executed;
    bool isRepusled;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;        
        timeBtwShots = startTimeBtwshots;
        isRepusled = GetComponent<Enemy>().repulsed;
        executed = GetComponent<Enemy>().executed;
    }

    // Update is called once per frame
    void Update()
    {

        executed = GetComponent<Enemy>().executed;

        if (executed == false && isRepusled == false)
        {
            animator.SetBool("IsStuned", false);


            if (Vector2.Distance(transform.position, player.position) > stoppingDistance  && Vector2.Distance(transform.position, player.position) < seeDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                animator.SetBool("IsMoving", true);
            }

            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
                animator.SetBool("IsMoving", false);
            }

            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                animator.SetBool("IsMoving", true);
            }

            if (timeBtwShots <= 0 && (Vector2.Distance(transform.position, player.position) < aggroRange))
            {
                animator.SetBool("IsShooting", true);
                Instantiate(projectile, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("TirManchot");
                timeBtwShots = startTimeBtwshots;                
            }

            else
            {
                timeBtwShots -= Time.deltaTime;
                animator.SetBool("IsShooting", false);
            }
        }

        if (executed == true)
        {
            animator.SetBool("IsStuned", true);
        }

        if (isRepusled == true)
        {
            animator.SetBool("IsStuned", true);
        }


    }
}