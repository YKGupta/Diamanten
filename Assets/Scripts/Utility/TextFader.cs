using UnityEngine;
using TMPro;
using NaughtyAttributes;
using System.Collections;

public class TextFader : MonoBehaviour
{
    [BoxGroup("Settings")]
    public TMP_Text text;
    [BoxGroup("Settings")]
    public float duration;

    [ContextMenu("Fade")]
    public void FadeInText()
    {
        text.ForceMeshUpdate();
        TMP_TextInfo textInfo = text.textInfo;
        int totalCharacters = textInfo.characterCount;

        float fadeStep = duration / totalCharacters;
        float overallStart = Time.time;

        for (int i = 0; i < totalCharacters; i++)
        {
            float fadeStartTime = overallStart + i * fadeStep;
            StartCoroutine(FadeCharacter(i, fadeStartTime, fadeStep));
        }
    }

    private IEnumerator FadeCharacter(int charIndex, float fadeStartTime, float duration)
    {
        TMP_CharacterInfo charInfo = text.textInfo.characterInfo[charIndex];

        if (!charInfo.isVisible)
            yield break;

        while(Time.time < fadeStartTime + duration)
        {
            float progress = Mathf.Clamp01((Time.time - fadeStartTime) / duration);
            UpdateCharacterAlpha(charIndex, progress);
            yield return null;
        }
        UpdateCharacterAlpha(charIndex, 1f);
    }

    private void UpdateCharacterAlpha(int ind, float alpha)
    {
        TMP_TextInfo info = text.textInfo;
        int materialIndex = info.characterInfo[ind].materialReferenceIndex;
        int vertexIndex = info.characterInfo[ind].vertexIndex;

        Color32[] vertexColors = info.meshInfo[materialIndex].colors32;
        byte alphaByte = (byte)(alpha * 255); // 0-1 => 0-255

        vertexColors[vertexIndex + 0].a = alphaByte;
        vertexColors[vertexIndex + 1].a = alphaByte;
        vertexColors[vertexIndex + 2].a = alphaByte;
        vertexColors[vertexIndex + 3].a = alphaByte;

        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
