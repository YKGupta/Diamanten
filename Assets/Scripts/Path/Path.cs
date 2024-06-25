using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Path : MonoBehaviour
{
    [ReadOnly]
    public List<Transform> waypoints;

    [BoxGroup("Fancy")]
    [HorizontalLine(2f, EColor.Red)]
    public Color gizmoColor;
    [BoxGroup("Fancy")]
    public Color initPointColor = Color.red;

    [HorizontalLine(2f, EColor.Red)]
    [BoxGroup("Readonly")]
    [ReadOnly]
    public int size;
    [BoxGroup("Readonly")]
    [ReadOnly]
    public int nextWayPointIndex;

    private void Start()
    {
        SetWayPoints();
    }

    [ContextMenu("Set Way Points")]
    private void SetWayPoints()
    {
        waypoints = new List<Transform>();
        for(int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i));
        }
        size = waypoints.Count;
    }

    public Transform GetNextWayPoint()
    {
        Transform temp = waypoints[nextWayPointIndex];
        nextWayPointIndex = (nextWayPointIndex + 1 ) % size;
        return temp;
    }

    public Transform MoveToClosestWayPoint(Vector3 position)
    {
        float min = float.MaxValue;
        Transform ans = null;
        foreach(Transform t in waypoints)
        {
            float x = Vector3.Distance(t.position, position);
            if(x < min)
            {
                min = x;
                ans = t;
            }
        }
        nextWayPointIndex = (waypoints.IndexOf(ans) + 1) % size;
        return ans;
    }

    public float GetDistanceFromWaypoint(Vector3 position)
    {
        int i = nextWayPointIndex == 0 ? size-1 : nextWayPointIndex - 1;
        return Vector3.Distance(position, waypoints[i].position);
    }

    private void OnDrawGizmos()
    {
        Transform prev = null;
        foreach(Transform t in waypoints)
        {
            if(prev != null)
                Gizmos.color = gizmoColor;
            else
                Gizmos.color = initPointColor;
            Gizmos.DrawSphere(t.position, .1f);
            Gizmos.color = Color.gray;
            if(prev != null)
            {
                Gizmos.DrawLine(prev.position, t.position);
            }
            prev = t;
        }

        if(size <= 0 || waypoints[0] == prev)
            return;

        Gizmos.DrawLine(prev.position, waypoints[0].position);
    }
}
