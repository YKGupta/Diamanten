using UnityEngine;
using NaughtyAttributes;

public class SendMessage : MonoBehaviour
{
    public bool sendToParent;
    [HideIf("sendToParent")]
    public GameObject targetGO;

    private void OnDrawGizmosSelected()
    {
        transform.parent.SendMessage("OnDrawGizmosSelected");
    }
}
