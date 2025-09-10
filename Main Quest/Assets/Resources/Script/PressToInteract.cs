using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressToInteract : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject pressEIndicator;
    private bool playerInRange = false;

    void Update()
    {
        //Input.GetKeyDown(KeyCode.E) pc
        //&& Input.GetTouch(0).phase == TouchPhase.Began) mobile
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueBox.SetActive(true);
            pressEIndicator.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            pressEIndicator.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            pressEIndicator.SetActive(false);
        }
    }
}