using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [BoxGroup("Settings")]
    public int mainSceneIndex = 1;
    [BoxGroup("Settings")]
    [Tooltip("Events to trigger when the play button is clicked(Not when the game actually switches to the scene)")]
    public UnityEvent onPlay;
    [BoxGroup("UI Settings")]
    public Slider loadingSlider;
    [BoxGroup("UI Settings")]
    public TMP_Text loadingPercentageText;    
    [BoxGroup("UI Settings")]
    public string loadingFormat;

    private AsyncOperation operation;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        if(operation != null)
            return;
        operation = SceneManager.LoadSceneAsync(mainSceneIndex);
        operation.allowSceneActivation = false;
        onPlay.Invoke();
    }

    public void AllowSceneActivation()
    {
        if(operation == null)
            return;
        operation.allowSceneActivation = true;
    }

    private void Update()
    {
        if(operation == null)
            return;
        // operation.progress = [0, 0.9], rest of [0.9, 1] -> activation phase
        float clampedValue = operation.progress / 0.9f;
        if(loadingSlider != null)
            loadingSlider.value = clampedValue;
        if(loadingPercentageText.text != null)
            loadingPercentageText.text = string.Format(loadingFormat, clampedValue * 100f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
