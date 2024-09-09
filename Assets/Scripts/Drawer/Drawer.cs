using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Drawer : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("Settings")]
    public GameObject interactionUIGO;
    [BoxGroup("Settings")]
    public float range = 1.5f;
    [BoxGroup("Settings")]
    public UnityEvent OnDrawerOpen;
    [BoxGroup("Settings")]
    public UnityEvent OnDrawerLockedHover;
    [BoxGroup("Settings")]
    public UnityEvent OnDrawerUnlockedHover;
    [BoxGroup("Settings")]
    public UnityEvent Reset;
    [BoxGroup("Settings")]
    [ReadOnly]
    public bool isUnlocked;

    private MouseEvents mouseEvents;

    private void Awake()
    {
        isUnlocked = false;
    }

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += OnDrawerClicked;
    }

    public void OnDrawerClicked(GameObject src)
    {
        if(!isUnlocked)
            return;
        OnDrawerOpen.Invoke();
        EndEffect();
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void StartEffect()
    {
        interactionUIGO.SetActive(true);
        if(isUnlocked)
            OnDrawerUnlockedHover.Invoke();
        else
            OnDrawerLockedHover.Invoke();
    }

    public void EndEffect()
    {
        interactionUIGO.SetActive(false);
        Reset.Invoke();
    }

    public bool isInteractable()
    {
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range;
    }
}
