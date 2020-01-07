using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineOBJ : MonoBehaviour
{
    public float HP;
    public bool Destroyed1 = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroyed1 = true;
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log("damage TAKEN !");
    }
}
