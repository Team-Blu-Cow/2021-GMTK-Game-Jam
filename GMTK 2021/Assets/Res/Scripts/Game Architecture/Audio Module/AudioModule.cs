using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bluModule
{
    public class AudioModule : Module
    {
        private List<AudioEvent> _audioEvents = new List<AudioEvent>();

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
        }
    }
}