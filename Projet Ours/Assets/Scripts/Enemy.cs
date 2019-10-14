using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public bool executed;

    GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        executed = false;
        nounours = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health <= 0)
        {
            if (executed == false && nounours.GetComponent<PlayerController>().rageActivated == false)
            {
                nounours.GetComponent<PlayerController>().rage += 1f;
            }
            else if (executed == true && nounours.GetComponent<PlayerController>().rageActivated == false)
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
