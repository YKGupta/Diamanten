using UnityEngine;
using NaughtyAttributes;

public class SurgicalChartEffects : MonoBehaviour
{
    [BoxGroup("Settings")]
    public Color presentColor;
    [BoxGroup("Settings")]
    public Color absentColor;
    [BoxGroup("Settings")]
    public int strength;
    [BoxGroup("Settings")]
    [ReadOnly]
    public Material mat;

    private void Start()
    {
        mat = transform.GetComponent<MeshRenderer>().material;
        presentColor.r *= 1 << strength;
        presentColor.g *= 1 << strength;
        presentColor.b *= 1 << strength;
        absentColor.r *= 1 << strength;
        absentColor.g *= 1 << strength;
        absentColor.b *= 1 << strength;
        Reset();
    }

    public void Show(Texture tex, bool present)
    {
        mat.SetTexture("_EmissionMap", tex);
        mat.SetColor("_EmissionColor", present ? presentColor : absentColor);
        mat.EnableKeyword("_EMISSION");
    }

    public void Reset()
    {
        mat.SetTexture("_EmissionMap", null);
        mat.DisableKeyword("_EMISSION");
    }
}
