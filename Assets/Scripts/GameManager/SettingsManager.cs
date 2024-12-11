using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public Action settingsUpdated;

    public static SettingsManager instance;

    private void Awake()
    {
        if(instance != null)
            return;
        instance = this;
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        settingsUpdated?.Invoke();
    }

    public void SetMouseSensitivty(float value)
    {
        PlayerPrefs.SetFloat("mouseSensitivity", value);
        settingsUpdated?.Invoke();
    }
}
