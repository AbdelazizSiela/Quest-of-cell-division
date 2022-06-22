using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectNotes : MonoBehaviour
{
    private Image noteImage;

    private Sprite currentNoteSprite;
    private bool noteDisplayed;

    private PlayerStats statsScript;

    private CharacterController controller;

    [SerializeField] private AudioClip notepad_pickup_sound;

    [HideInInspector] public bool waitForTutorial;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        noteImage = GameObject.Find("NoteImage").GetComponent<Image>();
        noteImage.gameObject.SetActive(false);

        statsScript = GetComponent<PlayerStats>();

        if(FindObjectOfType<TutorialEvents>())
        {
            waitForTutorial = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Note") && !noteDisplayed)
        {
            if (waitForTutorial)
            {
                return;
            }

            currentNoteSprite = other.GetComponent<Note>().noteSprite;
            DisplayNote();

            Destroy(other.gameObject);
        }
    }

    private void DisplayNote()
    {
        AudioSource.PlayClipAtPoint(notepad_pickup_sound, transform.TransformPoint(controller.center), AudioSettings.sfxVolume);

        statsScript.isInteracting = true;

        NotesMenu.collectedNotes.Add(currentNoteSprite);  

        noteImage.sprite = currentNoteSprite;
        noteImage.gameObject.SetActive(true);

        noteDisplayed = true;

        if(FindObjectOfType<TutorialEvents>())
        {
            FindObjectOfType<TutorialEvents>().isReadingNotepad = true;
        }
    }

    private void OnEscape()
    {
        if(noteDisplayed)
        {
            noteImage.gameObject.SetActive(false);

            noteDisplayed = false;

            statsScript.isInteracting = false;

            if (FindObjectOfType<TutorialEvents>())
            {
                FindObjectOfType<TutorialEvents>().isReadingNotepad = false;
            }
        }
    }
}
