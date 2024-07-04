using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public event Action<Collider, GameObject> triggerEntered;
    public UnityEvent enterUnityEvent;
    public event Action<Collider, GameObject> triggerStayed;
    public UnityEvent stayUnityEvent;
    public event Action<Collider, GameObject> triggerExited;
    public UnityEvent exitUnityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(!enabled)
            return;
        
        triggerEntered?.Invoke(other, gameObject);
        enterUnityEvent.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if(!enabled)
            return;
        
        triggerStayed?.Invoke(other, gameObject);
        stayUnityEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(!enabled)
            return;
        
        triggerExited?.Invoke(other, gameObject);
        exitUnityEvent.Invoke();
    }
}
