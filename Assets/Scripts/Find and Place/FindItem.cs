using UnityEngine;
using NaughtyAttributes;

public class FindItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Find Item Specifics")]
    [Range(0f, 50f)]
    public float range = 10f;
    public bool showGizmos = false;

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

    public bool isInteractable()
    {
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range;
    }

    private void OnDrawGizmosSelected()
    {
        if(!showGizmos)
            return;
            
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
