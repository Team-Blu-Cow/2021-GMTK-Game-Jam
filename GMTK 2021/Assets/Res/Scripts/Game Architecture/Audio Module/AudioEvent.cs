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
        _instance = FMODUnity.RuntimeManager.CreateInstance(_eventName);
    }

    public AudioEvent()
    {
        _instance = FMODUnity.RuntimeManager.CreateInstance(_eventName);
    }

    public void Play()
    {
        _instance.start();
    }

    public void Pause()
    {
        bool _paused;
        _instance.getPaused(out _paused);
        _instance.setPaused(!_paused);
    }

    public void FadeStop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void HardStop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void DeleteEvent()
    {
        _instance.release();
        released = true;
    }
}