using UnityEngine;
using NaughtyAttributes;

public class Flashlight : MonoBehaviour
{
    [BoxGroup("Controls")]
    public KeyCode ToggleKey;
    [BoxGroup("Controls")]
    [ReadOnly]
    public bool useLight = false;
    
    [BoxGroup("Settings")]
    public new Light light;
    [BoxGroup("Settings")]
    [Range(1f, 20f)]
    public float range = 10f;
    [BoxGroup("Settings")]
    [Range(1f, 100f)]
    public float intensity = 50f;
    [BoxGroup("Settings")]
    [OnValueChanged("OnColorChanged")]
    public Color color;


    private GameObject LightGO;

    private void Start()
    {
        LightGO = light.gameObject;
        useLight = false;
        SetLight(false, false);
    }

    private void Update()
    {
        if(!useLight)
            return;
        
        if(Input.GetKeyDown(ToggleKey))
            SetLight(!LightGO.activeSelf);
    }
    
    [ContextMenu("Update Settings")]
    public void UpdateSettings()
    {
        light.range = range;
        light.intensity = intensity;
        light.color = color;
    }

    public void OnColorChanged()
    {
        light.color = color;
    }

    public void SetLight(bool state = true, bool playSound = true)
    {        
        LightGO.SetActive(state);
        if(playSound)
            SoundManager.PlaySound(state ? SoundType.LanternOn : SoundType.LanternOff);
    }
}
