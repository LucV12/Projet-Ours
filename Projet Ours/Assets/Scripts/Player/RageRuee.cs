using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageRuee : MonoBehaviour
{
    public float rageTime;
    private bool dashRageActivated;
    public float dashRageSpeedBoost;
    public int rageDamageBoost;
    GameObject nounours;
    PlayerController pc;

    public void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        pc = nounours.GetComponent<PlayerController>();
    }

    public IEnumerator RageActive2 ()
    {
        StartCoroutine(pc.ColorChangeRage());
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().Play("DashRage");
        dashRageActivated = true;
        pc.rollSpeed = pc.rollSpeed * dashRageSpeedBoost;
        pc.canRoll = false;
        pc.damage = pc.damage * rageDamageBoost;

        yield return new WaitForSeconds(rageTime);

        pc.rollSpeed = pc.rollSpeed / dashRageSpeedBoost;
        pc.canRoll = true;
        dashRageActivated = false;
        pc.damage = pc.damage / rageDamageBoost;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && dashRageActivated == true && pc.trailActive == true)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}
