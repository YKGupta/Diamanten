using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class SleepDoor : MonoBehaviour
{
    public SleepInfo[] mannequinsSleepInfo;
    public RecordPlayerManager recordPlayerManager;
    public Door door;

    public UnityEvent onAllSlept;

    [ReadOnly]
    public bool haveAllMannequinsSlept;

    private HashSet<SleepInfo> mannequinsThatSlept;

    private void Start()
    {
        haveAllMannequinsSlept = false;
        mannequinsThatSlept = new HashSet<SleepInfo>();
    }

    private void Update()
    {
        if(!recordPlayerManager.areRecordsPlayedCorrectly || !door.isOpen || door.isUnderProcessing || haveAllMannequinsSlept)
            return;
        bool checkIfAllMannequinsSlept = true;
        foreach(SleepInfo mannequinSleepInfo in mannequinsSleepInfo)
        {
            if(mannequinsThatSlept.Contains(mannequinSleepInfo))
                continue;
            checkIfAllMannequinsSlept = false;
            if(Vector3.Distance(mannequinSleepInfo.mannequin.transform.position, transform.position) <= Constants.instance.MANNEQUIN_SLEEP_DOOR_RANGE)
                MakeSleep(mannequinSleepInfo);
        }
        haveAllMannequinsSlept = checkIfAllMannequinsSlept;
        if(haveAllMannequinsSlept)
            onAllSlept.Invoke();
    }

    private void MakeSleep(SleepInfo mannequinSleepInfo)
    {
        MannequinMovement mm = mannequinSleepInfo.mannequin.GetComponent<MannequinMovement>();
        mannequinSleepInfo.mannequin.GetComponent<MannequinChase>().enabled = false;
        mannequinSleepInfo.mannequin.GetComponent<MannequinAttack>().enabled = false;
        mm.StopAgent();
        mm.useDefault = false;
        mm.MoveAgent(mannequinSleepInfo.sleepGO.transform.position, mm.walkSpeed);
        mannequinsThatSlept.Add(mannequinSleepInfo);
    }
}
