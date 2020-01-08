using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hachoir : MonoBehaviour
{
    public float damage;
    bool canLooseLife;

    GameObject nounours;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        canLooseLife = nounours.GetComponent<PlayerController>().canLooseLife;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (canLooseLife == true)
            {
                nounours.GetComponent<PlayerController>().health -= damage;
                Debug.Log("HIT !");
                nounours.GetComponent<PlayerController>().HitByEnemy();
            }
        }
    }
}