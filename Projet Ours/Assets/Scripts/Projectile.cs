using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{

    public float speed;
    public float damage;
    GameObject nounours;

    Transform player;
    Vector2 target;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        nounours = GameObject.FindGameObjectWithTag("Player");
        player = nounours.transform;
        target = new Vector2(player.position.x, player.position.y);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //PlayerController pc = nounours.GetComponent<PlayerController>();
            //pc.health -= damage;
            nounours.GetComponent<PlayerController>().health -= damage;
            DestroyProjectile();
            Debug.Log("HIT !");
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
