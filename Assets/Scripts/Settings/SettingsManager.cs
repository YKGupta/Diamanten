using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public Action settingsUpdated;
    public AudioMixer audioMixer;

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
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualitySettings", Constants.instance.DEFAULT_QUALITY_SETTINGS));
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

    public void SetPlayerSpeedMultiplier(float value)
    {
        PlayerPrefs.SetFloat("speedMultiplier", value);
        PlayerPrefs.Save();
        settingsUpdated?.Invoke();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("qualitySettings", index);
        PlayerPrefs.Save();
        settingsUpdated?.Invoke();
    }
}
