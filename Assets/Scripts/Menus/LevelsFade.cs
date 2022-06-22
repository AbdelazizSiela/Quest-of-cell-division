using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsFade : MonoBehaviour
{
    [SerializeField] private GameObject fadeScreen;

   public void LoadNextLevel()
    {
        fadeScreen.SetActive(true);
        Invoke("LoadNextScene",0.5f);
    }

    private void LoadNextScene()
    {
        NotesMenu.SetOriginalNotes();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
