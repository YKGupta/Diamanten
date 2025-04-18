using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlaceItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Place Item Specifics")]
    public UnityEvent onItemPlaced;

    public void OnItemReleased()
    {
        EndEffect();
        onItemPlaced.Invoke();
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
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= Constants.instance.RANGE;
    }
}
