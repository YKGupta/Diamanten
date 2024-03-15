using UnityEngine;

public class PlaceItem : MonoBehaviour, IInteractionEffect
{
    public GameObject interactionUIGO;

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
}
