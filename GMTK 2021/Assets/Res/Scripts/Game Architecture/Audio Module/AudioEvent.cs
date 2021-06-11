using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent
{
    public bool released = false;
    private FMOD.Studio.EventInstance _instance;
    private FMOD.Studio.PLAYBACK_STATE _STATE;
    private string _eventName = "event:/Warning Noise/New Event";

    public AudioEvent(string name)
    {
        _eventName = name;
    }

    public AudioEvent()
    {
    }

    public IEnumerator Start()
    {
        _instance = FMODUnity.RuntimeManager.CreateInstance(_eventName);
        _instance.start();
        while (_STATE != FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            _instance.getPlaybackState(out _STATE);
            yield return null;
        }
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _instance.release();
        released = true;
    }
}