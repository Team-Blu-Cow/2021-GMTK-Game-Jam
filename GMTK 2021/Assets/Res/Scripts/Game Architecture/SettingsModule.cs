using UnityEngine;

namespace bluModule
{
    public class SettingsModule : Module
    {
        public AudioSettings audioSettings = new AudioSettings();
        public SaveData saveData = new SaveData();

        public void Init()
        {
            audioSettings.Init();
            saveData.init();
        }
    }

    public class AudioSettings
    {
        private FMOD.Studio.Bus masterBus;
        private FMOD.Studio.Bus SFXBus;
        private FMOD.Studio.Bus musicBus;
        private float masterVol = 0f;
        private float SFXVol = 0f;
        private float musicVol = 0f;

        public void Init()
        {
            masterVol = PlayerPrefs.GetFloat("MasterVolume", 1f);
            SFXVol = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            masterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
            SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
            musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
            masterBus.setVolume(masterVol);
            SFXBus.setVolume(SFXVol);
            musicBus.setVolume(musicVol);
        }

        public void SetMasterVolume(float in_vol)
        {
            masterVol = in_vol;
            PlayerPrefs.SetFloat("MasterVolume", masterVol);
            masterBus.setVolume(masterVol);
        }

        public void SetSFXVolume(float in_vol)
        {
            SFXVol = in_vol;
            PlayerPrefs.SetFloat("SFXVolume", SFXVol);
            SFXBus.setVolume(SFXVol);
        }

        public void SetMusicVolume(float in_vol)
        {
            musicVol = in_vol;
            PlayerPrefs.SetFloat("MusicVolume", musicVol);
            musicBus.setVolume(musicVol);
        }

        public float GetMasterVolume()
        {
            return masterVol;
        }

        public float GetSFXVolume()
        {
            return SFXVol;
        }

        public float GetMusicVolume()
        {
            return musicVol;
        }
    }

    public class SaveData
    {
        private int levelsComplete = 0;

        public void init()
        {
            levelsComplete = PlayerPrefs.GetInt("LevelsComplete", 0);
        }

        public int GetLevelsComplete()
        {
            return levelsComplete;
        }

        public void SetLevelsComplete(int input)
        {
            levelsComplete = input;
            PlayerPrefs.SetInt("LevelsComplete", input);
        }
    }
}