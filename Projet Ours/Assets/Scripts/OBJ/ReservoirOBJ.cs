using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirOBJ : MonoBehaviour
{
    public float HP;
    public bool Destroyed2 = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 5 && HP > 0)
        {
            animator.SetBool("IsHarmed", true);
        }

        if (HP <= 0)
        {
            Destroyed2 = true;
            animator.SetBool("IsKilled", true);
            animator.SetBool("IsHarmed", false);
        }

    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log("damage TAKEN !");
        StartCoroutine(ColorChange());
    }

    IEnumerator ColorChange()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
