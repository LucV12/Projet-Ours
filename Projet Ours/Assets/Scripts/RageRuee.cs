using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageRuee : MonoBehaviour
{
    public float rageTime;
    private bool dashRageActivated;
    public float dashRageSpeedBoost;

    public IEnumerator RageActive2 ()
    {
        dashRageActivated = true;
        GetComponent<PlayerController>().rollSpeed = GetComponent<PlayerController>().rollSpeed * dashRageSpeedBoost;
        GetComponent<PlayerController>().canRoll = false;

        yield return new WaitForSeconds(rageTime);

        GetComponent<PlayerController>().rollSpeed = GetComponent<PlayerController>().rollSpeed / dashRageSpeedBoost;
        GetComponent<PlayerController>().canRoll = true;
        dashRageActivated = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && dashRageActivated == true && GetComponent<PlayerController>().trailActive == true)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}
