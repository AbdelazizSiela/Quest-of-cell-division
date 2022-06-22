using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    private NPC currentNPC;
    private GameObject DialogBox;
    private TextMeshProUGUI nameText,dialogText;

    private bool startedConversation;
    private int currentPhraseIndex;

    private PlayerStats statsScript;

    private void Awake()
    {
        DialogBox = GameObject.Find("DialogBox");
        nameText = DialogBox.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        dialogText = DialogBox.transform.Find("DialogText").GetComponent<TextMeshProUGUI>();

        DialogBox.SetActive(false);

        statsScript = GetComponent<PlayerStats>();
    }
    private void OnTriggerEnter(Collider collision)
    {
      if(collision.CompareTag("NPC") && currentNPC == null)
        {
            currentNPC = collision.GetComponent<NPC>();
        }   
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("NPC") && currentNPC != null)
        {
            if(startedConversation)
            {
                EndConversation();
            }

            currentNPC = null;
        }
    }

    private void OnInteract()
    {
        if (statsScript.isInventory)
        {
            return;
        }

        if (currentNPC != null)
        {
            statsScript.isInteracting = true;

            if (!startedConversation)
            {
                StartConversation();
            }
           else
            {
                NextPhrase();
            }
        }

    }
    private void OnEscape()
    {
      if(currentNPC && startedConversation)
        {
            EndConversation();
        }

    }
    private void StartConversation()
    {
        DialogBox.SetActive(true);

        dialogText.text = currentNPC.Phrases[0];
        nameText.text = currentNPC.Name;

        startedConversation = true;
    }
    private void NextPhrase()
    {
        currentPhraseIndex++;
        if(currentPhraseIndex >= currentNPC.Phrases.Length)
        {
            EndConversation();
            return;
        }

        dialogText.text = currentNPC.Phrases[currentPhraseIndex];

        startedConversation = true;
    }
    private void EndConversation()
    {
        DialogBox.SetActive(false);

        currentPhraseIndex = 0;
        startedConversation = false;

        statsScript.isInteracting = false;
    }
}
