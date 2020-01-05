using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    private static GameObject playerInit = null;
    private static Camera cameraInit = null;
    private static GameManager GMInstance = null;

    public List<string> capaAvaible = new List<string>();
    public List<string> rageAvaible = new List<string>();




    void Awake()
    {
        capaAvaible.Add("Kick");
        rageAvaible.Add("Bearserk");

        if (GMInstance == null)
        {
            GMInstance = this;
            DontDestroyOnLoad(this.gameObject);

            if (playerInit == null)
            {
                Instantiate<GameObject>(player);
                playerInit = GameObject.FindGameObjectWithTag("Player");
                DontDestroyOnLoad(playerInit);
            }
            else
            {
                Destroy(playerInit);
            }

            if (cameraInit == null)
            {
                cameraInit = Camera.main;
                DontDestroyOnLoad(cameraInit);
                cameraInit.name = ("MainCameraInit");
            }
            else
            {
                Destroy(GameObject.Find("Main Camera"));
            }
        }
        else
        {
            Destroy(this);
        }
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
