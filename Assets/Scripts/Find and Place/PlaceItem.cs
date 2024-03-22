using NaughtyAttributes;
using UnityEngine;

public class PlaceItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Place Item Specifics")]
    [Range(0f, 50f)]
    public float range = 10f;

    public void OnItemReleased()
    {
        Debug.Log("Item Released!");
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
