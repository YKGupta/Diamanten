using System;
using System.Collections;
using UnityEngine;

public class DragItem : MonoBehaviour, IInteractionEffect
{
    public GameObject interactionUIGO;
    public LayerMask dropItemMask;
    public int correctId;
    public float range = 10f;

    [HideInInspector]
    public int currentId;

    public Action<DragItem> onItemDrag;
    public Action<DragItem> onItemDrop;

    private MouseEvents mouseEvents;
    private bool isBeingDragged;
    private Vector3 startPos, endPos;

    private void Start()
    {
        currentId = -1;
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += BeginDrag;
        mouseEvents.onMouseUp += EndDrag;
        isBeingDragged = false;
    }

    public void BeginDrag(GameObject obj)
    {
        if(!isInteractable())
            return;

        isBeingDragged = true;

        onItemDrag?.Invoke(this);

        interactionUIGO.transform.GetChild(0).gameObject.SetActive(false); // LMB Icon
        interactionUIGO.transform.GetChild(1).gameObject.SetActive(true); // Drag Icon
    }

    public void EndDrag(GameObject obj)
    {
        if(!isBeingDragged)
            return;
            
        isBeingDragged = false;

        onItemDrop?.Invoke(this);

        RaycastHit hitInfo;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, dropItemMask))
        {
            currentId = hitInfo.collider.GetComponent<DropItem>().id;

            if(currentId == correctId)
            {
                Debug.Log($"{name} was placed correctly!");
                enabled = false;
            }

            startPos = transform.position;
            endPos = hitInfo.collider.transform.position;
            Timer.instance.ClearAllTimers("drag");
            TimerInstance timer = Timer.instance.CreateTimer(1, 1, "drag");
            timer.timerStart += LerpPosition;
        }

        interactionUIGO.SetActive(false);
        interactionUIGO.transform.GetChild(0).gameObject.SetActive(true); // LMB Icon
        interactionUIGO.transform.GetChild(1).gameObject.SetActive(false); // Drag Icon
    }

    private IEnumerator LerpPosition(TimerInstance timer)
    {
        while(!Mathf.Approximately(timer.NormalizedValue(), 1f))
        {
            transform.position = Vector3.Lerp(startPos, endPos, timer.NormalizedValue());
            yield return null;
        }
        transform.position = endPos;
    }

    public void StartEffect()
    {
        if(!enabled)
        {
            interactionUIGO.SetActive(false);
            return;
        }

        interactionUIGO.SetActive(true);
    }

    public void EndEffect()
    {
        if(!enabled)
        {
            interactionUIGO.SetActive(false);
            return;
        }

        if(!isBeingDragged)
            interactionUIGO.SetActive(false);
    }

    private bool isInteractable()
    {
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range;
    }
}
