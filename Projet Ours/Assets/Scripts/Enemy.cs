using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool executed;

    public GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        executed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if (executed == false)
            {
                nounours.GetComponent<PlayerController>().rage += 0.1f;
            }
            else if (executed == true)
            {
                nounours.GetComponent<PlayerController>().rage += 0.5f;
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage TAKEN !");
    }
}
