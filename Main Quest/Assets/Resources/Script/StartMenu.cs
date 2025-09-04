using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("LastPlayedLevel", "")))
            continueButton.interactable = false;
    }

    public void OnContinueButtonPressed()
    {
        string lastLevel = PlayerPrefs.GetString("LastPlayedLevel", "Level1");
        SceneManager.LoadScene(lastLevel);
    }

    public void OnNewGamePressed()
    {
        SceneManager.LoadScene(2);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }

}
