using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("FirstScene");
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.LoadScene("Hub");
        
    }


    public void RunScene()
    {
        SceneManager.LoadScene("Run");
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = player.GetComponent<PlayerController>().InitPosition;
    }

    public void HubScene()
    {
        SceneManager.LoadScene("Hub");
    }

    public void PauseCene()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }
}
