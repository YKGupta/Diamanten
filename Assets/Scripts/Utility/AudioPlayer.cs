using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public void PlayClick()
    {
        SoundManager.PlaySound(SoundType.Click);
    }
}
