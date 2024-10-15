using UnityEngine;

public class SleepMannequin : MonoBehaviour
{
    public Transform mannequin;
    public GameObject GFX;
    public Material originalMat;

    private void Update()
    {
        if(Vector3.Distance(mannequin.position, transform.position) <= Constants.instance.MANNEQUIN_SLEEP_RANGE)
        {
            GFX.GetComponent<Renderer>().material = originalMat;
            mannequin.gameObject.SetActive(false);
            enabled = false;
        }
    }
}
