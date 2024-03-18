using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DragAndDropManager : MonoBehaviour
{
    public Transform dragAndDropParent;

    [ReadOnly]
    public List<DragAndDropInfo> dragAndDropItems;

    private Dictionary<DragItem, DragAndDropInfo> lookup;

    private void Start()
    {
        InitDragAndDropList();

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
        foreach(DropItem dropItem in info.dropItems)
        {
            if(dropItem.id == dragItem.currentId)
                continue;
                
            EndEffect_DropItem(dropItem.gameObject);   // Manually ending to make sure there aren't any inconsistent
            dropItem.mouseEvents.onMouseOver -= StartEffect_DropItem;
            dropItem.mouseEvents.onMouseExit -= EndEffect_DropItem;
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

    [ContextMenu("Initialise Drag and Drop Info")]
    private void InitDragAndDropList()
    {
        dragAndDropItems = new List<DragAndDropInfo>();
        for(int i = 0; i < dragAndDropParent.childCount; i++)
        {
            DragAndDropInfo temp = new DragAndDropInfo();
            Transform dragAndDropItem = dragAndDropParent.GetChild(i);
            for(int j = 0; j < dragAndDropItem.childCount; j++)
            {
                DragItem dragItem = dragAndDropItem.GetChild(j).GetComponent<DragItem>();
                DropItem dropItem = dragAndDropItem.GetChild(j).GetComponent<DropItem>();
                if(dragItem != null)
                {
                    temp.dragItems.Add(dragItem);
                }
                else
                {
                    temp.dropItems.Add(dropItem);
                }
            }
            dragAndDropItems.Add(temp);
        }
    }
}