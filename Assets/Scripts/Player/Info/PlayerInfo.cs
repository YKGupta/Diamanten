using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    private void Awake()
    {
        instance = this;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
