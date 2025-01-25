using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class ObjectivesUIController : MonoBehaviour
{
    [BoxGroup("Settings")]
    public Transform objectivesParent;
    [BoxGroup("Settings")]
    public GameObject objectivePrefab;
    [BoxGroup("Settings")]
    public ObjectivesManager objectivesManager;
    [BoxGroup("Settings")]
    public TMP_Text objectiveText;
    [BoxGroup("Settings")]
    public Animator objectiveTextAnimator;
    [BoxGroup("Settings (Styling)")]
    public TextStyle completedObjectiveStyle;
    [BoxGroup("Settings (Styling)")]
    public TextStyle currentObjectiveStyle;
    [BoxGroup("Settings (Styling)")]
    public TextStyle futureObjectiveStyle;

    private List<GameObject> objectivesGOList;
    private int currentInd;

    private void Start()
    {
        currentInd = 0;
        SetAllObjectives();
    }

    public void SetNextObjective(int index)
    {
        for(int i = 0; i < objectivesGOList.Count; i++)
        {
            if(i < index)
                SetStyle(objectivesGOList[i].GetComponentInChildren<TMP_Text>(), completedObjectiveStyle);
            else if(i == index)
                SetStyle(objectivesGOList[i].GetComponentInChildren<TMP_Text>(), currentObjectiveStyle);
            else
                SetStyle(objectivesGOList[i].GetComponentInChildren<TMP_Text>(), futureObjectiveStyle);
        }
        currentInd = index;
        objectiveTextAnimator.SetTrigger("animate");
    }

    public void UpdateText()
    {
        objectiveText.text = objectivesManager.objectives[currentInd].title;
    }

    private void SetStyle(TMP_Text text, TextStyle style)
    {
        text.fontStyle = style.fontStyle;
        text.fontSize = style.fontSize;
        text.color = style.color;
    }

    private void SetAllObjectives()
    {
        objectivesGOList = new List<GameObject>();
        foreach(Objective ob in objectivesManager.objectives)
        {
            GameObject obj = Instantiate(objectivePrefab, objectivesParent);
            TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
            text.text = ob.title;
            text.fontStyle = FontStyles.Normal;
            objectivesGOList.Add(obj);
        }
    }
}
