using System;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class RecordPlayerManager : MonoBehaviour
{
    public RecordPlayer[] recordPlayers;
    public int[] correctOrder;
    public UnityEvent onRecordsPlayCorrectly;

    [ReadOnly]
    public int[] currentOrder;
    [ReadOnly]
    public bool areRecordsPlayedCorrectly;

    private int currentRecordPlayInd;

    private void Start()
    {
        currentOrder = new int[correctOrder.Length];
        Array.Fill(currentOrder, -1);
        currentRecordPlayInd = 0;
        
        foreach(RecordPlayer rp in recordPlayers)
        {
            rp.onRecordPlay += OnRecordPlay;
        }
    }

    public void OnRecordPlay(RecordPlayer recordPlayer)
    {
        currentOrder[currentRecordPlayInd++] = recordPlayer.order;
        CheckRecordOrder();
    }

    private void CheckRecordOrder()
    {
        bool f = true;
        for(int i = 0; i < correctOrder.Length; i++)
        {
            if(correctOrder[i] != currentOrder[i])
            {
                f = false;
                break;
            }
        }
        areRecordsPlayedCorrectly = f;
        if(areRecordsPlayedCorrectly)
            onRecordsPlayCorrectly.Invoke();
    }
}
