using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{

    int correctAnswers = 0;

    int totalAnswers = 0;


    [SerializeField]
    GameObject resultPrefab;

    [SerializeField]
    Transform content;

    [SerializeField]
    TMP_Text resultScoreText;


    // Start is called before the first frame update
    public void AddResult(Character character, string userAnswer)
    {

        totalAnswers++;

        GameObject result = Instantiate(resultPrefab, content);


        if(character.name == userAnswer)
        {
            correctAnswers++;

            result.GetComponent<Image>().color = Color.green;
        }
        else
        {
            result.GetComponent<Image>().color = Color.red;
        }

        result.GetComponent<ResultItem>().SetResultFields(character, userAnswer);


        resultScoreText.text = $"Results [{correctAnswers}/10]";


        result.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Question {totalAnswers}: {character.name}";


        if(totalAnswers >= 10)
        {
            StartCoroutine(ShowResults());
        }


    }


    IEnumerator ShowResults()
    {

        //Check Highscore
        if(correctAnswers > PlayerPrefs.GetFloat("Highscore"))
        {
            UIController.Instance.NewHighscore();
            PlayerPrefs.SetFloat("Highscore", correctAnswers);
        }

        correctAnswers = 0;

        totalAnswers = 0;

        yield return new WaitForSeconds(1f);




    }

    public void ResetResults()
    {
        StartCoroutine(IResetResults());
    }

    public IEnumerator IResetResults()
    {
        yield return new WaitForSeconds(3f);

        foreach (Transform item in content.transform) 
        {
            Destroy(item.gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
