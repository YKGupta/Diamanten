using UnityEngine;

public class FindItem : MonoBehaviour, IInteractionEffect
{
    public GameObject interactionUIGO;

    public void OnItemCollected()
    {
        gameObject.SetActive(false);
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
