using System;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public event Action<Collider, GameObject> triggerEntered;
    public event Action<Collider, GameObject> triggerStayed;
    public event Action<Collider, GameObject> triggerExited;

    private void OnTriggerEnter(Collider other)
    {
        if(!enabled)
            return;
        
        triggerEntered?.Invoke(other, gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(!enabled)
            return;
        
        triggerStayed?.Invoke(other, gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!enabled)
            return;
        
        triggerExited?.Invoke(other, gameObject);
    }
}
