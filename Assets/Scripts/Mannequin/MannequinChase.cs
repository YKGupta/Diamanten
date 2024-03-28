using UnityEngine;
using NaughtyAttributes;

public class MannequinChase : MonoBehaviour
{
    public PlayerSafe playerSafe;
    public MannequinMovement movement;
    [Range(0f, 100f)]
    public float minInterval = 50f;
    [Range(0f, 600f)]
    public float maxInterval = 500f;

    [ReadOnly]
    public float timeToInterval;
    [ReadOnly]
    public bool isChasing;

    private void Start()
    {
        timeToInterval = GetRandomInterval();
        isChasing = false;
    }

    private void Update()
    {
        Chase();
    }

    private void Chase()
    {
        if(timeToInterval <= 0f)
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
            timeToInterval -= Time.deltaTime;
    }

    public void SetNextInterval()
    {
        timeToInterval = GetRandomInterval();
        isChasing = false;
        movement.useDefault = true;
        movement.StopAgent();
        Transform t = movement.defaultPath.MoveToClosestWayPoint(transform.position);
        movement.MoveAgent(t.position, movement.walkSpeed);
    }

    private float GetRandomInterval()
    {
        return Random.Range(minInterval, maxInterval);
    }
}
