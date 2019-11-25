using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private Rigidbody2D player;
    public GameObject crossHair;
    public float health;
    private TrailRenderer trail;
    GameObject nounours;

    [Header("Movement")]
    public float moveSpeed;
    public float rollTime;
    public float rollSpeed;
    public float rollDelay;
    bool canMove;
    bool trailActive = false;
    bool canRoll = true;

    [HideInInspector] public bool canLooseLife = true;

    [Header("Attack")]
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsGrabbable;
    public float attackRange;
    public int damage;
    private bool isAxisInUse = false;
    private bool isAxisInUseLeft = false;

    //public AnimationCurve curve;

    [Header("Rage")]
    public float rage;
    private bool rageAvaible;
    public bool rageActivated;
    public float startRageTime;
    private float rageTime;
    public float rageDecrease;
    public int rageDamageBoost;
    public int rageSpeedBoost;

    [Header("Capacities")]
    public float roarPushTime = 2f;
    public float roarPushSpeed = 2f;

    [Header("Collision and layers")]
    GameObject grabbableObject;
    public float enemyCheckDistance = 2f;
    public Collider2D CollStop;
    public LayerMask enemyLayer;
    public LayerMask EnviroLayer;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        canMove = true;
        trail = GetComponent<TrailRenderer>();
        nounours = GameObject.FindGameObjectWithTag("Player");
        rageActivated = false;
        rageAvaible = false;
    }

    void Update()
    {
        if (canMove == true) //Mouvement de base
        {
            MovePlayer();
        }

        if (Input.GetButton("Dash") && trailActive == false && canRoll == true)
        {
            StartCoroutine(Roll());
        }

        if (Input.GetButton("CapacityButton"))
        {
            //StartCoroutine(GetComponent<Kick>().KickActivable());
            GetComponent<Roar>().RoarActivable();
            //StartCoroutine(GetComponent<Tourbilol>().TourbilolActivable());
        }

        MoveCrossHair(); // Fontion pour viser

        if (timeBtwAttack <= 0) //Attaque
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
            animator.SetBool("isAttacking", false);
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

        if (rageAvaible == true && Input.GetButtonDown("Rage"))
        {
            RageActive1();
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

        Vector3 rollMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        RaycastHit2D EnviroHit = Physics2D.Raycast(transform.position, rollMovement, enemyCheckDistance, EnviroLayer);
        if (EnviroHit && EnviroHit.collider.gameObject.tag == "Enviro")
        {
            CollStop.enabled = true;
        }

    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
        {
            if (Input.GetAxisRaw("Grab") != 0)
            {
                if (isAxisInUseLeft == false)
                {
                  Grab();
                }
            }
            if (Input.GetAxisRaw("Grab") == 0)
            {
              isAxisInUseLeft = false;
            }
        }

        if (collision.gameObject.CompareTag("RoarItem"))
        {
            Debug.Log("COLLISION RoarITEM");

            if (Input.GetButtonDown("Y"))

            {
                Roar instance = GetComponent<Roar>();
                if (instance == null)
                {
                    nounours.AddComponent<Roar>();
                    Destroy(nounours.GetComponent<Kick>());
                    Destroy(nounours.GetComponent<Tourbilol>());
                }
                //Destroy(collision.gameObject);
            }                        
        }

        if (collision.gameObject.CompareTag("KickItem"))
        {
                Debug.Log("COLLISION KickITEM");

                if (Input.GetButtonDown("Y"))

                {
                    Kick instance = GetComponent<Kick>();
                    if (instance == null)
                    {
                        nounours.AddComponent<Kick>();
                        Destroy(nounours.GetComponent<Tourbilol>());
                        Destroy(nounours.GetComponent<Roar>());
                    }
                    //Destroy(collision.gameObject);
                }
        }

        if (collision.gameObject.CompareTag("TourbilolItem"))
        {
                    Debug.Log("COLLISION TourbilolITEM");

                    if (Input.GetButtonDown("Y"))
                    {
                        Tourbilol instance = GetComponent<Tourbilol>();
                        if (instance == null)
                        {
                            nounours.AddComponent<Tourbilol>();
                            Destroy(nounours.GetComponent<Kick>());
                            Destroy(nounours.GetComponent<Roar>());
                        }
                        //Destroy(collision.gameObject);
                    }
        }  
        
    }

    #region dash routines
    private IEnumerator Roll()   //Coroutine de la Roulade.
    {
        Vector3 rollMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 2f);
        canMove = false;
        canRoll = false;
        trailActive = true;

        RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, rollMovement, enemyCheckDistance, enemyLayer);
        if (enemyHit && enemyHit.collider.gameObject.tag == "Enemy")
        {
            CollStop.enabled = false;
        }  
            
        player.velocity = rollMovement * rollSpeed;
        trail.enabled = !trail.enabled;
        yield return new WaitForSeconds(rollTime);
        player.velocity = Vector2.zero;
        canMove = true;        
        trailActive = false;
        trail.enabled = !trail.enabled;
        CollStop.enabled = true;
        yield return new WaitForSeconds(rollDelay);
        canRoll = true;            
    }

    /*private IEnumerator RollWithCurve()
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
    }*/


    #endregion

    private void OnDrawGizmosSelected() //Visualisation de l'atk range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Vector3 rollMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 2f);
        Gizmos.DrawRay(transform.position, rollMovement);
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

    private void MovePlayer() //Fonction de déplacement
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        //transform.position = transform.position + movement * Time.deltaTime * moveSpeed;

        player.MovePosition(transform.position + movement.normalized * moveSpeed);
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

    private void RageActive1() //Fonction d'activable de l'effet de rage
    {
        rage = 0;
        rageAvaible = false;
        rageActivated = true;
        StartCoroutine(ColorChangeRage());
        Debug.Log("Rage Activée !!!");
        rageTime = startRageTime;
        
        damage = damage * rageDamageBoost;
        moveSpeed = moveSpeed * rageSpeedBoost;
    }

    private void Grab()
    {
        grabbableObject = GameObject.FindGameObjectWithTag("Grabbable");
        grabbableObject.GetComponent<Grabbable>().isGrabbed = true;
    }

    IEnumerator ColorChange()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator ColorChangeRage()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void HitByEnemy()
    {
        StartCoroutine(ColorChange());
    }

}
