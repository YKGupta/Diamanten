using UnityEngine;

public class Constants : MonoBehaviour
{
    [Range(0f, 10f)]
    public float RANGE = 1.5f;
    [Range(0f, 10f)]
    public float FIND_ITEM_RANGE = 2.5f;
    [Range(0f, 2f)]
    public float LAMP_COLLECT_RANGE = 0.64f;
    [Range(0f, 5f)]
    public float MANNEQUIN_ATTACK_RANGE = 1.5f;
    [Range(0f, 5f)]
    public float PLAYER_INVENTORY_INTERACTION_RANGE = 1.25f;
    [Range(0f, 5f)]
    public float MANNEQUIN_SLEEP_DOOR_RANGE = 1.25f;
    [Range(0f, 5f)]
    public float MANNEQUIN_SLEEP_RANGE = 1.25f;

    public static Constants instance;

    private void Awake()
    {
        instance = this;
    }
}
