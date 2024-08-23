using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public SoundType type;
    [Tooltip("Chooses a random clip to play")]
    public AudioClip[] clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Tooltip("Leave null for player sounds(source is player itself)")]
    public GameObject source;

    public AudioClip GetAudioClip()
    {
        if(clip.Length == 1)
            return clip[0];
        int randomIndex = UnityEngine.Random.Range(1, clip.Length);
        (clip[randomIndex], clip[0]) = (clip[0], clip[randomIndex]);
        return clip[0];
    }
}
