using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGetter : MonoBehaviour
{
    public bluModule.AudioSettings settingsMod;

    private void Start()
    {
        settingsMod = GameObject.FindObjectOfType<bluModule.SettingsModule>().audioSettings;
    }

    public void SetMasterVolume(float input)
    {
        settingsMod.SetMasterVolume(input);
    }
}