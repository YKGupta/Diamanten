using System;
using UnityEngine;
using NaughtyAttributes;

public class PlayerSafe : MonoBehaviour
{
    public Transform safeZonesParent;
    public bool isSafe;

    private void Start()
    {
        isSafe = false;
        
        for(int i = 0; i < safeZonesParent.childCount; i++)
        {
            TriggerEvents t = safeZonesParent.GetChild(i).GetComponent<TriggerEvents>();
            t.triggerStayed += OnStaySafeZone;
            t.triggerExited += OnExitSafeZone;
        }
    }

    public void OnStaySafeZone(Collider other, GameObject zone)
    {
        if(!other.CompareTag("Player"))
            return;
        
        isSafe = true;
    }

    public void OnExitSafeZone(Collider other, GameObject zone)
    {
        if(!other.CompareTag("Player"))
            return;
        
        isSafe = false;
    }
}
