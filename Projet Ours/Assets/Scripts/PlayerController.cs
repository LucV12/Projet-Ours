using System.Collections;
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


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    void Update()
    {
        if (canMove == true) //Mouvement de base
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);

            transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
        }

        if (Input.GetButton("Dash"))
        {
            StartCoroutine(Roll());
        }

        MoveCrossHair(); // Fontion pour viser

        if (timeBtwAttack <= 0) //Attaque suivant le temps entre les attaques
        {
            if (Input.GetButtonDown("Attack"))
            {
                timeBtwAttack = startTimeBtwAttack;
                Collider2D [] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                 if (enemiesToDamage[0].gameObject.tag == "Enemy")
                 {

                     enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(damage); 
                  }

                Debug.Log(enemiesToDamage.Length);
                Debug.Log("AtK");
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime; 
        }
    }

    IEnumerator Roll()   //C'est le code de la Roulade.
    {

        Vector3 rollMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        canMove = false;
        player.velocity = rollMovement * rollSpeed;
        yield return new WaitForSeconds(rollTime);
        player.velocity = Vector2.zero;
        canMove = true;

    }

    private void OnDrawGizmosSelected() //Visualisation de l'atk range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void MoveCrossHair() //Fonction de visée
    {
        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            aim *= 0.5f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

}
