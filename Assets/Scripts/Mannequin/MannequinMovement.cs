using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class MannequinMovement : MonoBehaviour
{
    public Path defaultPath;
    public NavMeshAgent agent;
    [Range(0f, 10f)]
    public float walkSpeed;
    [Range(0f, 10f)]
    public float runSpeed;
    public Animator gfxAnimator;
    public float animationSmoothTime = 0.2f;

    [ReadOnly]
    public bool useDefault = true;

    [ReadOnly]
    public float currentSpeed;

    private void Start()
    {
        useDefault = true;
        MoveAgent(defaultPath.GetNextWayPoint().position, walkSpeed);
    }

    private void Update()
    {
        if(!useDefault)
            return;
        Debug.Log(defaultPath.GetDistanceFromWaypoint(transform.position));
        if(defaultPath.GetDistanceFromWaypoint(transform.position) <= agent.stoppingDistance)
        {
            MoveAgent(defaultPath.GetNextWayPoint().position, walkSpeed);
        }
    }

    public void MoveAgent(Vector3 position, float speed)
    {
        currentSpeed = speed;
        agent.speed = currentSpeed;
        agent.SetDestination(position);
        float speedPercent = currentSpeed == runSpeed ? 1f : 0.5f;
        gfxAnimator.SetFloat("speedPercent", speedPercent);
    }

    public void StopAgent()
    {
        currentSpeed = 0;
        float speedPercent = currentSpeed == runSpeed ? 1f : 0.5f;
        gfxAnimator.SetFloat("speedPercent", speedPercent);
        agent.ResetPath();
    }
}
