using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DragAndDropManager : MonoBehaviour
{
    public List<DragAndDropInfo> dragAndDropItems;

    private Dictionary<DragItem, DragAndDropInfo> lookup;

    private void Start()
    {
        InitEventHandlers();
        
        lookup = new Dictionary<DragItem, DragAndDropInfo>();

        foreach(DragAndDropInfo i in dragAndDropItems)
        {
            foreach(DragItem dragItem in i.dragItems)
            {
                dragItem.onItemDrag += OnItemDrag;
                dragItem.onItemDrop += OnItemDrop;
                lookup.Add(dragItem, i);
            }
        }
    }

    public void OnItemDrag(DragItem dragItem)
    {
        DragAndDropInfo info = lookup[dragItem];
        foreach(DropItem dropItem in info.dropItems)
        {
            if(dropItem.id == dragItem.currentId)
                continue;
                
            dropItem.mouseEvents.onMouseOver += StartEffect_DropItem;
            dropItem.mouseEvents.onMouseExit += EndEffect_DropItem;
        }
    }

    public void OnItemDrop(DragItem dragItem)
    {
        DragAndDropInfo info = lookup[dragItem];
        bool allItemsDroppedCorrectly = true;
        foreach(DropItem dropItem in info.dropItems)
        {
            if(dropItem.id == dragItem.currentId)
                continue;

            EndEffect_DropItem(dropItem.gameObject);   // Manually ending to make sure there aren't any inconsistent
            dropItem.mouseEvents.onMouseOver -= StartEffect_DropItem;
            dropItem.mouseEvents.onMouseExit -= EndEffect_DropItem;
        }

        foreach(DragItem i in info.dragItems)
        {
            if(i.currentId != i.correctId)
            {
                allItemsDroppedCorrectly = false;
                break;
            }
        }

        if(allItemsDroppedCorrectly)
        {
            info.eventHandler.HandleEvent();
        }
    }

    public void StartEffect_DropItem(GameObject obj)
    {
        DropItem dropItem = obj.GetComponent<DropItem>();
        dropItem.interactionUIGO.SetActive(true);
    }

    public void EndEffect_DropItem(GameObject obj)
    {
        DropItem dropItem = obj.GetComponent<DropItem>();
        dropItem.interactionUIGO.SetActive(false);
    }

    private void InitEventHandlers()
    {
        foreach(DragAndDropInfo i in dragAndDropItems)
        {
            i.eventHandler = i.eventHandlerGO.GetComponent<IEventHandler>();
        }
    }
}