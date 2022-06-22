using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    private bool canStartGame;

    [SerializeField] private GameObject introMenu,mainMenu;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);
        canStartGame = true;
    }

    private void OnEnter()
    {
        if(!canStartGame)
        {
            return;
        }

        introMenu.SetActive(false);
        mainMenu.SetActive(true);

        canStartGame = false;
    }
}
