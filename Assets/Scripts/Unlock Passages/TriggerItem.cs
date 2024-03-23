using NaughtyAttributes;
using UnityEngine;

public class TriggerItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Trigger Item Specifics")]
    [Range(0f, 50f)]
    public float range = 10f;
    [BoxGroup("Trigger Item Specifics")]
    public GameObject eventHandlerGO;

    private MouseEvents mouseEvents;
    private IEventHandler eventHandler;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        eventHandler = eventHandlerGO.GetComponent<IEventHandler>();
        mouseEvents.onMouseDown += Trigger;
    }

    public void Trigger(GameObject obj)
    {
        if(!isInteractable())
            return;
            
        eventHandler.HandleEvent();
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
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range;
    }
}
