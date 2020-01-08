using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    GameObject player;
    GameObject audioManager;
    public static ScenesManager instance;
    //AudioManager AM;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        /*audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        AM = audioManager.GetComponent<AudioManager>();*/
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("FirstScene");
        player = GameObject.FindGameObjectWithTag("Player");
        HubScene();       
    }


    public void RunScene()
    {
        SceneManager.LoadScene("Run");
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = player.GetComponent<PlayerController>().InitPosition;
        //AM.MusiqueRun();
        //AudioManager.instance.MusiqueRun();
    }

    public void HubScene()
    {
        Debug.Log("ohoh");
        SceneManager.LoadScene("Hub");
        //AM.MusiqueHub();        
        //AudioManager.instance.MusiqueHub();
    }

    public void PauseCene()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }
}
