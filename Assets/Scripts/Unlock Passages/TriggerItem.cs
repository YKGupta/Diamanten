using NaughtyAttributes;
using UnityEngine;

public class TriggerItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Trigger Item Specifics")]
    [Range(0f, 50f)]
    public float range = 10f;

    private MouseEvents mouseEvents;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += Trigger;
    }

    public void Trigger(GameObject obj)
    {
        if(!isInteractable())
            return;
            
        Debug.Log("Triggerring some event");
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
