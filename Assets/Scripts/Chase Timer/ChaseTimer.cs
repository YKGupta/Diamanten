using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NaughtyAttributes;

public class ChaseTimer : MonoBehaviour
{
    [BoxGroup("Settings")]
    public Transform mannequinsParent;
    [BoxGroup("Settings")]
    public PlayerSafe playerSafe;
    [BoxGroup("Settings (UI)")]
    public GameObject chaseTimerGO;
    [BoxGroup("Settings (UI)")]
    public Image timerImage;
    [BoxGroup("Settings (Events)")]
    public UnityEvent firstTimeOn;
    [BoxGroup("Settings (Events)")]
    public UnityEvent firstTimeOff;

    [ReadOnly]
    public MannequinChase currentMannequin;

    private bool wasActive;
    private bool firstTimeOver;

    private void Start()
    {
        chaseTimerGO.SetActive(false);
        wasActive = false;
        firstTimeOver = false;
        for(int i = 0; i < mannequinsParent.childCount; i++)
        {
            mannequinsParent.GetChild(i).GetComponent<MannequinChase>().onNewInterval += OnNewInterval;
            currentMannequin = mannequinsParent.GetChild(i).GetComponent<MannequinChase>();
        }
    }

    private void Update()
    {
        bool anyMannequinActive = false;
        for(int i = 0; i < mannequinsParent.childCount; i++)
            anyMannequinActive |= mannequinsParent.GetChild(i).gameObject.activeSelf;
        if(!anyMannequinActive)
            return;
        chaseTimerGO.SetActive(playerSafe.isSafe);
        timerImage.fillAmount = currentMannequin.timeToInterval / currentMannequin.currentInterval;
        if(!firstTimeOver && wasActive != chaseTimerGO.activeSelf)
        {
            if(chaseTimerGO.activeSelf)
                firstTimeOn.Invoke();
            else
            {
                firstTimeOff.Invoke();
                firstTimeOver = true;
            }
        }
        wasActive = chaseTimerGO.activeSelf;
    }

    public void OnNewInterval(MannequinChase mannequin)
    {
        if(currentMannequin != null && currentMannequin.currentInterval <= mannequin.currentInterval)
            return;
        currentMannequin = mannequin;
    }
}
