using System.Collections.Generic;
using UnityEngine;

public class FindAndPlaceManager : MonoBehaviour
{
    public Transform findItemsParent;
    public Transform placeItemsParent;

    private Dictionary<GameObject, FindItemInfo> items;
    private FindItemInfo currentItem;

    private void Start()
    {
        items = new Dictionary<GameObject, FindItemInfo>();

        currentItem = null;

        for(int i = 0; i < findItemsParent.childCount; i++)
        {
            MouseEvents mouseEvents = findItemsParent.GetChild(i).GetComponent<MouseEvents>();
            FindItem findItem = findItemsParent.GetChild(i).GetComponent<FindItem>();
            PlaceItem placeItem = placeItemsParent.GetChild(i).GetComponent<PlaceItem>();
            MouseEvents mouseEventsP = placeItemsParent.GetChild(i).GetComponent<MouseEvents>();
            items.Add(findItemsParent.GetChild(i).gameObject, new FindItemInfo(findItem, mouseEvents, placeItem));
            mouseEvents.onMouseDown += SetFindItem;
            mouseEventsP.onMouseDown += ReleaseItem;
        }
    }

    public void SetFindItem(GameObject obj)
    {
        if(currentItem != null || !items[obj].findItem.isInteractable())
            return;
        
        currentItem = items[obj];
        currentItem.findItem.OnItemCollected();
        SoundManager.PlaySound(SoundType.RecordCollect);
    }

    public void ReleaseItem(GameObject obj)
    {
        PlaceItem placeItem = obj.GetComponent<PlaceItem>();

        if(currentItem == null || currentItem.placeItem != placeItem)
            return;
        
        currentItem = null;
        placeItem.OnItemReleased();
        SoundManager.PlaySound(SoundType.RecordPlace);
    }
}