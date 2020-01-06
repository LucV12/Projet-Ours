using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinata : MonoBehaviour
{
    public float health;
    public Animator animator;
    public Rigidbody2D enemy;
    public bool isKicked;
    public bool executed;
    public bool newPinata;
    bool animEnCours;
    GameObject nounours;
    public GameObject camera;
    Transform initialPos;
    public GameObject pinata;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        executed = false;
        isKicked = false;
        nounours = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.Find("Main Camera");
        enemy = GetComponent<Rigidbody2D>();
        initialPos = transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            nounours.GetComponent<PlayerController>().rage += 1;
            StartCoroutine(NewPinata());
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
    }

    public void TakeDamage(int damage, Vector3 aim)
    {
        if (executed == true)
        {
            health -= damage * 5;
            FindObjectOfType<AudioManager>().Play("Execution");
            Debug.Log("EXECUTED !");
        }


        Debug.Log("hitComeFrom " + aim);
        if (animEnCours == false)
        {
            StartCoroutine(HitAnim(aim));
        }

        health -= damage;
        FindObjectOfType<AudioManager>().Play("Frappe");
        Debug.Log("damage TAKEN !");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Grabbable") && other.gameObject.GetComponent<Grabbable>().isExplosive == false)
        {
            executed = true;
        }

        if (other.collider.CompareTag("Enviro") && isKicked == true)
        {
            executed = true;
            FindObjectOfType<AudioManager>().Play("StunContreMur");
        }
    }

    IEnumerator NewPinata()
    {
        health = 5;
        newPinata = true;
        Debug.Log("Pinata off");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);

    }

    IEnumerator HitAnim(Vector3 aim)
    {
        animEnCours = true;
        animator.SetBool("PinataHit", true);
        animator.SetFloat("AimHorizontal", aim.x);
        animator.SetFloat("AimVertical", aim.y);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("PinataHit", false);
        animEnCours = false;
    }
}
