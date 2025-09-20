using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winningUI : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}