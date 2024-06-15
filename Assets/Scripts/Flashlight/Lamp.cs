using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Animations.Rigging;

public class Lamp : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("Settings")]
    public Flashlight flashlight;
    [BoxGroup("Settings")]
    public float moveHandToLampDuration = 1f;
    [BoxGroup("Settings")]
    public TwoBoneIKConstraint handIK;
    [BoxGroup("Settings")]
    public Animator fingersAnimator;
    [BoxGroup("Interaction Settings")]
    [Range(0f, 10f)]
    public float range = 2f;
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Helpers")]
    public bool showGizmos = false;

    private MouseEvents mouseEvents;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += OnCollect;
    }

    public void OnCollect(GameObject obj)
    {
        if(!enabled)
            return;

        enabled = false;
        PlayerManager.instance.isPlayerAllowedToMove = false;
        PlayerManager.instance.isPlayerAllowedToCrouch = false;
        flashlight.useLight = true;
        TimerInstance timer = Timer.instance.CreateTimer(moveHandToLampDuration, 1, "hand");
        timer.timerStart += MoveHand;
    }

    public IEnumerator MoveHand(TimerInstance timer)
    {
        while(!Mathf.Approximately(timer.NormalizedValue(), 1f))
        {
            handIK.weight = timer.NormalizedValue();
            yield return null;
        }
        
        handIK.weight = 1f;
        fingersAnimator.SetTrigger("hold");
        transform.SetParent(handIK.transform);
        PlayerManager.instance.isPlayerAllowedToMove = true;
        PlayerManager.instance.isPlayerAllowedToCrouch = true;
    }
    public void StartEffect()
    {
        interactionUIGO.SetActive(true);
    }

    public void EndEffect()
    {
        interactionUIGO.SetActive(false);
    }

    public bool isInteractable()
    {
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range;
    }

    private void OnDrawGizmos()
    {
        if(!showGizmos)
            return;
        
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, range);
    }
}
