using UnityEngine;
using NaughtyAttributes;

public class PeakSyncMeter : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField]
    private float peak = 5f;
    [BoxGroup("Settings")]
    [SerializeField]
    private float increment = 1f;
    
    [ReadOnly]
    public float value;

    private bool sync;

    private void Start()
    {
        sync = true;
        value = peak;
    }

    private void Update()
    {
        if(!sync)
            return;
        
        if(value < peak)
        {
            Increment(increment * Time.deltaTime);
        }
    }

    public float GetValue()
    {
        return value;
    }

    public void Decrement(float amount)
    {
        value = Mathf.Max(0f, value - amount);
    }

    public void Increment(float amount)
    {
        value = Mathf.Min(peak, value + amount);
    }

    /// <summary>
    /// Sets whether the meter should sync with realtime or not
    /// </summary>
    /// <param name="state">If false, meter does not revert back to original value (default = true)</param>
    public void SetSync(bool state)
    {
        sync = state;
    }
}
