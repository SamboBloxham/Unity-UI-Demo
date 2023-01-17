using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UIController : MonoBehaviour
{

    public static UIController Instance { get; private set; }


    public Animator uiAnimator;


    [SerializeField]
    ParticleSystem confettiParticleSystem;


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

    bool mainMenuShown = true;

    public void HideMainMenu()
    {
        uiAnimator.SetBool("inMainMenu", false);
    }

    public void ShowMainMenu()
    {
        uiAnimator.SetBool("inMainMenu", true);
    }


    public void ShowResultsScreen()
    {
        uiAnimator.SetBool("inResults", true);
    }

    public void HideResultsScreen()
    {
        uiAnimator.SetBool("inResults", false);
    }

    public void NewHighscore()
    {
        uiAnimator.SetTrigger("newHighscore");
    }

    public void HighscoreConfetti()
    {
        print("highscore CONFET");

        confettiParticleSystem.Play();
    }



}
