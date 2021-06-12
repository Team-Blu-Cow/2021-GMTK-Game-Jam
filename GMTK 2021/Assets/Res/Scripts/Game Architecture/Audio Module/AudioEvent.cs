using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent
{
    public bool released = false;

    private FMOD.Studio.EventInstance _instance;

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

    public void TogglePause()
    {
        bool _paused;
        _instance.getPaused(out _paused);
        _instance.setPaused(!_paused);
    }

    public void Pause()
    {
        bool _paused;
        _instance.getPaused(out _paused);
        if (_paused)
            return;

        _instance.setPaused(true);
    }

    public void Unpause()
    {
        bool _paused;
        _instance.getPaused(out _paused);
        if (!_paused)
            return;

        _instance.setPaused(false);
    }

    public void FadeStop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void HardStop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void DeleteEvent()
    {
        _instance.release();
        released = true;
    }

    public void SetParameter(string name, float value)
    {
        _instance.setParameterByName(name, value);
    }
}