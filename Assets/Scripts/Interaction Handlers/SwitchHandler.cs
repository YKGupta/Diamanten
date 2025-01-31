using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class SwitchHandler : MonoBehaviour
{
    [BoxGroup("Settings")]
    public UnityEvent onStateTrue;
    [BoxGroup("Settings")]
    public UnityEvent onStateFalse;
    [BoxGroup("Settings")]
    [ReadOnly]
    public bool state;

    private void Start()
    {
        state = false;
    }

    public void Interact()
    {
        state = !state;
        SoundManager.PlaySound(SoundType.SwitchToggle);
        if(state)
            onStateTrue.Invoke();
        else
            onStateFalse.Invoke();
    }
}
