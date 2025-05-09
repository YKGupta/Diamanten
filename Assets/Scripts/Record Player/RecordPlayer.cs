using System;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class RecordPlayer : MonoBehaviour, IInteractionEffect
{
    public GameObject interactionUIGO;
    public KeyCode playKey;
    public int order;
    public UnityEvent onRecordPlay_UnityEvent;

    public event Action<RecordPlayer> onRecordPlay;

    [ReadOnly]
    public bool isPlaying;

    private void Start()
    {
        isPlaying = false;
    }

    private void Update()
    {
        if(!interactionUIGO.activeSelf || !Input.GetKeyDown(playKey) || isPlaying)
            return;
        
        isPlaying = true;
        onRecordPlay?.Invoke(this);
        onRecordPlay_UnityEvent.Invoke();
    }

    public void StartEffect()
    {
        interactionUIGO.SetActive(true);
    }

    public void EndEffect()
    {
        interactionUIGO.SetActive(false);
    }

    public bool isInteractable()
    {
        return !isPlaying && enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= Constants.instance.RANGE;
    }
}
