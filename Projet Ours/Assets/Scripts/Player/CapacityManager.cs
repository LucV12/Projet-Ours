using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityManager : MonoBehaviour
{
    GameObject[] activables = new GameObject[3];
    int activeCapa;
    GameObject nounours;
    public float delayCapa;
    public bool canCapa;
    GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        delayCapa = nounours.GetComponent<PlayerController>().startActiveDelay;
        canCapa = true;
        UI = GameObject.FindGameObjectWithTag("UI");
        activables = GetComponentInParent<PlayerController>().capacities;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activables.Length; i++)
        {
            if (activables[i].activeSelf)
            {
                activeCapa = i;
                break;
            }
        }

        if (Input.GetButton("CapacityButton") && canCapa == true)
        {
            Debug.Log("PRESS RB");

            if (activeCapa == 0)
            {
                StartCoroutine(GetComponentInChildren<Kick>().KickActivable());
                FindObjectOfType<AudioManager>().Play("Kick");
            }

            if (activeCapa == 1)
            {
                GetComponentInChildren<Roar>().RoarActivable();
                FindObjectOfType<AudioManager>().Play("Roar");
            }

            if (activeCapa == 2)
            {
                StartCoroutine(GetComponentInChildren<Tourbilol>().TourbilolActivable());
                FindObjectOfType<AudioManager>().Play("Tourbilol");
            }

            StartCoroutine(UI.GetComponent<ActiveAndRollUIScript>().activeUICooldown());
            StartCoroutine(CapacityDelay());

        }


    }

    private IEnumerator CapacityDelay ()
    {
        canCapa = false;
        yield return new WaitForSeconds(delayCapa);
        canCapa = true;
    }


}
