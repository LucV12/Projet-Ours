using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBarScript : MonoBehaviour
{
    Image rageBar;
    public GameObject nounours;

    // Start is called before the first frame update
    void Start()
    {
        rageBar = GetComponent<Image>();
        nounours = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rageBar.fillAmount = nounours.GetComponent<PlayerController>().rage;
    }
}
