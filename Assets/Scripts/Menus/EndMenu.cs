using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private bool canReturn;

    public void ShowReturnMenu()
    {
        canReturn = true;
    }

    private void OnEscape()
    {
        if(!canReturn)
        {
            return;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Destroy(FindObjectOfType<Music>().gameObject);
        SceneManager.LoadScene(0);

    }
}
