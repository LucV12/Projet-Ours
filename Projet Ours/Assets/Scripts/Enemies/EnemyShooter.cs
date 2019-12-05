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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;        
        timeBtwShots = startTimeBtwshots;
        isRepusled = GetComponent<Enemy>().repulsed;
    }

    // Update is called once per frame
    void Update()
    {
        executed = GetComponent<Enemy>().executed;

        if (executed == false && isRepusled == false)
        {

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance  && Vector2.Distance(transform.position, player.position) < seeDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }

            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0 && (Vector2.Distance(transform.position, player.position) < aggroRange))
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwshots;
            }

            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}