using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [BoxGroup("Settings")]
    public GameObject menuGO;
    [BoxGroup("Settings")]
    public KeyCode pauseKey;
    [BoxGroup("Settings")]
    public bool allowToOpenInventory = false;
    [BoxGroup("Settings")]
    public bool allowToOpenObjectives = false;

    [BoxGroup("Settings")]
    [ShowIf("allowToOpenInventory")]
    public KeyCode inventoryKey;
    [BoxGroup("Settings")]
    public KeyCode notebookKey;
    [BoxGroup("Settings")]
    [ShowIf("allowToOpenObjectives")]
    public KeyCode objectiveKey;
    [BoxGroup("Settings")]
    public GameObject pauseMenuGO;
    [BoxGroup("Settings")]
    public GameObject inventoryGO;
    [BoxGroup("Settings")]
    public GameObject notebookGO;
    [BoxGroup("Settings")]
    public GameObject objectiveGO;
    [BoxGroup("Quit Settings")]
    public int menuSceneIndex = 0;
    [BoxGroup("Quit Settings")]
    public Slider loadingSlider;
    [BoxGroup("Quit Settings")]
    public TMP_Text loadingPercentageText;
    [BoxGroup("Quit Settings")]
    public string loadingFormat = "{0:F1}%";

    [ReadOnly]
    public NavLink currentState = NavLink.PauseMenu;

    private AsyncOperation operation;

    private void Update()
    {
        if(operation != null)
        {
            float clampedValue = operation.progress / 0.9f;
            if(loadingSlider != null)
                loadingSlider.value = clampedValue;
            if(loadingPercentageText.text != null)
                loadingPercentageText.text = string.Format(loadingFormat, clampedValue * 100f);
            return;
        }
        bool pauseKeyPressed = Input.GetKeyDown(pauseKey), inventoryKeyPressed = allowToOpenInventory && Input.GetKeyDown(inventoryKey), notebookKeyPressed = Input.GetKeyDown(notebookKey), objectiveKeyPressed = allowToOpenObjectives && Input.GetKeyDown(objectiveKey);
        if(!pauseKeyPressed && !inventoryKeyPressed && !notebookKeyPressed && !objectiveKeyPressed)
            return;
        NavLink target = pauseKeyPressed ? NavLink.PauseMenu : inventoryKeyPressed ? NavLink.Inventory : notebookKeyPressed ? NavLink.Notebook : NavLink.Objective;
        // I want the system to be as follows:
        // Let (state, menu, input), where state = current state of the menu(on/off), menu = currently opened menu(is state is true), input = key pressed
        //      state       menu            input       Output
        //      on          any             PauseKey    off
        //      off         nil             input       on(input)
        //      on          input           !PauseKey   off
        //      on          !input          !PauseKey   on(input menu)
        if(pauseKeyPressed)
            Set(!menuGO.activeSelf, target);
        else
            Set(!menuGO.activeSelf || currentState != target, target);
    }

    public void ResumeButton()
    {
        Set(false, NavLink.PauseMenu);
    }

    public void QuitButton()
    {
        if(operation != null)
            return;
        operation = SceneManager.LoadSceneAsync(menuSceneIndex);
    }

    public void Set(bool state, NavLink target)
    {
        PlayerManager.instance.isPlayerAllowedToMove = !state;
        PlayerManager.instance.isPlayerAllowedToLook = !state;

        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;

        if(state)
            Navigate(target);

        menuGO.SetActive(state);

        Time.timeScale = state ? 0f : 1f;
    }

    public void Navigate(NavLink target)
    {
        pauseMenuGO.SetActive(false);
        inventoryGO.SetActive(false);
        notebookGO.SetActive(false);
        objectiveGO.SetActive(false);
        switch(target)
        {
            case NavLink.PauseMenu:
            {
                pauseMenuGO.SetActive(true);
                break;
            }
            case NavLink.Inventory:
            {
                inventoryGO.SetActive(true);
                break;
            }
            case NavLink.Notebook:
            {
                notebookGO.SetActive(true);
                break;
            }
            case NavLink.Objective:
            {
                objectiveGO.SetActive(true);
                break;
            }
        }

        currentState = target;
    }
}
