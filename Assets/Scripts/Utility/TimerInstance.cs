using System;
using System.Collections;
using UnityEngine;

public class TimerInstance : MonoBehaviour
{
    public float duration;
    public int clockSpeed = 1;
    public string id = "all";
    public event Func<TimerInstance, IEnumerator> timerOver;
    public event Func<TimerInstance, IEnumerator> timerStart;

    private float timer;

    private void Start()
    {
        timer = duration;
        if(timerStart != null)
            StartCoroutine(timerStart(this));
    }

    private void Update()
    {
        timer = Mathf.Max(0f, timer - Time.deltaTime * clockSpeed);
        if(timer == 0f)
        {
            if(timerOver != null)
                StartCoroutine(timerOver(this));
            Destroy(gameObject, 1f);
        }
    }

    /// <summary>
    /// Get timer's current normalized value
    /// </summary>
    /// <returns>Returns a value going from 0 to 1 over time</returns>
    public float NormalizedValue()
    {
        if(duration == 0f)
            return 1f;
        return (duration - timer) / duration;
    }
}