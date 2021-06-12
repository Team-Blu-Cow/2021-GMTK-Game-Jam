using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioTester : MonoBehaviour
{
    private float ratio = 0f;
    // Start is called before the first frame update

    private void Start()
    {
        bluModule.Application.instance.audioModule.PlayMusicEvent("event:/music/Main theme");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            ratio += 0.2f;
            if (ratio > 1)
            {
                ratio = 1;
            }
            bluModule.Application.instance.audioModule.GetMusicEvent("event:/music/Main theme").SetParameter("Track Ratio", ratio);
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            ratio -= 0.2f;
            if (ratio < 0)
            {
                ratio = 0;
            }
            bluModule.Application.instance.audioModule.GetMusicEvent("event:/music/Main theme").SetParameter("Track Ratio", ratio);
        }
    }
}