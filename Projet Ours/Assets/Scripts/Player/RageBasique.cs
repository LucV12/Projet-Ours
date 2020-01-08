using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBasique : MonoBehaviour
{
    public float rageTime;
    public int rageDamageBoost;
    GameObject nounours;
    PlayerController pc;
    GameObject rageManager;
    RageManager RM;

    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        pc = nounours.GetComponent<PlayerController>();
        rageManager = GameObject.FindGameObjectWithTag("RageManager");
        RM = rageManager.GetComponent<RageManager>();
    }

    public IEnumerator RageActive1()
    {
        pc.canLooseLife = false;
        StartCoroutine(pc.ColorChangeRage());
        RM.isInRage = true;
        pc.damage = pc.damage * rageDamageBoost;
        Debug.Log("Rage Activée !!!");
        yield return new WaitForSeconds(rageTime);
        pc.damage = pc.damage / rageDamageBoost;
        pc.canLooseLife = true;
        RM.isInRage = false;
    }
}
