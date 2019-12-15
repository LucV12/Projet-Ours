﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Trigger : MonoBehaviour
{
    public Dialog dialogue;

    public Transform talkPos;
    public float talkRange;
    public LayerMask whatIsThePlayer;

    private bool isTalking;

    public Animator textBox;

    public GameObject toucheA;

    void Start()
    {
        isTalking = false;
        toucheA.SetActive(false);
    }

    void Update()
    {
        if (isTalking == false)
        {
            Collider2D[] playerToTalk = Physics2D.OverlapCircleAll(talkPos.position, talkRange, whatIsThePlayer);

            if (playerToTalk.Length > 0)
            {
                toucheA.SetActive(true);
            }
            else
            {
                toucheA.SetActive(false);
            }

            if (playerToTalk.Length > 0 && Input.GetKeyDown(KeyCode.P))
            {
                TriggerDialogue();
                isTalking = true;
                toucheA.SetActive(false);
            }
        }
        else if (isTalking == true && Input.GetKeyDown(KeyCode.P))
        {
            ContinueDialogue();
        }

        if (!textBox.GetBool("IsOpen"))
        {
            isTalking = false;
        }

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }

    public void ContinueDialogue()
    {
        FindObjectOfType<DialogManager>().DisplayNextSentence();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, talkRange);
    }
}