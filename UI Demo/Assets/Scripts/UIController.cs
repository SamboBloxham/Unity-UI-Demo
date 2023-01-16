using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController Instance { get; private set; }


    public Animator uiAnimator;

    void Awake()
    {
        Instance = this;
    }


    public void ShowSettingsMenu()
    {
        uiAnimator.SetBool("inSettings", true);
    }

    public void HideSettingsMenu()
    {
        uiAnimator.SetBool("inSettings", false);
    }

    public void ShowQuestionsScreen()
    {

    }


}
