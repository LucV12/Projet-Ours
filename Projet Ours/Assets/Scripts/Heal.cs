using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float heal;
    GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (nounours.GetComponent<PlayerController>().health <= 8)
            {
                nounours.GetComponent<PlayerController>().health += heal;
                Debug.Log("Heal !");

                DestroyFish();
            }
        }

        void DestroyFish()
        {
            Destroy(gameObject);
        }
    }
}
