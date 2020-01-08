using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public Animator animator;
    public GameObject mainMenu;

    // Start is called before the first frame update

    public void StartIntro()
    {
        animator.SetBool("IsBoucling", true);
        mainMenu.SetActive(true);
    }
}
