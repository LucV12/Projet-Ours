using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Update()
    {
        Destroy(gameObject, 2f);
    }
}
