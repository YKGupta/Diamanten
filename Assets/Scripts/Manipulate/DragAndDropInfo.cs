using System.Collections.Generic;

[System.Serializable]
public class DragAndDropInfo
{
    public List<DragItem> dragItems;
    public List<DropItem> dropItems;

    public DragAndDropInfo()
    {
        dragItems = new List<DragItem>();
        dropItems = new List<DropItem>();
    }
}