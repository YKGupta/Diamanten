using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class FindItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Find Item Specifics")]
    public bool showGizmos = false;
    [BoxGroup("Find Item Specifics")]
    public UnityEvent onItemCollect;

    public void OnItemCollected()
    {
        gameObject.SetActive(false);
        onItemCollect.Invoke();
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
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= Constants.instance.FIND_ITEM_RANGE;
    }

    private void OnDrawGizmosSelected()
    {
        if(!showGizmos)
            return;
            
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Constants.instance.FIND_ITEM_RANGE);
    }
}
