using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSensitivitySlider;
    public TMP_Text volumePercentageText;
    public TMP_Text mouseSensitivityPercentageText;

    public string textFormat = "{0:00}%";

    private void Start()
    {
        OnSettingsUpdated();
        SettingsManager.instance.settingsUpdated += OnSettingsUpdated;
    }

    public void OnSettingsUpdated()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f), mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", Constants.instance.DEFAULT_MOUSE_SENSITIVITY);
        
        volumeSlider.value = volume;
        mouseSensitivitySlider.value = mouseSensitivity;

        if(volumePercentageText != null)
            volumePercentageText.text = string.Format(textFormat, volume * 100f);
        if(mouseSensitivityPercentageText != null)
            mouseSensitivityPercentageText.text = string.Format(textFormat, (mouseSensitivity - mouseSensitivitySlider.minValue) / (mouseSensitivitySlider.maxValue - mouseSensitivitySlider.minValue) * 100f);
    }
}
