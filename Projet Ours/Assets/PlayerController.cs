using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D player;

    public float moveSpeed;
    public float rollTime;
    public float rollSpeed;
    bool canMove;

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

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Roll());
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

        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.velocity = Vector2.left * rollSpeed;
            canMove = false;               //Si on laisse la possibilité de se déplacer pendant le dash, le déplacement va annuler cette dernière.
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player.velocity = Vector2.right * rollSpeed;
            canMove = false;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            player.velocity = Vector2.up * rollSpeed;
            canMove = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player.velocity = Vector2.down * rollSpeed;
            canMove = false;
        }
        yield return new WaitForSeconds(rollTime);
        player.velocity = Vector2.zero;
        canMove = true;*/
    }
}
