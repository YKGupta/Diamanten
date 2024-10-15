using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Door : MonoBehaviour
{
    [BoxGroup("Settings")]
    public TriggerEvents trigger;
    [BoxGroup("Settings")]
    public UnityEvent startInstruction;
    [BoxGroup("Settings")]
    public UnityEvent endInstruction;

    [BoxGroup("Animation")]
    public Animator animator;
    [BoxGroup("Animation")]
    public string boolName;

    [BoxGroup("Door Events")]
    public UnityEvent onDoorLockedEnter;
    [BoxGroup("Door Events")]
    public UnityEvent onDoorLockedExit;
    [BoxGroup("Door Events")]
    public UnityEvent onDoorOpen;
    [BoxGroup("Door Events")]
    public UnityEvent onDoorClose;

    [ReadOnly]
    public bool isUnlocked;
    [ReadOnly]
    public bool isOpen;
    [ReadOnly]
    public bool isUnderProcessing;

    private bool isTriggered;

    private void Start()
    {
        isUnlocked = false;
        isUnderProcessing = false;
        isOpen = false;
        isTriggered = false;
        trigger.triggerEntered += TriggerEntered;
        trigger.triggerExited += TriggerExited;
    }

    private void Update()
    {
        if(!isTriggered || !isUnlocked || isUnderProcessing)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    public void TriggerEntered(Collider other, GameObject obj)
    {
        if(!isUnlocked)
        {
            onDoorLockedEnter.Invoke();
            return;
        }

        isTriggered = true;

        startInstruction.Invoke();
    }

    public void TriggerExited(Collider other, GameObject obj)
    {
        if(!isUnlocked)
        {
            onDoorLockedExit.Invoke();
            return;
        }

        isTriggered = false;

        endInstruction.Invoke();
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void FinishProcessing()
    {
        isUnderProcessing = false;
    }

    private void ToggleDoor()
    {
        isUnderProcessing = true;
        isOpen = !isOpen;
        if(isOpen)
            onDoorOpen.Invoke();
        else
            onDoorClose.Invoke();
        SoundManager.PlaySound(isOpen ? SoundType.DoorOpen : SoundType.DoorClose);
        animator.SetBool(boolName, isOpen);
    }
}
