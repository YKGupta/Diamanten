using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [BoxGroup("Events")]
    public UnityEvent onStart;

    private void Start()
    {
        onStart.Invoke();
    }
}
