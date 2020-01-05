using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    Text nameText;
    Text dialogueText;

    GameObject NTO;
    GameObject DTO;
    GameObject textBox;

    Animator animator;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        NTO = GameObject.FindGameObjectWithTag("NameText");
        nameText = NTO.GetComponent<Text>();
        DTO = GameObject.FindGameObjectWithTag("DialogueText");
        dialogueText = DTO.GetComponent<Text>();
        textBox = GameObject.FindGameObjectWithTag("TextBox");
        animator = textBox.GetComponent<Animator>();
    }

    public void StartDialogue (Dialog dialogue)
    {

        animator.SetBool("IsOpen", true);
        
        Debug.Log("Début de la conversation avec" + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
