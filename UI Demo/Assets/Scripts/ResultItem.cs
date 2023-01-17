using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultItem : MonoBehaviour
{
    [SerializeField]
    Character correctAnswer;

    [SerializeField]
    string userAnswer;


    public void DisplayResult()
    {
        ResultPopUpHandler.Instance.DisplayPopUp(correctAnswer, userAnswer);
    }


    public void SetResultFields(Character correctAnswerField, string userAnswerField)
    {

        correctAnswer = correctAnswerField;

        userAnswer = userAnswerField;

    }

    

}
