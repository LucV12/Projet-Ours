using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private Rigidbody2D player;
    public GameObject crossHair;
    public float health;

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
    private bool isAxisInUse = false;

    public AnimationCurve curve;

    public float rage;
    private bool rageAvaible;
    public bool rageActivated;
    public float startRageTime;
    private float rageTime;
    public float rageDecrease;
    public int rageDamageBoost;
    public int rageSpeedBoost;



    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        canMove = true;

        rageActivated = false;
        rageAvaible = false;
    }

    void Update()
    {
        if (canMove == true) //Mouvement de base
        {
            MovePlayer();
        }

        if (Input.GetButton("Dash"))
        {
            StartCoroutine(Roll());
        }

        MoveCrossHair(); // Fontion pour viser

        if (timeBtwAttack <= 0) //Attaque suivant le temps entre les attaques
        {
            if (Input.GetAxisRaw("Attack") != 0)
            {
                if (isAxisInUse == false)
                {
                    Attack();
                }
            }

            if (Input.GetAxisRaw("Attack") == 0)
            {
                isAxisInUse = false;
                animator.SetBool("isAttacking", false);  
            }           
        }

        else
        {
            timeBtwAttack -= Time.deltaTime; 
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (rage < 1 && rage > 0)
        {
            rage -= rageDecrease;
        }
        else if (rage >= 1)
        {
            rageAvaible = true;
        }

        if (rageAvaible == true && Input.GetKeyDown(KeyCode.A))
        {
            rage = 0;
            rageAvaible = false;
            rageActivated = true;
            Debug.Log("Rage Activée !!!");
            rageTime = startRageTime;
            damage = damage * rageDamageBoost;
            moveSpeed = moveSpeed * rageSpeedBoost;
        }

        if (rageActivated == true)
        {
            rageTime -= Time.deltaTime;
        }

        if (rageTime <= 0 && rageActivated == true)
        {
            rageActivated = false;
            damage = damage / rageDamageBoost;
            moveSpeed = moveSpeed / rageSpeedBoost;
        }
    }

    #region dash routines
    private IEnumerator Roll()   //C'est le code de la Roulade.
    {

        Vector3 rollMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        canMove = false;
        player.velocity = rollMovement * rollSpeed;
        yield return new WaitForSeconds(rollTime);
        player.velocity = Vector2.zero;
        canMove = true;

    }

    private IEnumerator RollWithCurve()
    {
        float timer = 0.0f;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        canMove = false;

        while (timer < rollTime)
        {
            player.velocity = direction.normalized * (rollSpeed * curve.Evaluate(timer / rollTime));

            timer += Time.deltaTime;
            yield return null;
        }

        player.velocity = Vector2.zero;
        canMove = true;
    }
    #endregion

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
            aim *= 0.25f;
            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

    private void MovePlayer () //Fonction de déplacement
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
    }

    private void Attack() //Fonction de l'attaque
    {
        timeBtwAttack = startTimeBtwAttack;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        if (enemiesToDamage.Length > 0 && enemiesToDamage[0].gameObject.tag == "Enemy")
        {
            enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(damage);
        }

        Debug.Log(enemiesToDamage.Length);
        Debug.Log("AtK");
        isAxisInUse = true;

        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);
        animator.SetFloat("AimHorizontal", aim.x);
        animator.SetFloat("AimVertical", aim.y);
        animator.SetBool("isAttacking", true);
    }
}
