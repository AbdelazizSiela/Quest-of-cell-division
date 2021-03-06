using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
