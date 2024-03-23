using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateObject : MonoBehaviour, IEventHandler
{
    public string triggerName;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleEvent()
    {
        animator.SetTrigger(triggerName);
    }
}
