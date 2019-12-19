using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D enemy;
    public float health;
    public bool executed;
    [HideInInspector] public bool repulsed;
    public GameObject blood;
    public GameObject camera;
    public GameObject Flak2Sang;
    public bool isKicked;
    private ArenaSpawn arenaSpawn;

    GameObject nounours;

    public void init(ArenaSpawn _arenaSpawn)
    {
        arenaSpawn = _arenaSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        repulsed = false;
        executed = false;
        isKicked = false;
        nounours = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.Find("Main Camera");
        enemy = GetComponent<Rigidbody2D>();
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
                FindObjectOfType<AudioManager>().Play("MortManchot");
            }
            else if (executed == true && nounours.GetComponent<PlayerController>().rageActivated == false)
            {
                nounours.GetComponent<PlayerController>().rage += 0.5f;
            }

            Instantiate(blood, transform.position, Quaternion.identity);
            camera.GetComponent<CameraShaker>().Shake();
            Instantiate(Flak2Sang, transform.position, Quaternion.identity);
            //arenaSpawn.onEnemyDeath();
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        if (executed == true)
        {
            health -= damage * 5;
            FindObjectOfType<AudioManager>().Play("Execution");
            Debug.Log("EXECUTED !");
        }

        health -= damage;
        FindObjectOfType<AudioManager>().Play("Frappe");
        Debug.Log("damage TAKEN !");
        StartCoroutine(colorChange());       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Grabbable"))
        {
            executed = true;
        }

        if (other.collider.CompareTag("Enviro") && isKicked == true)
        {
            executed = true;
            FindObjectOfType<AudioManager>().Play("StunContreMur");
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
        //gameObject.GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(2.5f);
        //gameObject.GetComponent<Renderer>().material.color = Color.white;
        executed = false;
    }

    public IEnumerator Repulsed()
    {
        enemy.velocity = (transform.position - nounours.transform.position).normalized * nounours.GetComponent<PlayerController>().roarPushSpeed;
        repulsed = true;
        yield return new WaitForSeconds(nounours.GetComponent<PlayerController>().roarPushTime);
        repulsed = false;
        enemy.velocity = Vector2.zero;
        Debug.Log("Roarred !!!");
    }

    private void DeathScream()
    {
        FindObjectOfType<AudioManager>().Play("MortManchot");
    }
}
