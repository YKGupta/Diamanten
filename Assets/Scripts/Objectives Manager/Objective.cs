using UnityEngine;

[System.Serializable]
public class Objective
{
    [Tooltip("The text to be displayed")]
    public string title;

    private bool state;

    public void MarkAsCompleted()
    {
        state = true;
    }

    public bool IsCompleted()
    {
        return state;
    }
}
