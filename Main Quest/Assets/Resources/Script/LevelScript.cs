using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;

    void Start()
    {
        PlayerPrefs.SetString("LastPlayedLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Level 1 is always unlocked and fully visible
        level1Button.interactable = true;
        level1Button.image.color = Color.white;

        // Level 2
        if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
        {
            level2Button.interactable = true;
        }
        else
        {
            level2Button.interactable = false;
            level2Button.image.color = new Color(0.7f, 0.7f, 0.7f); // dark gray for locked
        }

        // Level 3
        if (PlayerPrefs.GetInt("Level2Completed", 0) == 1)
        {
            level3Button.interactable = true;
        }
        else
        {
            level3Button.interactable = false;
            level3Button.image.color = new Color(0.7f, 0.7f, 0.7f);
        }

        // Level 4
        if (PlayerPrefs.GetInt("Level3Completed", 0) == 1)
        {
            level4Button.interactable = true;
        }
        else
        {
            level4Button.interactable = false;
            level4Button.image.color = new Color(0.7f, 0.7f, 0.7f);
        }
}
}
