using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] sentences;
    public float typingSpeed;
    public GameObject dialogueBox;
    private int index;
    public EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == sentences[index])
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

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
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
            spawner.hasTalkedToNPC = true;

            if (CoroutineProxy.Instance != null)
            {
                CoroutineProxy.Instance.RunCoroutine(spawner.SpawnAfterDelay());
            }
            else
            {
                Debug.LogWarning("CoroutineProxy.Instance is null. Make sure it's in the scene.");
            }

            dialogueBox.SetActive(false);
        }
    }
}
