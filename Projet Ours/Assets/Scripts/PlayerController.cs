using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D player;
    public GameObject crossHair;

    public float moveSpeed;
    public float rollTime;
    public float rollSpeed;
    bool canMove;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            /*animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);*/

            transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
        }

        if (Input.GetButton("Fire"))
        {
            StartCoroutine(Roll());
        }

        MoveCrossHair();
    }

    IEnumerator Roll()   //C'est le code de la Roulade.
    {

        Vector3 rollMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        canMove = false;
        player.velocity = rollMovement * rollSpeed;
        yield return new WaitForSeconds(rollTime);
        player.velocity = Vector2.zero;
        canMove = true;

       /* if(timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Attack"))
            {

            }
        }*/
    }

    private void MoveCrossHair()
    {
        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            aim *= 15f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

    private void Attack()
    {

    }

}
