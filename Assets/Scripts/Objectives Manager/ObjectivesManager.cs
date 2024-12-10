using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class ObjectivesManager : MonoBehaviour
{
    [BoxGroup("Settings")]
    public Objective[] objectives;
    [BoxGroup("Settings")]
    public UnityEvent<int> onNextObjective;

    private int currentObjectiveIndex;

    private void Start()
    {
        currentObjectiveIndex = -1;
        SetNextObjective();
    }

    public void SetNextObjective()
    {
        currentObjectiveIndex++;
        onNextObjective.Invoke(currentObjectiveIndex);
    }
}
