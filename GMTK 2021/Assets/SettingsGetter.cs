using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGetter : MonoBehaviour
{
    public AudioSettings settingsMod;

    private void Start()
    {
        GameObject.FindObjectOfType<SettingsModule>()
    }
}