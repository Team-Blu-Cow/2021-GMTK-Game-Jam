using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGetter : MonoBehaviour
{
    public bluModule.AudioSettings audioSettings;

    private void Start()
    {
        audioSettings = GameObject.FindObjectOfType<bluModule.SettingsModule>().audioSettings;
    }

    public void SetMasterVolume(float input)
    {
        audioSettings.SetMasterVolume(input);
    }

    public void SetSFXVolume(float input)
    {
        audioSettings.SetSFXVolume(input);
    }

    public void SetMusicVolume(float input)
    {
        audioSettings.SetMusicVolume(input);
    }
}