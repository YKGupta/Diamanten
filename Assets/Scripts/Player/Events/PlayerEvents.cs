using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action<PlayerMovement> Moved;
    public event Action<PlayerMovement> NotMoved;
    public event Action<PlayerMovement> Jumped;

    public void InvokeMoved(PlayerMovement parameter)
    {
        Moved?.Invoke(parameter);
    }

    public void InvokeNotMoved(PlayerMovement parameter)
    {
        NotMoved?.Invoke(parameter);
    }

    public void InvokeJumped(PlayerMovement parameter)
    {
        Jumped?.Invoke(parameter);
    }
}
