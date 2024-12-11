using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AssetManager : MonoBehaviour
{
    [Tooltip("List of all the sounds to be used by the game")]
    public Sound[] sounds;
    [Tooltip("Audio Mixer to be used by the sound manager")]
    public AudioMixerGroup audioMixer;

    [HideInInspector]
    public Dictionary<SoundType, Sound> map;
    public static AssetManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        map = new Dictionary<SoundType, Sound>();
        MapSounds();
    }

    private void MapSounds()
    {
        foreach(Sound s in sounds)
            map.Add(s.type, s);
    }

    public Sound GetSound(SoundType type)
    {
        return map[type];
    }
}
