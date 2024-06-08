using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NaughtyAttributes;

public class ImageZoom : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [BoxGroup("Settings")]
    public ScrollRect scrollRect;
    [BoxGroup("Instructions Specifics")]
    public GameObject dragInstructionsGO;
    [BoxGroup("Instructions Specifics")]
    public GameObject zoomedGO;

    private Rect rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>().rect;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        dragInstructionsGO.SetActive(false);
        zoomedGO.SetActive(true);
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 x = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition, data.enterEventCamera, out x);
        if(WithinBounds(x, rect))
        {
            scrollRect.normalizedPosition = NormalizedPosition(x, rect);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        zoomedGO.SetActive(false);
        dragInstructionsGO.SetActive(true);
    }

    private bool WithinBounds(Vector2 v, Rect r)
    {
        return !(v.x > r.xMax || v.x < r.xMin || v.y > r.yMax || v.y < r.yMin);
    }

    private Vector2 NormalizedPosition(Vector2 v, Rect r)
    {
        float xMax = r.xMax + Mathf.Abs(r.xMin);
        float yMax = r.yMax + Mathf.Abs(r.yMin);
        return new Vector2((v.x + Mathf.Abs(r.xMin)) / xMax, (v.y + Mathf.Abs(r.yMin)) / yMax);
    }
}
