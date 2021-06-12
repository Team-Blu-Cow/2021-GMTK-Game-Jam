using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bluModule
{
    public class AudioModule : Module
    {
        private Dictionary<string, AudioEvent> _musicEvents = new Dictionary<string, AudioEvent>();
        private Dictionary<string, AudioEvent> _audioEvents = new Dictionary<string, AudioEvent>();

        public void NewAudioEvent(string name) // use "object/event"
        {                                      // e.g. "player/footstep"
            if (name != null)
            {
                _audioEvents.Add(name, new AudioEvent(name));
            }
            else
            {
                _audioEvents.Add(null, new AudioEvent());
            }
        }

        public void NewMusicEvent(string name) // use "object/event"
        {                                      // e.g. "player/footstep"
            if (name != null)
            {
                _musicEvents.Add(name, new AudioEvent(name));
            }
            else
            {
                _musicEvents.Add(name, new AudioEvent());
            }
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

        public void PlayAudioEvent(string name) // use copied path from event browser
        {                                       // e.g. "event:/player/footstep"
            _audioEvents[name].Play();
        }

        public void PlayMusicEvent(string name) // use copied path from event browser
        {                                       // e.g. "event:/player/footstep"
            _musicEvents[name].Play();
        }

        public void DeleteAudioEvent(string name)
        {
            _audioEvents[name].DeleteEvent();
        }

        public void DeleteMusicEvent(string name)
        {
            _musicEvents[name].DeleteEvent();
        }

        public void Init()
        {
            NewAudioEvent("event:/player/footstep");
            NewAudioEvent("event:/environment/objects/interactables/pick up");
            NewAudioEvent("event:/environment/objects/interactables/put down");
            NewAudioEvent("event:/environment/objects/nodes/button insert");
            NewAudioEvent("event:/environment/objects/nodes/button remove");
            NewMusicEvent("event:/music/Beef Stroganoff");
        }
    }
}