using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Dialogue Reference")]
    public DialogueScript dialogue; // Assign in Inspector

    public void TalkToPlayer()
    {
        if (dialogue != null)
        {
            dialogue.dialogueBox.SetActive(true); // show dialogue box
            dialogue.StartDialogue();             // begin dialogue typing
        }
        else
        {
            Debug.LogWarning("⚠️ Dialogue reference missing on NPC!");
        }
    }
}
