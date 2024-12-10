using UnityEngine;

public class SleepMannequin : MonoBehaviour
{
    public Transform mannequin;
    public GameObject GFX;
    public Material originalMat;

    private void Update()
    {
        // Since when a mannequin is set for going to sleep, we set the MannequinChase component to disabled, so I am using that to verify whether is mannequin is on the way to sleep or not
        if(mannequin.GetComponent<MannequinChase>().enabled)
            return;
        
        if(Vector3.Distance(mannequin.position, transform.position) <= Constants.instance.MANNEQUIN_SLEEP_RANGE)
        {
            GFX.GetComponent<Renderer>().material = originalMat;
            mannequin.gameObject.SetActive(false);
            enabled = false;
        }
    }
}
