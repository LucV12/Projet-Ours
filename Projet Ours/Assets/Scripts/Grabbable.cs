using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{

    public bool isGrabbed = false;
    Transform player;
    GameObject nounours;
    [SerializeField] Rigidbody2D grabbableObject;
    bool isAxisInUseLeft = false;
    public float pouissance2FEU;
    public float throwTime;
    public BoxCollider2D colliderHit;
    public bool isExplosive;
    public LayerMask whatIsEnemies;
    public float explosionRange;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        player = nounours.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed == true)
        {

            Vector3 moveGrab = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);
            transform.position = player.position + moveGrab * 0.25f;

            if(Input.GetAxisRaw("Grab") == 0 && isGrabbed == true)
            {                
                    StartCoroutine(ThrowThings(moveGrab));               
            }
            if (Input.GetAxisRaw("Grab") == 0)
            {
                isAxisInUseLeft = false;
            }
        }
    }

    private IEnumerator ThrowThings(Vector3 moveGrab)
    {        
        isGrabbed = false;
        grabbableObject.velocity = moveGrab.normalized * pouissance2FEU;
        colliderHit.enabled = true;
        yield return new WaitForSeconds(throwTime);
        grabbableObject.velocity = Vector3.zero;
        colliderHit.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            if (isExplosive == true)
            {
                Collider2D[] enemiesToBlow = Physics2D.OverlapCircleAll(transform.position, explosionRange, whatIsEnemies);
                for (int i = 0; i < enemiesToBlow.Length; i++)
                {
                    enemiesToBlow[i].GetComponent<Enemy>().TakeDamage(1);
                }
                Destroy(gameObject);
            }
            grabbableObject.velocity = grabbableObject.velocity * 0f;
            colliderHit.enabled = false;
        }
    }
}
