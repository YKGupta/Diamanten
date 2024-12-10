using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class InteractiveObject : MonoBehaviour
{
    [BoxGroup("Settings")]
    public TriggerEvents trigger;
    [BoxGroup("Settings")]
    public KeyCode interactionKey;
    [BoxGroup("Settings (Events)")]
    public UnityEvent onTriggerEnter;
    [BoxGroup("Settings (Events)")]
    public UnityEvent onTriggerExit;
    [BoxGroup("Settings (Events)")]
    public UnityEvent onInteract;

    [ReadOnly]
    public bool isTriggered;

    private void Start()
    {
        isTriggered = false;
        trigger.triggerEntered += TriggerEntered;
        trigger.triggerExited += TriggerExited;
    }

    private void Update()
    {
        if(!isTriggered)
            return;

        if(Input.GetKeyDown(interactionKey))
            Interact();
    }

    private void Interact()
    {
        onInteract.Invoke();
    }

    public void TriggerEntered(Collider other, GameObject obj)
    {
        isTriggered = true;
        onTriggerEnter.Invoke();
    }

    public void TriggerExited(Collider other, GameObject obj)
    {
        isTriggered = false;
        onTriggerExit.Invoke();
    }
}
