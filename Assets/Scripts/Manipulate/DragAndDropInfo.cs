using System.Collections.Generic;
using NaughtyAttributes;

[System.Serializable]
public class DragAndDropInfo
{
    public List<DragItem> dragItems;
    public List<DropItem> dropItems;

    public UnityEngine.GameObject eventHandlerGO;

    public IEventHandler eventHandler;

    public DragAndDropInfo()
    {
        dragItems = new List<DragItem>();
        dropItems = new List<DropItem>();
        eventHandler = null;
    }
}