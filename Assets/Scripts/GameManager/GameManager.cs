using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [BoxGroup("Events")]
    public UnityEvent onStart;
    [BoxGroup("Events")]
    public UnityEvent onGameWon;

    private void Start()
    {
        onStart.Invoke();
    }

    public void EndGame()
    {
        Debug.Log("You won!");
        SoundManager.PlaySound(SoundType.Victory);
        Cursor.lockState = CursorLockMode.None;
        onGameWon.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
