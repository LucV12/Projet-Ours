using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapyChulo : MonoBehaviour
{
    GameObject gameManager;
    GameManager GM;

    public GameObject[] capacities;
    public GameObject[] rages;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.capaAvaible.Contains("Tourbi") && capacities[1].activeSelf == false)
        {
            capacities[1].SetActive(true);
        }

        if (GM.capaAvaible.Contains("Roar") && capacities[2].activeSelf == false)
        {
            capacities[2].SetActive(true);
        }

        if (GM.rageAvaible.Contains("Ruee") && rages[1].activeSelf == false)
        {
            rages[1].SetActive(true);
        }

        if (GM.rageAvaible.Contains("Terri") && rages[2].activeSelf == false)
        {
            rages[2].SetActive(true);
        }
    }
}
