using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject nounours;
    Image FirstHeart;
    Image SecondHeart;
    Image ThirdHeart;
    Image FourthHeart;
    Image FifthHeart;

    // Start is called before the first frame update
    void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        heart1 = GameObject.FindGameObjectWithTag("Heart1");
        heart2 = GameObject.FindGameObjectWithTag("Heart2");
        heart3 = GameObject.FindGameObjectWithTag("Heart3");
        heart4 = GameObject.FindGameObjectWithTag("Heart4");
        heart5 = GameObject.FindGameObjectWithTag("Heart5");
        FirstHeart = heart1.GetComponent<Image>();
        SecondHeart = heart2.GetComponent<Image>();
        ThirdHeart = heart3.GetComponent<Image>();
        FourthHeart = heart4.GetComponent<Image>();
        FifthHeart = heart5.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (nounours.GetComponent<PlayerController>().health == 10)
        {
            FirstHeart.fillAmount = 1;
            SecondHeart.fillAmount = 1;
            ThirdHeart.fillAmount = 1;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 9)
        {
            FirstHeart.fillAmount = 0.5f;
            SecondHeart.fillAmount = 1;
            ThirdHeart.fillAmount = 1;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 8)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 1;
            ThirdHeart.fillAmount = 1;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 7)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0.5f;
            ThirdHeart.fillAmount = 1;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 6)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 1;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 5)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 0.5f;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 4)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 0;
            FourthHeart.fillAmount = 1;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 3)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 0;
            FourthHeart.fillAmount = 0.5f;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 2)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 0;
            FourthHeart.fillAmount = 0;
            FifthHeart.fillAmount = 1;
        }
        if (nounours.GetComponent<PlayerController>().health == 1)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 0;
            FourthHeart.fillAmount = 0;
            FifthHeart.fillAmount = 0.5f;
        }
        if (nounours.GetComponent<PlayerController>().health == 0)
        {
            FirstHeart.fillAmount = 0;
            SecondHeart.fillAmount = 0;
            ThirdHeart.fillAmount = 0;
            FourthHeart.fillAmount = 0;
            FifthHeart.fillAmount = 0;
        }
    }
}
