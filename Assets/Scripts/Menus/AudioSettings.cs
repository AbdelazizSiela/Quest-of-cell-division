using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public static float musicVolume, sfxVolume;

    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("FirstTime") == 0)
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("SFXVolume", 1);

            PlayerPrefs.SetInt("FirstTime", 1);
        }

        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        musicSlider.value = musicVolume;
        musicSource.volume = musicVolume;
        musicSource.enabled = true;

        sfxSlider.value = sfxVolume;
    }

    public void OnChangeMusicVolume()
    {
        musicVolume = musicSlider.value;
        musicSource.volume = musicVolume;

        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void OnChangeSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
}
