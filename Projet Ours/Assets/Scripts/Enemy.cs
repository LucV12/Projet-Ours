using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public bool executed;
    public GameObject blood;
    public GameObject camera;
    public GameObject Flak2Sang;

    GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        executed = false;
        nounours = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if (executed == true)
        {
            StartCoroutine(executionReady());
        }

        if (health <= 0)
        {
            if (executed == false && nounours.GetComponent<PlayerController>().rageActivated == false)
            {
                nounours.GetComponent<PlayerController>().rage += 0.2f;
            }
            else if (executed == true && nounours.GetComponent<PlayerController>().rageActivated == false)
            {
                nounours.GetComponent<PlayerController>().rage += 0.5f;
            }

            Instantiate(blood, transform.position, Quaternion.identity);
            camera.GetComponent<CameraShaker>().Shake();
            Instantiate(Flak2Sang, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        if (executed = true)
        {
            health -= damage * 5;
            Debug.Log("EXECUTED !");
        }

        health -= damage;
        Debug.Log("damage TAKEN !");
        StartCoroutine(colorChange());       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Grabbable"))
        {
            executed = true;
        }
    }

    IEnumerator colorChange()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator executionReady()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        executed = false;
    }
}
