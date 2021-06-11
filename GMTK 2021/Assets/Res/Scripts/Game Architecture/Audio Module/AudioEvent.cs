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

    public IEnumerator Start()
    {
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

public class MusicEvent : AudioEvent
{
    private FMOD.Studio.EventInstance _instance;
    private string _eventName = "event:/Warning Noise/New Event";

    public MusicEvent(string name)
    {
        _eventName = name;
        _instance = FMODUnity.RuntimeManager.CreateInstance(_eventName);
    }

    public MusicEvent()
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
        _instance.setPaused(_paused);
    }

    public void FadeStop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _instance.release();
        released = true;
    }

    public void HardStop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _instance.release();
        released = true;
    }
}