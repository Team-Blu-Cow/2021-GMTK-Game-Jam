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
            bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button remove");
        }
        
    }
}