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

    private List<GameObject> objectivesGOList;

    private void Start()
    {
        SetAllObjectives();
    }

    public void SetNextObjective(int index)
    {
        for(int i = 0; i < objectivesGOList.Count; i++)
        {
            if(i < index)
                objectivesGOList[i].GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Strikethrough;
            else if(i == index)
                objectivesGOList[i].GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
            else
                objectivesGOList[i].GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Italic;
        }
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
