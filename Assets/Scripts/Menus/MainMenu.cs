using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject currentMenu;
    [SerializeField] private GameObject mainMenu;

    public void Play()
    {
        Invoke("LoadTutorial",0.5f);
    }
    private void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void setCurrentMenu(GameObject newMenu)
    {
        currentMenu = newMenu;
    }
    private void OnEscape()
    {
        if(currentMenu == null)
        {
            return;
        }

        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
