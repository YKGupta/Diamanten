using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Door : MonoBehaviour
{
    [Foldout("Settings")]
    public TriggerEvents trigger;
    [Foldout("Settings")]
    public UnityEvent startInstruction;
    [Foldout("Settings")]
    public UnityEvent endInstruction;

    [Foldout("Animation")]
    public Animator animator;
    [Foldout("Animation")]
    public string boolName;

    [ReadOnly]
    public bool isUnlocked;

    private bool isUnderProcessing;
    private bool isOpen;

    private void Start()
    {
        isUnlocked = false;
        isUnderProcessing = false;
        isOpen = false;
        trigger.triggerEntered += TriggerEntered;
        trigger.triggerExited += TriggerExited;
    }

    private void Update()
    {
        if(!isUnlocked || isUnderProcessing)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    public void TriggerEntered(Collider other, GameObject obj)
    {
        if(!isUnlocked)
            return;
        
        startInstruction.Invoke();
    }

    public void TriggerExited(Collider other, GameObject obj)
    {
        if(!isUnlocked)
            return;

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
        animator.SetBool(boolName, isOpen);
    }
}
