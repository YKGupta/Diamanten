using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSensitivitySlider;
    public Slider playerSpeedMultiplierSlider;
    public TMP_Dropdown qualitySettingsDropdown;
    public TMP_Text volumePercentageText;
    public TMP_Text mouseSensitivityPercentageText;
    public TMP_Text playerSpeedMultiplierPercentageText;

    public string textFormat = "{0:00}%";

    private void Start()
    {
        OnSettingsUpdated();
        SettingsManager.instance.settingsUpdated += OnSettingsUpdated;
    }

    public void OnSettingsUpdated()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f), mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", Constants.instance.DEFAULT_MOUSE_SENSITIVITY), playerSpeedMultiplier = PlayerPrefs.GetFloat("speedMultiplier", 1f);
        int index = PlayerPrefs.GetInt("qualitySettings", Constants.instance.DEFAULT_QUALITY_SETTINGS);
        
        volumeSlider.value = volume;
        mouseSensitivitySlider.value = mouseSensitivity;
        playerSpeedMultiplierSlider.value = playerSpeedMultiplier;
        qualitySettingsDropdown.value = index;

        if(volumePercentageText != null)
            volumePercentageText.text = string.Format(textFormat, volume * 100f);
        if(mouseSensitivityPercentageText != null)
            mouseSensitivityPercentageText.text = string.Format(textFormat, (mouseSensitivity - mouseSensitivitySlider.minValue) / (mouseSensitivitySlider.maxValue - mouseSensitivitySlider.minValue) * 100f);
        if(playerSpeedMultiplierPercentageText != null)
            playerSpeedMultiplierPercentageText.text = string.Format(textFormat, (playerSpeedMultiplier - playerSpeedMultiplierSlider.minValue) / (playerSpeedMultiplierSlider.maxValue - playerSpeedMultiplierSlider.minValue) * 100f);
    }
}
