using System;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    public event Action<GameObject> onMouseEnter;
    public event Action<GameObject> onMouseOver;
    public event Action<GameObject> onMouseExit;
    public event Action<GameObject> onMouseDown;
    public event Action<GameObject> onMouseUp;

    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke(gameObject);
    }

    private void OnMouseOver()
    {
        onMouseOver?.Invoke(gameObject);
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke(gameObject);
    }

    private void OnMouseDown()
    {
        onMouseDown?.Invoke(gameObject);
    }

    private void OnMouseUp()
    {
        onMouseUp?.Invoke(gameObject);
    }
}
