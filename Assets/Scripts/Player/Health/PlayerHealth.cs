using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [BoxGroup("Events")]
    public UnityEvent onGameLost;
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
        Cursor.lockState = CursorLockMode.None;
        onGameLost.Invoke();
    }
}
