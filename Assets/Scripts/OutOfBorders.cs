using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBorders : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float dangerY;

    private Vector3 originalPos;

    private void Awake()
    {
        originalPos = player.transform.position;
    }
    private void Update()
    {
        if(player.position.y <= dangerY)
        {
            /* NotesMenu.currentIndex = NotesMenu.originalIndex;
             NotesMenu.collectedNotes = new List<Sprite>();

             for (int i = 0; i < NotesMenu.originalNotes.Count; i++)
             {
                 NotesMenu.collectedNotes.Add(NotesMenu.originalNotes[i]);
             }


             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); */

            player.GetComponent<CharacterController>().enabled = false;

            if (FindObjectOfType<Health>())
            {
                FindObjectOfType<Health>().TakeDamage(10);
            }

            player.transform.position = originalPos;

            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
