using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBasique : MonoBehaviour
{
    public float rageTime;
    public int rageDamageBoost;

    public IEnumerator RageActive1()
    {
        StartCoroutine(GetComponent<PlayerController>().ColorChangeRage());
        GetComponent<PlayerController>().damage = GetComponent<PlayerController>().damage * rageDamageBoost;
        GetComponent<PlayerController>().canLooseLife = false;
        Debug.Log("Rage Activée !!!");
        yield return new WaitForSeconds(rageTime);
        GetComponent<PlayerController>().damage = GetComponent<PlayerController>().damage / rageDamageBoost;
        GetComponent<PlayerController>().canLooseLife = true;
    }
}
