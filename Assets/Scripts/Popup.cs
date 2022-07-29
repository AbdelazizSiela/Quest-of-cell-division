using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject popupGO, interactionHint;
    [SerializeField] private TextMeshProUGUI popupText,interactionText;
    [SerializeField] private GameObject[] keyBinds;
    [SerializeField] private GameObject keybindE, keybindI, keybindEsc;

    [SerializeField] private string[] Indications;

    private TutorialEvents tutorialEvents;

    private int currentIndication;
    private bool waitingOnInteraction,waitingOnEscape,waitingOnInventory;

    private IEnumerator Start()
    {
        tutorialEvents = FindObjectOfType<TutorialEvents>();

        yield return new WaitForSeconds(0f);

        keyBinds[0].SetActive(true);
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

            keybindEsc.SetActive(false);
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

                    keyBinds[0].SetActive(false);
                    keyBinds[1].SetActive(true);

                    ChangeText();
                }
                break;
            case 1:
                if (tutorialEvents.isSprinting)
                {
                    currentIndication++;
                    tutorialEvents.waitOnJump = true;

                    keyBinds[1].SetActive(false);
                    keyBinds[2].SetActive(true);

                    ChangeText();
                }
                break;
            case 2:
                if (tutorialEvents.isJumping)
                {
                    keyBinds[2].SetActive(false);
                    keybindE.SetActive(true);

                    currentIndication++;

                    ChangeInteractionText("Press    to skip");
                    OnInteractionText(true);
                    ChangeText();
                }
                break;
            case 4:
                if (FindObjectOfType<CollectNotes>().waitForTutorial)
                {
                    keybindE.SetActive(false);
                    FindObjectOfType<CollectNotes>().waitForTutorial = false;
                }

                if (tutorialEvents.isReadingNotepad)
                {
                    keybindEsc.SetActive(true);

                    currentIndication++;
                    ChangeInteractionText("Press    to close");
                    OnEscapeText(true);
                    ChangeText();
                }
                break;
            case 5:
                if (tutorialEvents.isEscaping)
                {
                    keybindEsc.SetActive(false);

                    currentIndication++;
                    tutorialEvents.waitOnEscape = false;

                    ChangeText();
                }
                break;
            case 6:
                if (tutorialEvents.isReadingNotepad)
                {
                    keybindEsc.SetActive(true);

                    currentIndication++;
                    ChangeInteractionText("Press    to close");
                    OnEscapeText(true);
                    ChangeText();
                }
                break;
            case 8:
                if(!waitingOnInventory)
                {
                    keybindI.SetActive(true);

                    FindObjectOfType<NotesMenu>().waitForTutorial = false;

                    ChangeInteractionText("Press     to open inventory");
                    OnInventoryText(true);
                }
                break;
            case 9:

                keybindI.SetActive(false);
                keybindEsc.SetActive(true);
                keyBinds[3].SetActive(true);

                currentIndication++;
                ChangeInteractionText("Press    to close");
                OnEscapeText(true);

                break;
            case 11:
                if(!tutorialEvents.isInventory)
                {
                    keyBinds[3].SetActive(false);
                    keybindEsc.SetActive(false);
                    keybindE.SetActive(true);

                    ChangeInteractionText("Press    to skip");
                    OnInteractionText(true);
                    ChangeText();
                }
                break;
            case 12:
                keybindE.SetActive(false);

                currentIndication++;

                DisablePopup();
                FindObjectOfType<NPCQuestionareInteraction>().waitForTutorial = false;

                break;
            case 13:
                if(tutorialEvents.isAnswer)
                {
                    EnablePopup();
                    keybindE.SetActive(true);

                    ChangeInteractionText("Press    to skip");
                    OnInteractionText(true);
                    ChangeText();
                }
                break;
            case 14:

                currentIndication++;

                ChangeInteractionText("Press    to skip");
                OnInteractionText(true);
                ChangeText();
                
                break;
            case 16:

                currentIndication++;

                ChangeInteractionText("Press    to skip");
                OnInteractionText(true);
                ChangeText();

                break;
            case 18:

                currentIndication++;

                ChangeInteractionText("Press    to skip");
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
