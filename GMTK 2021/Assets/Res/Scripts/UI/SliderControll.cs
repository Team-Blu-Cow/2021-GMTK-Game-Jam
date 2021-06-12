using UnityEngine;
using UnityEngine.UI;

public class SliderControll : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();

        sliders[0].value = bluModule.Application.instance.settingsModule.audioSettings.GetMasterVolume();
        sliders[0].onValueChanged.AddListener(OnValueChangeMaster);

        sliders[1].value = bluModule.Application.instance.settingsModule.audioSettings.GetMusicVolume();
        sliders[1].onValueChanged.AddListener(OnValueChangeMusic);

        sliders[2].value = bluModule.Application.instance.settingsModule.audioSettings.GetSFXVolume();
        sliders[2].onValueChanged.AddListener(OnValueChangeSFX);
    }

    public void OnValueChangeMaster(float value)
    {
        bluModule.Application.instance.settingsModule.audioSettings.SetMasterVolume(value);
    }

    public void OnValueChangeMusic(float value)
    {
        bluModule.Application.instance.settingsModule.audioSettings.SetMusicVolume(value);
    }

    public void OnValueChangeSFX(float value)
    {
        bluModule.Application.instance.settingsModule.audioSettings.SetSFXVolume(value);
    }
}