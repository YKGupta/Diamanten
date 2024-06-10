using UnityEngine;
using NaughtyAttributes;

public class Lamp : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("Settings")]
    public Flashlight flashlight;
    [BoxGroup("Interaction Settings")]
    [Range(0f, 10f)]
    public float range = 2f;
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Helpers")]
    public bool showGizmos = false;

    private MouseEvents mouseEvents;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += OnCollect;
    }

    public void OnCollect(GameObject obj)
    {
        flashlight.useLight = true;
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

    private void OnDrawGizmos()
    {
        if(!showGizmos)
            return;
        
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, range);
    }
}
