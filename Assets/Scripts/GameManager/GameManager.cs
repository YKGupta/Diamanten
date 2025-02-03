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

    public void EndGame()
    {
        Debug.Log("You won!");
        SoundManager.PlaySound(SoundType.Victory);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
