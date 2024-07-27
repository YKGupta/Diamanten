using UnityEngine;

public static class SoundManager
{
    private static GameObject oneShotSoundGO;

    public static void PlaySound(SoundType soundType)
    {
        if(oneShotSoundGO == null)
        {
            oneShotSoundGO = new GameObject("Sound Source");
            oneShotSoundGO.AddComponent<AudioSource>();
        }

        AudioSource source = oneShotSoundGO.GetComponent<AudioSource>();
        Sound sound = AssetManager.instance.GetSound(soundType);
        source.volume = sound.volume;
        source.PlayOneShot(sound.GetAudioClip());
    }
}
