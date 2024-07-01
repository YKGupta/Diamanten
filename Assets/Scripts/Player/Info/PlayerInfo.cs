using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Transform center;
    public static PlayerInfo instance;

    private void Awake()
    {
        instance = this;
    }

    public Vector3 GetPosition()
    {
        return center.position;
    }
}
