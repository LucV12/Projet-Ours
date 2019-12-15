using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject playerInit;
    Camera cameraInit;

    
    void Awake()
    {
        Instantiate<GameObject>(player);
        playerInit = GameObject.FindGameObjectWithTag("Player");
        cameraInit = Camera.main;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(playerInit);
        DontDestroyOnLoad(cameraInit);
    }

    
    void Update()
    {
        cameraInit.transform.position = new Vector3(playerInit.transform.position.x, playerInit.transform.position.y, -10f);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), "FPS: " + (int)(1.0f / Time.smoothDeltaTime));
    }
}
