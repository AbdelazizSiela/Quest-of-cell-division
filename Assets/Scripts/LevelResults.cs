using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelResults : MonoBehaviour
{
    private TimerManager timeScript;

    [SerializeField] private int twoStarTime, threeStarTime;

    [SerializeField] private GameObject secondStar, thirdStar;
    [SerializeField] private TextMeshProUGUI timerText,endingTimerText;

    private void Start()
    {
        timeScript = FindObjectOfType<TimerManager>();

        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<StarterAssets.ThirdPersonController>().enabled = false;

        endingTimerText.text = "You finished in " + timerText.text;

        if (timeScript.timeRemaining >= threeStarTime)
        {
            secondStar.SetActive(true);
            thirdStar.SetActive(true);

            return;
        }

        if (timeScript.timeRemaining >= twoStarTime)
        {
            secondStar.SetActive(true);

            return;
        }
    }

}
