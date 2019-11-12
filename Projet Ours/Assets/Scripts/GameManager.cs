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
        player = GameObject.Find("nounours");
    }

    
    void Update()
    {
        topView.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), "FPS: " + (int)(1.0f / Time.smoothDeltaTime));
    }
}
