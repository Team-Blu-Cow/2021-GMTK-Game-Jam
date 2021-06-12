using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bluModule
{
    public class AudioModule : Module
    {
        private Dictionary<string, AudioEvent> _musicEvents = new Dictionary<string, AudioEvent>();
        private Dictionary<string, AudioEvent> _audioEvents = new Dictionary<string, AudioEvent>();

        public void NewAudioEvent(string name, int poly = 0) // use "object/event"
        {                                      // e.g. "player/footstep"
            if (name != null)
            {
                if (poly == 0)
                    _audioEvents.Add(name, new AudioEvent(name));
                else
                    _audioEvents.Add(name, new AudioEvent(name, poly));
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

        public void TogglePauseMusicEvent(string name)
        {
            _musicEvents[name].TogglePause();
        }

        public void PauseMusicEvent(string name)
        {
            _musicEvents[name].Pause();
        }

        public void UnpauseMusicEvent(string name)
        {
            _musicEvents[name].Unpause();
        }

        public void StopMusicEvent(string name, bool fade = false)
        {
            if (fade)

                _musicEvents[name].FadeStop();
            else
                _musicEvents[name].HardStop();
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

        public AudioEvent GetAudioEvent(string name)
        {
            return _audioEvents[name];
        }

        public AudioEvent GetMusicEvent(string name)
        {
            return _musicEvents[name];
        }

        public void StopAllEvents(bool fade = false)
        {
            if (fade)
            {
                GetMusicEvent("event:/music/Main theme").SetParameter("Track Ratio", 0f);
                FMODUnity.RuntimeManager.GetBus("bus:/Master").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            else
            {
                GetMusicEvent("event:/music/Main theme").SetParameter("Track Ratio", 0f);
                FMODUnity.RuntimeManager.GetBus("bus:/Master").stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }

        public void Init()
        {
            CreateEvents();
            Debug.Log("Attempting to init settings module");
            bluModule.Application.instance.settingsModule.Init();
        }

        private void CreateEvents()
        {
            NewAudioEvent("event:/player/footstep", 20);
            NewAudioEvent("event:/environment/objects/interactables/plugs/pick up", 5);
            NewAudioEvent("event:/environment/objects/interactables/plugs/put down", 5);
            NewAudioEvent("event:/environment/objects/nodes/button insert", 5);
            NewAudioEvent("event:/environment/objects/nodes/button remove", 5);
            NewAudioEvent("event:/environment/objects/interactables/pressure plates/insert", 5);
            NewAudioEvent("event:/environment/objects/interactables/pressure plates/remove", 5);
            NewAudioEvent("event:/environment/objects/interactables/door/open", 3);
            NewAudioEvent("event:/environment/objects/interactables/door/close", 3);
            NewAudioEvent("event:/UI/buttons/on click");
            NewMusicEvent("event:/music/Beef Stroganoff");
            NewMusicEvent("event:/music/Main theme");
        }
    }
}