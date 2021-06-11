using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bluModule
{
    public class AudioModule : Module
    {
        private List<AudioEvent> _audioEvents = new List<AudioEvent>();
        private Dictionary<string, MusicEvent> _musicEvents = new Dictionary<string, MusicEvent>();

        public void NewAudioEvent(string name) // use "object/event"
        {                                      // e.g. "player/footstep"
            if (name != null)
            {
                _audioEvents.Add(new AudioEvent(name));
            }
            else
            {
                _audioEvents.Add(new AudioEvent());
            }
            StartCoroutine(_audioEvents[_audioEvents.Count - 1].Start());
        }

        public void NewMusicEvent(string name) // use "object/event"
        {                                      // e.g. "player/footstep"
            if (name != null)
            {
                _musicEvents.Add(name, new MusicEvent(name));
            }
            else
            {
                _musicEvents.Add(name, new MusicEvent());
            }
            _musicEvents[name].Play();
        }

        public void PauseMusicEvent(string name)
        {
            _musicEvents[name].Pause();
        }

        public void StopMusicEvent(string name, bool fade = false)
        {
            if (fade)
            {
                _musicEvents[name].FadeStop();
            }
            else
            {
                _musicEvents[name].HardStop();
            }
        }

        public void NewOneShot(string name) // use copied path from event browser
        {                                   // e.g. "event:/player/footstep"
            FMODUnity.RuntimeManager.PlayOneShot(name);
        }

        private void Update() // TODO: @anyone optimise
        {
            if (_audioEvents.Count != 0)
            {
                foreach (AudioEvent aEvent in _audioEvents)
                {
                    if (aEvent.released)
                    {
                        _audioEvents.Remove(aEvent);
                        break;
                    }
                }
                Debug.Log(_audioEvents.Count);
            }

            if (_musicEvents.Count != 0)
            {
                foreach (KeyValuePair<string, MusicEvent> mEvent in _musicEvents)
                {
                    if (mEvent.Value.released)
                    {
                        _musicEvents.Remove(mEvent.Key);
                        break;
                    }
                }
                Debug.Log(_musicEvents.Count);
            }
        }
    }
}