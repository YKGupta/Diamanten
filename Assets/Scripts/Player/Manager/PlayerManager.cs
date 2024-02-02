using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [HideInInspector]
    public bool isPlayerAllowedToMove = true;
    [HideInInspector]
    public bool isPlayerAllowedToLook = true;
    [HideInInspector]
    public bool isPlayerAllowedToCrouch = true;

    private void Awake()
    {
        instance = this;
    }
}
