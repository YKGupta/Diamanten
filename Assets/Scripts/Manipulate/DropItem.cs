using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject interactionUIGO;
    [HideInInspector]
    public MouseEvents mouseEvents; // Event subscription is handled by DragAndDropManager
    public int id;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
    }
}
