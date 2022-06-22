using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject popupGO, interactionHint;
    [SerializeField] private TextMeshProUGUI popupText,interactionText;

    [SerializeField] private string[] Indications;

    private TutorialEvents tutorialEvents;

    private int currentIndication;
    private bool waitingOnInteraction,waitingOnEscape,waitingOnInventory;

    private IEnumerator Start()
    {
        tutorialEvents = FindObjectOfType<TutorialEvents>();

        yield return new WaitForSeconds(0f);

        tutorialEvents.waitOnMove = true;
        EnablePopup();

    }

    private void EnablePopup()
    {
        popupGO.SetActive(true);
        ChangeText();

    }
    private void DisablePopup()
    {
        popupGO.SetActive(false);
    }

    private void ChangeText()
    {
        popupText.text = Indications[currentIndication];
    }
    private void OnInteractionText(bool status)
    {
        interactionHint.SetActive(status);
        waitingOnInteraction = status;
    }
    private void OnEscapeText(bool status)
    {
        interactionHint.SetActive(status);
        waitingOnEscape = status;
    }
    private void OnInventoryText(bool status)
    {
        interactionHint.SetActive(status);
        waitingOnInventory = status;
    }
    private void ChangeInteractionText(string newText)
    {
        interactionText.text = newText;
    }
    private void WaitOnInteraction()
    {
        tutorialEvents.waitOnInteract = true;

        if(tutorialEvents.isInteracting)
        {
            currentIndication++;

            ChangeText();
            OnInteractionText(false);

            tutorialEvents.isInteracting = false;
            tutorialEvents.waitOnInteract = false;
            return;
        }
    }
    private void WaitOnEscape()
    {
        tutorialEvents.waitOnEscape = true;

        if (tutorialEvents.isEscaping)
        {
            currentIndication++;

            ChangeText();
            OnEscapeText(false);

            tutorialEvents.isEscaping = false;
            tutorialEvents.waitOnEscape = false;
            return;
        }
    }
    private void WaitOnInventory()
    {
        if (tutorialEvents.isInventory)
        {
            currentIndication++;

            ChangeText();
            OnInventoryText(false);
            return;
        }
    }
    private void Update()
    {
        if(waitingOnInteraction)
        {
            WaitOnInteraction();
        }
        if (waitingOnEscape)
        {
            WaitOnEscape();
        }
        if (waitingOnInventory)
        {
            WaitOnInventory();
        }

        switch (currentIndication)
        {
            case 0:
                if(tutorialEvents.isMoving)
                {
                    currentIndication++;
                    tutorialEvents.waitOnSprint = true;

                    ChangeText();
                }
                break;
            case 1:
                if (tutorialEvents.isSprinting)
                {
                    currentIndication++;
                    tutorialEvents.waitOnJump = true;

                    ChangeText();
                }
                break;
            case 2:
                if (tutorialEvents.isJumping)
                {
                    currentIndication++;

                    ChangeInteractionText("Press E to skip");
                    OnInteractionText(true);
                    ChangeText();
                }
                break;
            case 4:
                if (FindObjectOfType<CollectNotes>().waitForTutorial)
                {
                    FindObjectOfType<CollectNotes>().waitForTutorial = false;
                }

                if (tutorialEvents.isReadingNotepad)
                {
                    currentIndication++;
                    ChangeInteractionText("Press Esc to close");
                    OnEscapeText(true);
                    ChangeText();
                }
                break;
            case 5:
                if (tutorialEvents.isEscaping)
                {
                    currentIndication++;
                    tutorialEvents.waitOnEscape = false;

                    ChangeText();
                }
                break;
            case 6:
                if (tutorialEvents.isReadingNotepad)
                {
                    currentIndication++;
                    ChangeInteractionText("Press Esc to close");
                    OnEscapeText(true);
                    ChangeText();
                }
                break;
            case 8:
                if(!waitingOnInventory)
                {
                    FindObjectOfType<NotesMenu>().waitForTutorial = false;

                    ChangeInteractionText("Press I to open inventory");
                    OnInventoryText(true);
                }
                break;
            case 9:

                currentIndication++;
                ChangeInteractionText("Press Esc or I to close");
                OnEscapeText(true);

                break;
            case 11:
                if(!tutorialEvents.isInventory)
                {
                    ChangeInteractionText("Press E to skip");
                    OnInteractionText(true);
                    ChangeText();
                }
                break;
            case 12:

                currentIndication++;

                DisablePopup();
                FindObjectOfType<NPCQuestionareInteraction>().waitForTutorial = false;

                break;
            case 13:
                if(tutorialEvents.isAnswer)
                {
                    EnablePopup();

                    ChangeInteractionText("Press E to skip");
                    OnInteractionText(true);
                    ChangeText();
                }
                break;
            case 14:

                currentIndication++;

                ChangeInteractionText("Press E to skip");
                OnInteractionText(true);
                ChangeText();
                
                break;
            case 16:

                currentIndication++;

                ChangeInteractionText("Press E to skip");
                OnInteractionText(true);
                ChangeText();

                break;
            case 18:

                currentIndication++;

                ChangeInteractionText("Press E to skip");
                OnInteractionText(true);
                ChangeText();

                break;
            case 20:
                currentIndication++;
                DisablePopup();
                break;
        }
    }
}
