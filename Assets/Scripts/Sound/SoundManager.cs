using UnityEngine;

public static class SoundManager
{
    private static GameObject oneShotSoundGO;

    public static void PlaySound(SoundType soundType)
    {
        Sound sound = AssetManager.instance.GetSound(soundType);
        if(sound.source == null)
            PlayOneShot(sound);
        else
            PlayOnGameObject(sound);
    }

    private static void PlayOneShot(Sound sound)
    {
        if(oneShotSoundGO == null)
        {
            oneShotSoundGO = new GameObject("Sound Source");
            oneShotSoundGO.AddComponent<AudioSource>();
        }
        AudioSource source = oneShotSoundGO.GetComponent<AudioSource>();
        source.volume = sound.volume;
        source.PlayOneShot(sound.GetAudioClip());
    }

    private static void PlayOnGameObject(Sound sound)
    {
        AudioSource source = null;
        if(!sound.source.TryGetComponent<AudioSource>(out source))
        {
            sound.source.AddComponent<AudioSource>();
            source = sound.source.GetComponent<AudioSource>();
        }
        source.volume = sound.volume;
        source.PlayOneShot(sound.GetAudioClip());
    }
}
