using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuFunctions : MonoBehaviour
{
    public void Play()
    {
        UIController.Instance.ShowQuestionsScreen();
    }

    public void Settings()
    {
        UIController.Instance.ShowSettingsMenu();
    }
}
