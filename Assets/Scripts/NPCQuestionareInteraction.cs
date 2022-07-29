using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCQuestionareInteraction : MonoBehaviour
{
    private NPCQuestionnaire currentNPC;
    private GameObject QuestionareBox;
    private TextMeshProUGUI nameText, questionText, dialogText;
    private GameObject figureBox;
    private CharacterController controller;

    [SerializeField] private TextMeshProUGUI[] entriesTexts;
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject endingScene;
    [SerializeField] private AudioClip door_open_sound;
    [SerializeField] private GameObject resultsMenu;

    private bool startedConversation,isAnsweredCorrect,isAnsweredFalse;
    private int currentPhraseIndex;

    private PlayerStats statsScript;
    private LevelsFade levelsFadeScript;

    [HideInInspector] public bool waitForTutorial;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        QuestionareBox = GameObject.Find("QuestionareBox");
        figureBox = GameObject.Find("FigureBox");
        controller = GetComponent<CharacterController>();

        figureBox.SetActive(false);

        nameText = QuestionareBox.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        questionText = QuestionareBox.transform.Find("Question-DialogText").GetComponent<TextMeshProUGUI>();

        dialogText = QuestionareBox.transform.Find("Question-DialogText").GetComponent<TextMeshProUGUI>();

        QuestionareBox.SetActive(false);

        statsScript = GetComponent<PlayerStats>();
        levelsFadeScript = FindObjectOfType<LevelsFade>();

        if (FindObjectOfType<TutorialEvents>())
        {
            waitForTutorial = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if(waitForTutorial)
        {
            return;
        }

      if(collision.CompareTag("NPCQuestionare") && currentNPC == null)
        {
            currentNPC = collision.GetComponent<NPCQuestionnaire>();
        }   
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("NPCQuestionare") && currentNPC != null)
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
        if(statsScript.isInventory)
        {
            return;
        }

        if(currentNPC != null)
        {
           statsScript.isInteracting = true;

            if (!startedConversation)
            {
                StartConversation();
            }
           else if(isAnsweredCorrect)
            {
               NextPhrase();
            }
            else if(isAnsweredFalse)
            {
                EndConversation();
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        QuestionareBox.SetActive(true);

        if(currentNPC.myFigure != null)
        {
            figureBox.SetActive(true);
            figureBox.transform.Find("Figure").GetComponent<Image>().sprite = currentNPC.myFigure;
        }
        for (int i = 0; i < currentNPC.Entries.Length; i++)
        {
            entriesTexts[i].transform.parent.gameObject.SetActive(true);
            entriesTexts[i].text = currentNPC.Entries[i];

            if(i == currentNPC.correctEntery)
            {
                entriesTexts[i].transform.parent.GetComponent<Entry>().isCorrect = true;
            }
        }
     

        questionText.text = currentNPC.Question;
        nameText.text = currentNPC.Name;
        startedConversation = true;
    }
    public void CorrectEntry()
    {
        for (int i = 0; i < currentNPC.Entries.Length; i++)
        {
            if (i == currentNPC.correctEntery)
            {
                entriesTexts[i].transform.parent.GetComponent<Entry>().isCorrect = false;
            }

            entriesTexts[i].transform.parent.gameObject.SetActive(false);
         
        }
        dialogText.text = currentNPC.Phrases[0];

        if(currentNPC.myDoor)
        {
            AudioSource.PlayClipAtPoint(door_open_sound, transform.TransformPoint(controller.center), AudioSettings.sfxVolume);
            currentNPC.myDoor.GetComponent<Animator>().SetTrigger("Open");

        }
     else
        {
            if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                if(resultsMenu != null)
                {
                    resultsMenu.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                controller.enabled = false;
                endingScene.SetActive(true);

                FindObjectOfType<EndMenu>().Invoke("ShowReturnMenu", 1f);
            }
        }

        isAnsweredCorrect = true;

        if(FindObjectOfType<ActivateProtectionSpheres>())
        {
            FindObjectOfType<ActivateProtectionSpheres>().ActivateNext();
        }

        interactionText.SetActive(true);
    }
    public void FalseEntry()
    {
        for (int i = 0; i < currentNPC.Entries.Length; i++)
        {
            entriesTexts[i].transform.parent.gameObject.SetActive(false);
        }

        dialogText.text = currentNPC.falseEntryMessage;

        isAnsweredFalse = true;

        interactionText.SetActive(true);

        if (FindObjectOfType<Health>())
        {
            FindObjectOfType<Health>().TakeDamage(5);
        }
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
        if(isAnsweredCorrect)
        {
            currentNPC.GetComponent<Collider>().enabled = false;

            if(FindObjectOfType<TutorialEvents>())
            {
                FindObjectOfType<TutorialEvents>().isAnswer = true;
            }
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        QuestionareBox.SetActive(false);

        figureBox.SetActive(false);
        

        currentPhraseIndex = 0;
        startedConversation = false;
        isAnsweredCorrect = false;
        isAnsweredFalse = false;
        currentNPC = null;

        statsScript.isInteracting = false;

        interactionText.SetActive(false);
    }
}
