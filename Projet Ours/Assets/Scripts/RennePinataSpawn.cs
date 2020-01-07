using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RennePinataSpawn : MonoBehaviour
{
    public bool isSpawning;
    public bool deadPinata;
    public GameObject pinata;
    public GameObject currentPinata;
    public Transform pinataPos;
    Transform pinataPosMemory;
    // Start is called before the first frame update
    void Start()
    {
        deadPinata = currentPinata.GetComponent<Pinata>().newPinata;
        isSpawning = false;
        currentPinata = GameObject.FindGameObjectWithTag("Pinata");
        pinataPos = GameObject.FindGameObjectWithTag("Pinata").transform;
        pinataPosMemory = pinataPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning == false)
        {
            deadPinata = currentPinata.GetComponent<Pinata>().newPinata;
        }

        if (isSpawning == false && deadPinata == true)
        {
            isSpawning = true;
            StartCoroutine(SpawningNewPinata());
        }
    }

    IEnumerator SpawningNewPinata()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Spawning New Pinata");
        Instantiate(pinata, pinataPosMemory);
        currentPinata = GameObject.FindGameObjectWithTag("Pinata");
        pinataPos = GameObject.FindGameObjectWithTag("Pinata").transform;
        isSpawning = false;
    }
}
