using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestionnaire : MonoBehaviour
{
    public string Name;
    public string Question;
    public string falseEntryMessage;

    public string[] Entries;
    public string[] Phrases;


    public int correctEntery;

    public GameObject myDoor;
    public Sprite myFigure;
}

