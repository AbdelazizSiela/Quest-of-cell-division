using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotesMenu : MonoBehaviour
{
    [SerializeField] private GameObject notesMenu;

    public static List<Sprite> collectedNotes, originalNotes;
    public static int currentIndex,originalIndex;

    private StarterAssets.ThirdPersonController movementScript;
    private Animator Anim;
    private PlayerStats statsScript;

    private CharacterController controller;

    [SerializeField] private AudioClip notepad_pickup_sound;

    [HideInInspector] public bool waitForTutorial;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        movementScript = GetComponent<StarterAssets.ThirdPersonController>();
        statsScript = GetComponent<PlayerStats>();

        if (collectedNotes == null)
        {
            collectedNotes = new List<Sprite>();
        }

        if (originalNotes == null)
        {
            originalNotes = new List<Sprite>();
        }

        if (FindObjectOfType<TutorialEvents>())
        {
            waitForTutorial = true;
        }
    }
    public static void SetOriginalNotes()
    {
        originalNotes = new List<Sprite>();

        for (int i = 0; i < collectedNotes.Count; i++)
        {
            originalNotes.Add(collectedNotes[i]);
        }

        originalIndex = currentIndex;
    }
    private void OnInventory()
    {
        if (waitForTutorial)
        {
            return;
        }
        if (statsScript.isInteracting || collectedNotes.Count == 0)
        {
            return;
        }

        if (!movementScript.enabled && !notesMenu.activeSelf)
        {
            return;
        }

        if(!notesMenu.activeSelf)
        {
            notesMenu.SetActive(true);

            if (FindObjectOfType<TutorialEvents>())
            {
                FindObjectOfType<TutorialEvents>().isInventory = true;
            }

            statsScript.isInventory = true;
            movementScript.enabled = false;
            Anim.enabled = false;
            UpdateDisplay();
        }
    }
    private void OnEscape()
    {
        if(notesMenu.activeSelf)
        {
            if (FindObjectOfType<TutorialEvents>())
            {
                FindObjectOfType<TutorialEvents>().isInventory = false;
            }

            notesMenu.SetActive(false);

            Anim.enabled = true;
            movementScript.enabled = true;
            statsScript.isInventory = false;
        }

    }

    private void OnLeftNavigation()
    {
        if(!notesMenu.activeSelf || currentIndex <= 0)
        {
            return;
        }

        currentIndex--;
        UpdateDisplay();
    }
    private void OnRightNavigation()
    {
        if (!notesMenu.activeSelf || currentIndex >= collectedNotes.Count - 1)
        {
            return;
        }

        currentIndex++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        AudioSource.PlayClipAtPoint(notepad_pickup_sound, transform.TransformPoint(controller.center), AudioSettings.sfxVolume);

        notesMenu.transform.Find("Note").GetComponent<Image>().sprite = collectedNotes[currentIndex];
    }
}
