using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteBoss : MonoBehaviour
{
    public bool Locked = true;
    MachineOBJ machineOBJ;
    ReservoirOBJ reservoirOBJ;
    GameObject machineSalle;
    GameObject reservoirSalle;

    // Start is called before the first frame update
    void Start()
    {
        machineSalle = GameObject.FindGameObjectWithTag("OBJ");
        machineOBJ = machineSalle.GetComponentInChildren<MachineOBJ>();
        reservoirSalle = GameObject.FindGameObjectWithTag("OBJ2");
        reservoirOBJ = reservoirSalle.GetComponentInChildren<ReservoirOBJ>();
    }

    // Update is called once per frame
    void Update()
    {
        if (machineOBJ.Destroyed1 == true && reservoirOBJ.Destroyed2 == true)
        {
            Locked = false;
        }

        if (Locked == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
