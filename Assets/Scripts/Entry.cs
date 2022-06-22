using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public bool isCorrect;
    public NPCQuestionareInteraction questionareScript;

    private void Awake()
    {
        questionareScript = FindObjectOfType<NPCQuestionareInteraction>();   
    }

    public void CheckEntry()
    {
        if(isCorrect)
        {
            questionareScript.CorrectEntry();
        }
        else
        {
            questionareScript.FalseEntry();
        }
    }
}
