using UnityEngine;

public class Toggler : MonoBehaviour
{
    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}