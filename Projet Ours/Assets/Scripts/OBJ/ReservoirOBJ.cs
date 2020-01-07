using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirOBJ : MonoBehaviour
{
    public float HP;
    public bool Destroyed2 = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroyed2 = true;
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log("damage TAKEN !");
    }
}
