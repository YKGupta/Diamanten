using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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
    [BoxGroup("Settings")]
    public PlayerLook playerLook;
    [BoxGroup("Settings")]
    public float playerRotationAmount;
    [BoxGroup("UI")]
    public GameObject interactionUIGO;
    [BoxGroup("Helpers")]
    public bool showGizmos = false;

    public UnityEvent onLampCollect;

    private MouseEvents mouseEvents;
    private Quaternion playerBodyInitialRot;
    private Quaternion playerBodyFinalRot;

    private void Start()
    {
        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += OnCollect;
    }

    public void OnCollect(GameObject obj)
    {
        if(!isInteractable())
            return;

        onLampCollect.Invoke();
        enabled = false;
        PlayerManager.instance.isPlayerAllowedToMove = false;
        PlayerManager.instance.isPlayerAllowedToCrouch = false;
        PlayerManager.instance.isPlayerAllowedToLook = false;
        flashlight.useLight = true;
        playerBodyInitialRot = playerLook.playerBody.rotation;
        playerBodyFinalRot = Quaternion.Euler(playerLook.playerBody.eulerAngles + Vector3.up * playerRotationAmount);
        TimerInstance timer = Timer.instance.CreateTimer(moveHandToLampDuration, 1, "hand");
        timer.timerStart += MoveHand;
    }

    public IEnumerator MoveHand(TimerInstance timer)
    {
        while(!Mathf.Approximately(timer.NormalizedValue(), 1f))
        {
            handIK.weight = timer.NormalizedValue();
            playerLook.playerBody.rotation = Quaternion.Lerp(playerBodyInitialRot, playerBodyFinalRot, timer.NormalizedValue());
            yield return null;
        }
        
        handIK.weight = 1f;
        playerLook.playerBody.rotation = playerBodyFinalRot;
        fingersAnimator.SetTrigger("hold");
        transform.SetParent(handIK.transform);
        PlayerManager.instance.isPlayerAllowedToMove = true;
        PlayerManager.instance.isPlayerAllowedToCrouch = true;
        PlayerManager.instance.isPlayerAllowedToLook = true;
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
        return enabled && Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= Constants.instance.LAMP_COLLECT_RANGE;
    }

    private void OnDrawGizmos()
    {
        if(!showGizmos)
            return;
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Constants.instance.LAMP_COLLECT_RANGE);
    }
}
