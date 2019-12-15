using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBasique : MonoBehaviour
{
    public float rageTime;
    public int rageDamageBoost;
    GameObject nounours;
    PlayerController pc;

    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        pc = nounours.GetComponent<PlayerController>();
    }

    public IEnumerator RageActive1()
    {
        StartCoroutine(pc.ColorChangeRage());
        pc.damage = pc.damage * rageDamageBoost;
        pc.canLooseLife = false;
        Debug.Log("Rage Activée !!!");
        yield return new WaitForSeconds(rageTime);
        pc.damage = pc.damage / rageDamageBoost;
        pc.canLooseLife = true;
    }
}
