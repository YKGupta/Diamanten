using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage()
    {
        Debug.Log("Player Died!");
        SoundManager.PlaySound(SoundType.Death);
    }
}
