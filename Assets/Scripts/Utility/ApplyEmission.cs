using UnityEngine;

public class ApplyEmission : MonoBehaviour
{
    public Material mat;
    public Texture tex;
    public Color color;
    public int strength;

    private void Start()
    {
        color.r *= 1 << strength;
        color.g *= 1 << strength;
        color.b *= 1 << strength;
        Reset();
    }

    public void Emit()
    {
        mat.SetTexture("_EmissionMap", tex);
        mat.SetColor("_EmissionColor", color);
        mat.EnableKeyword("_EMISSION");
    }

    public void Reset()
    {
        mat.SetTexture("_EmissionMap", null);
        mat.DisableKeyword("_EMISSION");
    }
}
