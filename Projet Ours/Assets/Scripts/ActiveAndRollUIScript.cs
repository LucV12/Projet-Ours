using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAndRollUIScript : MonoBehaviour
{
    public GameObject rollCooldown;
    public GameObject activeCooldown;
    public GameObject nounours;
    private bool rollCooldownActivated;
    private bool activeCooldownActivated;
    Image rollLayer;
    Image activeLayer;
    private float rollFill;
    private float startRollFill;
    private float activeFill;
    private float startActiveFill;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        rollCooldown = GameObject.FindGameObjectWithTag("RollUI");
        activeCooldown = GameObject.FindGameObjectWithTag("ActiveUI");
        rollLayer = rollCooldown.GetComponent<Image>();
        activeLayer = activeCooldown.GetComponent<Image>();
        rollCooldownActivated = false;
        activeCooldownActivated = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (rollCooldownActivated == true)
        {
            rollLayer.fillAmount = rollFill/startRollFill;
            rollFill -= Time.deltaTime;
        }
        else
        {
            rollLayer.fillAmount = 0;
        }

        if (activeCooldownActivated == true)
        {
            activeLayer.fillAmount = activeFill/startActiveFill;
            activeFill -= Time.deltaTime;
        }
        else
        {
            activeLayer.fillAmount = 0;
        }
    }

    public IEnumerator rollUICooldown()
    {
        Debug.Log("Roll Cooldown");
        rollCooldownActivated = true;
        rollFill = nounours.GetComponent<PlayerController>().rollDelay;
        startRollFill = rollFill;
        yield return new WaitForSeconds(rollFill);
        rollCooldownActivated = false;
    }

    public IEnumerator activeUICooldown()
    {
        Debug.Log("Active Cooldown");
        activeCooldownActivated = true;
        activeFill = nounours.GetComponent<PlayerController>().startActiveDelay;
        startActiveFill = activeFill;
        yield return new WaitForSeconds(activeFill);
        activeCooldownActivated = false;
    }
}
