using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] Toggle muteToggle;

    [SerializeField] Toggle darkModeToggle;

    [SerializeField] Slider volumeSlider;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Material darknessMaterial;

    [SerializeField] GameObject clearHighscorePopup;


    private void Start()
    {

        if (PlayerPrefs.GetInt("playedBefore") != 0)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("AudioVolume");
        }


        PlayerPrefs.SetInt("playedBefore", 1);


        if (PlayerPrefs.GetInt("DarkMode") == 1) darkModeToggle.isOn = true; 
    }


    public void ToggleMute()
    {
        if (muteToggle.isOn)
        {
            audioMixer.SetFloat("Volume", -80);
            PlayerPrefs.SetFloat("AudioVolume", -80);
        }
        else
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);
            PlayerPrefs.SetFloat("AudioVolume", volumeSlider.value);
        }
    }

    public void AdjustVolume()
    {
        muteToggle.SetIsOnWithoutNotify(false);
        audioMixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);
        PlayerPrefs.SetFloat("AudioVolume", volumeSlider.value);

    }

    public void ToggleDarkMode()
    {
        if(darkModeToggle.isOn)
        {
            PlayerPrefs.SetInt("DarkMode", 1);
            darknessMaterial.color = Color.grey;
        }
        else
        {
            PlayerPrefs.SetInt("DarkMode", 0);
            darknessMaterial.color = Color.white;
        }


    }

    public void AskToClearHighscore()
    {
        clearHighscorePopup.SetActive(true);
    }

    public void ClearHighscore()
    {
        PlayerPrefs.SetFloat("Highscore", 0);
        clearHighscorePopup.SetActive(false);
    }

    public void ClosePopup()
    {
        clearHighscorePopup.SetActive(false);
    }



    public void Back()
    {
        UIController.Instance.HideSettingsMenu();
    }


}
