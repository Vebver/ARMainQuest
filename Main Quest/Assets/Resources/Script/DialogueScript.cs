using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [Header("Dialogue UI")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    [Header("Dialogue Content")]
    public string[] sentences;
    public float typingSpeed = 0.05f;

    [Header("Spawner Reference")]
    public EnemySpawner spawner; // Assign in Inspector!

    private int index;

    void Start()
    {
        dialogueText.text = "";
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < sentences.Length) // ✅ check array bounds
            {
                if (dialogueText.text == sentences[index])
                {
                    nextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = sentences[index];
                }
            }
        }
    }


    public void StartDialogue()
    {
        index = 0;
        dialogueText.text = "";
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        if (index < sentences.Length) // ✅ prevents error
        {
            foreach (char letter in sentences[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }


    void nextLine()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            // Dialogue finished
            dialogueBox.SetActive(false);

            if (EnemyManager.Instance != null)
                EnemyManager.Instance.StartSpawning();

            enabled = false; // ✅ stop Update() from running
        }
    }


}
