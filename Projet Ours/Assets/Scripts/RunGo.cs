using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGo : MonoBehaviour
{
    GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponentInChildren<ScenesManager>().RunScene();
        }
    }
}
