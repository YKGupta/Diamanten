using System;
using UnityEngine;
using NaughtyAttributes;

public class MannequinChase : MonoBehaviour, ITargetStateHandler
{
    public PlayerSafe playerSafe;
    public MannequinMovement movement;
    public Flashlight playerFlashlight;
    [Tooltip("By what factors must the timeToInterval be reduced every frame the light is used by the player.")]
    public float reductionMultiplier = 3f;
    [Tooltip("At what distance from player, should the mannequin start chasing if not already due to timeToInterval")]
    public float chaseRange = 3f;
    [Range(0f, 100f)]
    public float minInterval = 50f;
    [Range(0f, 600f)]
    public float maxInterval = 500f;

    public bool showGizmos;

    [ReadOnly]
    public float timeToInterval, currentInterval;
    [ReadOnly]
    public bool isChasing;

    public Action<MannequinChase> onNewInterval;

    private void Start()
    {
        currentInterval = timeToInterval = GetRandomInterval();
        isChasing = false;
    }

    private void Update()
    {
        Chase();
    }

    private void Chase()
    {
        if(timeToInterval <= 0f || isPlayerWithinRange())
        {
            if(!isChasing)
            {
                // Means it's the first frame after starting the chase
                movement.useDefault = false;
                movement.StopAgent();
            }
            
            if(playerSafe.isSafe)
            {
                SetNextInterval();
            }
            else
            {
                movement.MoveAgent(PlayerInfo.instance.GetPosition(), movement.runSpeed);
                isChasing = true;
            }
        }

        if(!isChasing)
            timeToInterval -= Time.deltaTime * (playerFlashlight.light.gameObject.activeSelf ? reductionMultiplier : 1);
    }

    public void SetNextInterval()
    {
        timeToInterval = GetRandomInterval();
        currentInterval = timeToInterval;
        if(onNewInterval != null)
            onNewInterval.Invoke(this);
        isChasing = false;
        movement.useDefault = true;
        movement.StopAgent();
        Transform t = movement.defaultPath.MoveToClosestWayPoint(transform.position);
        movement.MoveAgent(t.position, movement.walkSpeed);
    }

    private float GetRandomInterval()
    {
        return UnityEngine.Random.Range(minInterval, maxInterval);
    }

    private bool isPlayerWithinRange()
    {
        return Vector3.Distance(transform.position, PlayerInfo.instance.center.position) <= chaseRange;
    }

    private void OnDrawGizmosSelected()
    {
        if(!showGizmos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public TargetState GetState()
    {
        return isChasing ? TargetState.Engaged : TargetState.Idle;
    }
}
