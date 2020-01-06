using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D player;
    public GameObject crossHair;
    public float health;
    private TrailRenderer trail;
    GameObject nounours;
    public GameObject slashParticle;
    private GameObject UI;
    ScenesManager SM;
    public Vector3 InitPosition;
    PauseMenu PM;
    GameObject gameOver;
    GameOverScript GOS;
    GameObject gameManager;
    GameManager GM;

    [Header("Movement")]
    public float moveSpeed;
    public float rollTime;
    public float rollSpeed;
    public float rollDelay;
    public bool canMove;
    public bool trailActive = false;
    public bool canRoll = true;
    public AnimationCurve curve;

    [HideInInspector] public bool canLooseLife = true;

    [Header("Attack")]   
    public float startTimeBtwAttack;
    private float timeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsGrabbable;
    public float attackRange;
    public int damage;
    private bool isAxisInUse = false;
    private bool isAxisInUseLeft = false;    

    [Header("Rage")]
    public float rage;
    public bool rageAvaible;
    public bool rageActivated;
    public float startRageTime;
    private float rageTime;
    public float rageDecrease;
    public int rageDamageBoost;
    public int rageSpeedBoost;
    public GameObject[] rages;

    [Header("Capacities")]
    public float roarPushTime = 2f;
    public float roarPushSpeed = 2f;
    public float startActiveDelay = 2f;
    public GameObject[] capacities;

    [Header("Collision and layers")]
    public float enemyCheckDistance = 2f;
    GameObject grabbableObject;    
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
        UI = GameObject.FindGameObjectWithTag("UI");
        SM = GameObject.FindGameObjectWithTag("ScenesManager").GetComponent<ScenesManager>();
        InitPosition = Vector3.zero;
        PM = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        gameOver = GameObject.FindGameObjectWithTag("GameOver");
        GOS = gameOver.GetComponent<GameOverScript>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {

        if(PM.GameIsPaused == false)
        {
            MoveCrossHair();
        }

        if (canMove == true && PM.GameIsPaused == false) //Mouvement de base
        {
            MovePlayer();
        }

        if (Input.GetButton("Dash") && trailActive == false && canRoll == true)
        {
            StartCoroutine(Roll());
            FindObjectOfType<AudioManager>().Play("Roulade");
            StartCoroutine(UI.GetComponent<ActiveAndRollUIScript>().rollUICooldown());
        }

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
            GOS.GameOver();              
        }


        if (rage < 1 && rage > 0)
        {
            rage -= rageDecrease;
        }
        else if (rage >= 1)
        {
            rageAvaible = true;
        }

        if (rageActivated == true)
        {
            rageTime -= Time.deltaTime;
        }

        if (rageTime <= 0 && rageActivated == true)
        {
            rageActivated = false;
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
            grabbableObject = collision.gameObject;

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

            if (GM.capaAvaible.Contains("Roar") == false)
            {
                GM.capaAvaible.Add("Roar");
            }
            
            if (Input.GetButtonDown("Y"))

            {
                capacities[0].SetActive(false);
                capacities[1].SetActive(true);
                capacities[2].SetActive(false);

                UI.GetComponent<ActiveAndRollUIScript>().activeSprites[0].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().activeSprites[1].SetActive(true);
                UI.GetComponent<ActiveAndRollUIScript>().activeSprites[2].SetActive(false);
            }                        
        }

        if (collision.gameObject.CompareTag("KickItem"))
        {
                Debug.Log("COLLISION KickITEM");

                if (GM.capaAvaible.Contains("Kick") == false)
                {
                    GM.capaAvaible.Add("Kick");                    
                }

            if (Input.GetButtonDown("Y"))

            {
                 capacities[0].SetActive(true);
                 capacities[1].SetActive(false);
                 capacities[2].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().activeSprites[0].SetActive(true);
                UI.GetComponent<ActiveAndRollUIScript>().activeSprites[1].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().activeSprites[2].SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("TourbilolItem"))
        {
                    Debug.Log("COLLISION TourbilolITEM");

            if (GM.capaAvaible.Contains("Tourbi") == false)
            {
                GM.capaAvaible.Add("Tourbi");
            }

            if (Input.GetButtonDown("Y"))
                    {
                        capacities[0].SetActive(false);
                        capacities[1].SetActive(false);
                        capacities[2].SetActive(true);

                        UI.GetComponent<ActiveAndRollUIScript>().activeSprites[0].SetActive(false);
                        UI.GetComponent<ActiveAndRollUIScript>().activeSprites[1].SetActive(false);
                        UI.GetComponent<ActiveAndRollUIScript>().activeSprites[2].SetActive(true);
                    }
        }

        if (collision.gameObject.CompareTag("BearserkItem"))
        {
            Debug.Log("COLLISION BearserkITEM");

            if (GM.rageAvaible.Contains("Bearserk") == false)
            {
                GM.rageAvaible.Add("Bearserk");
            }

            if (Input.GetButtonDown("Y"))

            {
                rages[0].SetActive(true);
                rages[1].SetActive(false);
                rages[2].SetActive(false);

                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[0].SetActive(true);
                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[1].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[2].SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("RueeItem"))
        {
            Debug.Log("COLLISION RueeITEM");

            if (GM.rageAvaible.Contains("Ruee") == false)
            {
                GM.rageAvaible.Add("Ruee");
            }

            if (Input.GetButtonDown("Y"))

            {
                rages[0].SetActive(false);
                rages[1].SetActive(true);
                rages[2].SetActive(false);

                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[0].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[1].SetActive(true);
                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[2].SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("TerriItem"))
        {
            Debug.Log("COLLISION TerriITEM");

            if (GM.rageAvaible.Contains("Terri") == false)
            {
                GM.rageAvaible.Add("Terri");
            }

            if (Input.GetButtonDown("Y"))
            {
                rages[0].SetActive(false);
                rages[1].SetActive(false);
                rages[2].SetActive(true);

                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[0].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[1].SetActive(false);
                UI.GetComponent<ActiveAndRollUIScript>().rageSprites[2].SetActive(true);
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

        animator.SetBool("IsRolling", true);
        yield return new WaitForSeconds(0.05f);
        player.velocity = rollMovement * rollSpeed;
        trail.enabled = !trail.enabled;
        yield return new WaitForSeconds(rollTime);
        animator.SetBool("IsRolling", false);
        player.velocity = Vector2.zero;
        canMove = true;        
        trailActive = false;
        trail.enabled = !trail.enabled;
        CollStop.enabled = true;
        yield return new WaitForSeconds(rollDelay);
        canRoll = true;            
    }

    private IEnumerator RollWithCurve()
    {
        float timer = 0.0f;

        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);
        canMove = false;

        while (timer < rollTime)
        {
            player.velocity = aim.normalized * (rollSpeed * curve.Evaluate(timer / rollTime));

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

        player.MovePosition(transform.position + movement.normalized * moveSpeed);
    }

    private void Attack() //Fonction de l'attaque
    {
        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);

        timeBtwAttack = startTimeBtwAttack;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        if (enemiesToDamage.Length > 0 && enemiesToDamage[0].gameObject.tag == "Enemy")
        {
            enemiesToDamage[0].GetComponent<Enemy>().TakeDamage(damage);
            Instantiate(slashParticle, crossHair.transform.position, Quaternion.identity);
        }

        Debug.Log(enemiesToDamage.Length);
        Debug.Log("AtK");
        StartCoroutine(RollWithCurve());
        isAxisInUse = true;

        
        animator.SetFloat("AimHorizontal", aim.x);
        animator.SetFloat("AimVertical", aim.y);
        animator.SetBool("isAttacking", true);
    }

    private void Grab()
    {
        //grabbableObject = GameObject.FindGameObjectWithTag("Grabbable");
        grabbableObject.GetComponent<Grabbable>().isGrabbed = true;
    }

    IEnumerator ColorChange()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;

    }

    public IEnumerator ColorChangeRage()
    {
        animator.SetBool("IsRageActivated", true);
        canMove = false;
        FindObjectOfType<AudioManager>().Play("EnclenchementRage");
        //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1.1f);
        FindObjectOfType<AudioManager>().Play("RageBoom");
        //gameObject.GetComponent<Renderer>().material.color = Color.white;
        animator.SetBool("IsRageActivated", false);
        canMove = true;
    }

    public void HitByEnemy()
    {
        StartCoroutine(ColorChange());
        FindObjectOfType<AudioManager>().Play("HitByEnemy");
    }

}
