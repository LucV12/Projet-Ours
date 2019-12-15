using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageManager : MonoBehaviour
{

    public GameObject[] rages;
    int activeRage;
    GameObject nounours;
    bool rageAvaibleManager;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rages.Length; i++)
        {
            if (rages[i].activeSelf)
            {
                activeRage = i;
                break;
            }
        }


        rageAvaibleManager = nounours.GetComponent<PlayerController>().rageAvaible;

        if (rageAvaibleManager == true && Input.GetButtonDown("Rage"))
        {
            Debug.Log("PRESS B");

            if (activeRage == 0)
            {
                StartCoroutine(GetComponentInChildren<RageBasique>().RageActive1());
            }

            if (activeRage == 1)
            {
                StartCoroutine(GetComponentInChildren<RageRuee>().RageActive2());
            }

            if (activeRage == 2)
            {
                StartCoroutine(GetComponentInChildren<RageTerrifiante>().RageActive3());
            }

            nounours.GetComponent<PlayerController>().rage = 0;
            nounours.GetComponent<PlayerController>().rageAvaible = false;
            nounours.GetComponent<PlayerController>().rageActivated = true;

        }
    }
}


