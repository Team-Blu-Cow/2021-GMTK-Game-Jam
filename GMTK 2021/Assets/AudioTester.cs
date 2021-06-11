using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioTester : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            bluModule.Application.instance.audioModule.NewOneShot("event:/player/footstep");
        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            bluModule.Application.instance.audioModule.NewMusicEvent("event:/music/New Event");
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            bluModule.Application.instance.audioModule.StopMusicEvent("event:/music/New Event");
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            bluModule.Application.instance.audioModule.StopMusicEvent("event:/music/New Event", true);
        }
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            bluModule.Application.instance.audioModule.PauseMusicEvent("event:/music/New Event");
        }
    }
}