using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuUI; // Assign your Pause Menu panel here
    private bool isPaused = false;

    // Called when Pause button is pressed
    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
        // Make sure time resumes before switching scenes
        Time.timeScale = 1f;

        // Replace "MainMenu" with your actual main menu scene name
        SceneManager.LoadScene("Start Menu");
    }
}
