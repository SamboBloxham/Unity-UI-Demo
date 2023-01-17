using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopUpHandler : MonoBehaviour
{

    [SerializeField]
    TMP_Text correctAnswerText;

    [SerializeField]
    Image characterImage;

    [SerializeField]
    TMP_Text userAnswerText;



    [SerializeField]
    GameObject popUp;


    public static ResultPopUpHandler Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void DisplayPopUp(Character correctAnswer, string userAnswer)
    {

        correctAnswerText.text = correctAnswer.name;

        characterImage.sprite = correctAnswer.sprite;

        userAnswerText.text = $"Your Answer:\n{userAnswer}";

        popUp.SetActive(true);

    }

    public void Close()
    {
        popUp.SetActive(false);
    }

}
