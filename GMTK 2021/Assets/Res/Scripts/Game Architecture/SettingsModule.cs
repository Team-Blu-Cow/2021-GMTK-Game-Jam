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

            PlayerPrefs.Save();
        }

        public void SetSFXVolume(float in_vol)
        {
            SFXVol = in_vol;
            PlayerPrefs.SetFloat("SFXVolume", SFXVol);
            SFXBus.setVolume(SFXVol);

            PlayerPrefs.Save();
        }

        public void SetMusicVolume(float in_vol)
        {
            musicVol = in_vol;
            PlayerPrefs.SetFloat("MusicVolume", musicVol);
            musicBus.setVolume(musicVol);

            PlayerPrefs.Save();
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
        private bool fullscreen = false;
        private Resolution savedResolution = new Resolution();

        public void init()
        {
            savedResolution.width = PlayerPrefs.GetInt("resolutionX", 800);
            savedResolution.height = PlayerPrefs.GetInt("resolutiony", 450);
            levelsComplete = PlayerPrefs.GetInt("LevelsComplete", 0);
            fullscreen = PlayerPrefs.GetInt("fullscreen", 0) == 1 ? true : false;
        }

        public int GetLevelsComplete()
        {
            return levelsComplete;
        }

        public bool GetFullscreen()
        {
            return fullscreen;
        }

        public Resolution GetResolution()
        {
            return savedResolution;
        }

        public void SetLevelsComplete(int input)
        {
            if (input > levelsComplete)
            {
                levelsComplete = input;
                PlayerPrefs.SetInt("LevelsComplete", input);
                PlayerPrefs.Save();
            }
        }

        public void SetFullscreen(bool input)
        {
            fullscreen = input;
            PlayerPrefs.SetInt("fullscreen", input ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetResolution(Resolution input)
        {
            savedResolution = input;
            PlayerPrefs.SetInt("resolutionX", savedResolution.width);
            PlayerPrefs.SetInt("resolutiony", savedResolution.height);
            PlayerPrefs.Save();
        }
    }
}