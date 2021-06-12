using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodySoundController : MonoBehaviour
{
    public void Footstep()
    {
        bluModule.Application.instance.audioModule.PlayAudioEvent("event:/player/footstep");
    }
}