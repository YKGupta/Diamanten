using UnityEngine;
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

    [ReadOnly]
    public MannequinChase currentMannequin;

    private void Start()
    {
        chaseTimerGO.SetActive(false);
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
    }

    public void OnNewInterval(MannequinChase mannequin)
    {
        if(currentMannequin != null && currentMannequin.currentInterval <= mannequin.currentInterval)
            return;
        currentMannequin = mannequin;
    }
}
