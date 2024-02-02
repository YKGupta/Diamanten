using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private List<TimerInstance> timers;

    public static Timer instance;
    
    private void Awake()
    {
        instance = this;
        timers = new List<TimerInstance>();
    }

    public TimerInstance CreateTimer(float duration, int clockSpeed = 1, string id = "all")
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(transform);
        obj.name = $"Timer - ({duration}s * {clockSpeed}x)";
        TimerInstance instance = obj.AddComponent<TimerInstance>();
        instance.duration = duration;
        instance.clockSpeed = clockSpeed;
        instance.id = id;
        instance.timerOver += RemoveTimer;
        timers.Add(instance);
        return instance;
    }

    public IEnumerator RemoveTimer(TimerInstance timer)
    {
        timers.Remove(timer);
        yield return null;
    }

    public void ClearAllTimers(string id = "all")
    {
        for(int i = timers.Count - 1; i >= 0; i--)
        {
            TimerInstance t = timers[i];
            if(id.Equals("all") || id.Equals(t.id))
            {
                Destroy(t.gameObject);
                timers.Remove(t);
            }
        }
    }
}
