using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
///using static UnityEditor.Progress;
//using UnityEngine.TextCore.Text;

public class QuestionMaster : MonoBehaviour
{

    [SerializeField]
    ResultsManager resultsManager;

    [SerializeField]
    Character[] realAnswers;

    [SerializeField]
    string[] allAnswers;



    [SerializeField]
    GameObject[] answerButtons;


    TMP_Text[] answerButtonText;


    [SerializeField]
    Image silhouetteImage;

    [SerializeField]
    Animator questionAnimator;

    [SerializeField]
    Color buttonColour;

    [SerializeField]
    GameObject answerHighlight;

    [SerializeField]
    GameObject answerStrike;


    [SerializeField]
    AudioSource correctSound;

    [SerializeField]
    AudioSource incorrectSound;

    int questionsAnswered = 0;


    



    // Start is called before the first frame update
    void Awake()
    {
        answerButtonText = new TMP_Text[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtonText[i] = answerButtons[i].transform.GetChild(0).GetComponent<TMP_Text>();
        }
        

        realAnswers = Resources.LoadAll<Character>("Characters");
        PopulateAllAnswers();
        overflowCheck = 0;
    }

    private void Start()
    {
        LoadNewQuestion();
    }

    Coroutine userAnsweringQuestion;

    Character currentQuestion;

    int overflowCheck = 0;

    void LoadNewQuestion()
    {
        currentQuestion = realAnswers[UnityEngine.Random.Range(0,realAnswers.Length)];

        if(currentQuestion.answered == true && overflowCheck < realAnswers.Length)
        {
            print("question reloaded");
            overflowCheck++;
            LoadNewQuestion();
            return;
        }

        LoadAnswers(currentQuestion);


        silhouetteImage.sprite = currentQuestion.sprite;

    }

    List<string> availableAnswers = new List<string>();

    int correctAnswer;

    void LoadAnswers(Character question)
    {
        availableAnswers.Clear();

        availableAnswers.Add(question.name);


        while(availableAnswers.Count < answerButtonText.Length)
        {

            string randomAnswer = allAnswers[UnityEngine.Random.Range(0, allAnswers.Length)];

            if (!availableAnswers.Contains(randomAnswer))
            {
                availableAnswers.Add(randomAnswer);
            }
        }


        System.Random random = new System.Random();


        List<int> listRandomNumbers = new List<int>();

        for (int i = 0; i < availableAnswers.Count; i++)
        {
            int randomNumber;

            do
            {
                randomNumber = random.Next(0, availableAnswers.Count);
            } while (listRandomNumbers.Contains(randomNumber));


            listRandomNumbers.Add(randomNumber);

        }

        for (int i = 0; i < answerButtonText.Length; i++)
        {

            answerButtonText[i].text = availableAnswers[listRandomNumbers[i]];

            if (listRandomNumbers[i] == 0) correctAnswer = i;
            

        }
    }


    void PopulateAllAnswers()
    {
        var file = Resources.Load<TextAsset>("ListOfAllCharacters");
        var content = file.text;
        allAnswers = content.Split(",");
    }


    public void UserAnswered(int userAnswer)
    {
        answerButtons[userAnswer].GetComponent<Image>().color = Color.red;

        answerButtons[correctAnswer].GetComponent<Image>().color = Color.green;





        foreach (var item in answerButtons)
        {
            item.GetComponent<Button>().interactable = false;
        }


        //Correct answer animation here
        if(currentQuestion.name == answerButtonText[userAnswer].GetComponent<TMP_Text>().text)
        {
            correctSound.Play();
            answerHighlight.transform.position = answerButtons[correctAnswer].transform.position;
            questionAnimator.SetTrigger("highlight");
        }
        else
        {
            incorrectSound.Play();
            answerStrike.transform.parent = answerButtons[userAnswer].transform;
            questionAnimator.SetTrigger("strike");
        }


        questionsAnswered++;

        currentQuestion.answered = true;


        resultsManager.AddResult(currentQuestion, answerButtonText[userAnswer].GetComponent<TMP_Text>().text);


        if (userAnsweringQuestion != null)
        StopCoroutine(userAnsweringQuestion);

        userAnsweringQuestion = StartCoroutine(QuestionAnswered());

    }


    IEnumerator QuestionAnswered()
    {

        questionAnimator.SetTrigger("reveal");
        //Reveal Silhouette using animation

        yield return new WaitForSeconds(1.5f);


        if (questionsAnswered < 10)
        {

            questionAnimator.SetTrigger("next");
            
        }
        else
        {
            UIController.Instance.ShowResultsScreen();
            questionsAnswered = 0;


            yield return new WaitForSeconds(3f);


            questionAnimator.SetTrigger("next");

            foreach (var item in answerButtons)
            {
                item.GetComponent<Image>().color = buttonColour;
            }

            foreach (var item in answerButtons)
            {
                item.GetComponent<Button>().interactable = true;
            }

            LoadNewQuestion();
        }

    }


    public void ExitEarly()
    {

        StartCoroutine(IExitEarly());

    }

    IEnumerator IExitEarly()
    {
        UIController.Instance.ShowMainMenu();
        questionsAnswered = 0;


        yield return new WaitForSeconds(1f);


        //questionAnimator.SetTrigger("next");

        foreach (var item in answerButtons)
        {
            item.GetComponent<Image>().color = buttonColour;
        }

        foreach (var item in answerButtons)
        {
            item.GetComponent<Button>().interactable = true;
        }

        LoadNewQuestion();
    }



    public void SetNextQuestion()
    {
        foreach (var item in answerButtons)
        {
            item.GetComponent<Image>().color = buttonColour;
        }

        LoadNewQuestion();

        
    }

    public void ReEnableButtons()
    {
        foreach (var item in answerButtons)
        {
            item.GetComponent<Button>().interactable = true;
        }

    }




}
