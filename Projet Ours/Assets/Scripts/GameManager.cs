using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public Camera topView;

    
    void Start()
    {
       // Instantiate<GameObject>(player);      
    }

    
    void Update()
    {
        topView.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
