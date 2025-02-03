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
        if(currentObjectiveIndex > 0)
            SoundManager.PlaySound(SoundType.ObjectiveComplete);
        currentObjectiveIndex++;
        onNextObjective.Invoke(currentObjectiveIndex);
    }
}
