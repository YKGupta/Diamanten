using UnityEngine;
using UnityEngine.Events;
using TMPro;
using NaughtyAttributes;
using System.Collections;

public class TextFader : MonoBehaviour
{
    [BoxGroup("Settings")]
    public TMP_Text text;
    [BoxGroup("Settings")]
    public float duration;
    [BoxGroup("Events")]
    public UnityEvent onFadeEnd;

    private float fadeEndTime;
    private bool hasFadeEnded;

    private void Start()
    {
        fadeEndTime = -1;
    }

    private void Update()
    {
        if(fadeEndTime < 0 || hasFadeEnded)
            return;
        if(Time.unscaledTime >= fadeEndTime)
        {
            onFadeEnd.Invoke();
            fadeEndTime = -1;
            hasFadeEnded = true;
        }
    }    

    [ContextMenu("Fade")]
    public void FadeInText()
    {
        if(Time.unscaledTime < fadeEndTime)
            return;

        hasFadeEnded = false;
        text.ForceMeshUpdate();
        TMP_TextInfo textInfo = text.textInfo;
        int totalCharacters = textInfo.characterCount;

        float fadeStep = duration / totalCharacters;
        float overallStart = Time.unscaledTime;
        fadeEndTime = overallStart + duration;

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

        while(Time.unscaledTime < fadeStartTime + duration)
        {
            float progress = Mathf.Clamp01((Time.unscaledTime - fadeStartTime) / duration);
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
