using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject GameOverUI;

    public bool GameIsOver = false;

    GameObject pauseMenu;
    PauseMenu PM;

    GameObject sceneManager;
    ScenesManager SM;

    GameObject nounours;
    PlayerController PC;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        PM = pauseMenu.GetComponent<PauseMenu>();
        nounours = GameObject.FindGameObjectWithTag("Player");
        PC = nounours.GetComponent<PlayerController>();
        sceneManager = GameObject.FindGameObjectWithTag("ScenesManager");
        SM = sceneManager.GetComponent<ScenesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start") && GameIsOver == true)
        {
            nounours.SetActive(true);
            GameIsOver = false;
            SM.HubScene();
            PC.health = 10;
            PC.transform.position = PC.InitPosition;
            Time.timeScale = 1f;
            GameOverUI.SetActive(false);
            nounours.SetActive(true);
            nounours.GetComponent<Renderer>().material.color = Color.white;
            PC.canMove = true;
            GameIsOver = false;
        }
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;
        nounours.SetActive(false);
    }
}
