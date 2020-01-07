using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    [HideInInspector]
    public bool istouching;
    private void OnCollisionStay2D(Collision2D collision)
    {
        istouching = true;
    }

}
