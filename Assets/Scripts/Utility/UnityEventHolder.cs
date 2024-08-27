using UnityEngine;
using UnityEngine.Events;

public class UnityEventHolder : MonoBehaviour, IEventHandler
{
    public UnityEvent unityEvent;

    public void HandleEvent()
    {
        unityEvent.Invoke();
    }
}
