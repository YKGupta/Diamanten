using System;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class SurgicalToolClickHandler : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("Settings")]
    public int id;
    [BoxGroup("Settings")]
    public UnityEvent OnAbsent;
    [BoxGroup("Settings")]
    public UnityEvent OnPresent;
    [BoxGroup("Settings")]
    public GameObject interactionUI;
    [BoxGroup("Settings")]
    public float range;
    [BoxGroup("Settings")]
    public Texture emissionMap;
    [BoxGroup("Settings")]
    [ReadOnly]
    public bool isPlaced;

    private MouseEvents mouseEvents;
    
    public event Action<SurgicalToolClickHandler> onToolPlaced;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += ClickHandler;
    }

    public void ClickHandler(GameObject src)
    {
        if(!isInteractable())
            return;

        Item item = InventoryManager.instance.FindItem(id);
        if(item == null)
        {
            OnAbsent.Invoke();
            return;
        }
        OnPresent.Invoke();
    }

    public void Use()
    {
        if(!isInteractable())
            return;

        Item item = InventoryManager.instance.FindItem(id);
        if(item == null)
        {
            Debug.LogError($"No item [{item.id}, {item.name}] exists!");
            return;
        }
        EndEffect();
        InventoryManager.instance.RemoveItem(item, true, false);
        isPlaced = true;
        onToolPlaced?.Invoke(this);
    }

    public void StartEffect()
    {
        interactionUI.SetActive(true);
        GetComponentInParent<SurgicalChartEffects>().Show(emissionMap, isPresent());
    }

    public void EndEffect()
    {
        interactionUI.SetActive(false);
        GetComponentInParent<SurgicalChartEffects>().Reset();
    }

    public bool isInteractable()
    {
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range;
    }

    private bool isPresent()
    {
        Item item = InventoryManager.instance.FindItem(id);
        return item != null;
    }
}
