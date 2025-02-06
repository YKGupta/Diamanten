using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool hasDied = false;

    private void Start()
    {
        hasDied = false;
    }

    public void TakeDamage()
    {
        if(hasDied)
            return;
        hasDied = true;
        SoundManager.PlaySound(SoundType.Death);
    }
}
