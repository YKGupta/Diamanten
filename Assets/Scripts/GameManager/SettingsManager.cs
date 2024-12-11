using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public Action settingsUpdated;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider mouseSensitivitySlider;

    public static SettingsManager instance;

    private void Awake()
    {
        if(instance != null)
            return;
        instance = this;
    }

    private void Start()
    {
        audioMixer.SetFloat("volume", (float)Math.Log10(PlayerPrefs.GetFloat("volume", 1f)) * 20);
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity", Constants.instance.DEFAULT_MOUSE_SENSITIVITY);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", (float)Math.Log10(value) * 20);
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
        settingsUpdated?.Invoke();
    }

    public void SetMouseSensitivty(float value)
    {
        PlayerPrefs.SetFloat("mouseSensitivity", value);
        PlayerPrefs.Save();
        settingsUpdated?.Invoke();
    }
}
