using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class SurgicalChartHandler : MonoBehaviour
{
    [BoxGroup("Settings")]
    public UnityEvent OnAllToolsPlaced;
    [BoxGroup("Settings")]
    [ReadOnly]
    public List<SurgicalToolClickHandler> tools;

    private void Start()
    {
        tools = new List<SurgicalToolClickHandler>();
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<SurgicalToolClickHandler>(out SurgicalToolClickHandler temp))
            {
                tools.Add(temp);
                temp.onToolPlaced += OnToolPlaced;
            }
        }
    }

    public void OnToolPlaced(SurgicalToolClickHandler src)
    {
        if(AllPlaced())
        {
            OnAllToolsPlaced.Invoke();
            Unsubscribe();
        }
    }

    private void Unsubscribe()
    {
        foreach(SurgicalToolClickHandler i in tools)
            i.onToolPlaced -= OnToolPlaced;
    }

    private bool AllPlaced()
    {
        foreach(SurgicalToolClickHandler i in tools)
        {
            if(!i.isPlaced)
                return false;
        }
        return true;
    }
}
