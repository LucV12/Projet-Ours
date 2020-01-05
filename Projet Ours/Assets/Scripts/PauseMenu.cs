using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    GameObject GameOver;
    GameOverScript GOS;

    private void Start()
    {
        GameOver = GameObject.FindGameObjectWithTag("GameOver");
        GOS = GameOver.GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start") && GOS.GameIsOver == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
