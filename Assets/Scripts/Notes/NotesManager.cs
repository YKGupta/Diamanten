using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NaughtyAttributes;

public class NotesManager : MonoBehaviour
{
    [BoxGroup("Input")]
    public KeyCode notebookViewKey;
    [BoxGroup("UI")]
    public GameObject notebookUIGO;
    [BoxGroup("UI")]
    public Transform noteUIContentParent;
    [BoxGroup("UI")]
    public GameObject noteUIPrefab;
    [BoxGroup("UI")]
    public GameObject notebookScrollViewGO;
    [BoxGroup("UI")]
    public GameObject viewNotePanelUIGO;
    [BoxGroup("UI")]
    public Image viewNoteImage;
    [BoxGroup("UI")]
    public Image viewZoomedNoteImage;

    [Foldout("Events")]
    public UnityEvent onCollectNote;

    [ReadOnly]
    public List<NoteItem> noteItems;

    [HideInInspector]
    public bool isPlayerAllowedToOpenNotebook;

    private List<GameObject> noteUIItems;

    public static NotesManager instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        noteItems = new List<NoteItem>();
        noteUIItems = new List<GameObject>();
        isPlayerAllowedToOpenNotebook = true;
    }

    private void Update()
    {
        if(!isPlayerAllowedToOpenNotebook)
            return;

        if(Input.GetKeyDown(notebookViewKey))
        {
            ToggleNotebook();
        }
    }

    public void AddNote(NoteItem item)
    {
        noteItems.Add(item);
        onCollectNote.Invoke();

        GameObject newNote = Instantiate(noteUIPrefab, noteUIContentParent);

        UI_Initialiser ui = newNote.GetComponent<UI_Initialiser>();
        ui.SetImage(item.noteSprite);
        
        NoteItemButton btn = newNote.GetComponent<NoteItemButton>();
        btn.onButtonClick += ViewNote;

        noteUIItems.Add(newNote);
    }

    public void ToggleNotebook()
    {
        bool state = !notebookUIGO.activeSelf;

        PlayerManager.instance.isPlayerAllowedToMove = !state;
        PlayerManager.instance.isPlayerAllowedToLook = !state;

        InventoryManager.instance.isPlayerAllowedToOpenInventory = !state;

        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;

        notebookUIGO.SetActive(state);

        Time.timeScale = state ? 0f : 1f;
    }

    public void ViewNote(NoteItemButton btn)
    {
        if(viewNotePanelUIGO.activeSelf)
            return;
        
        UI_Initialiser ui = btn.GetComponent<UI_Initialiser>();
        viewNoteImage.sprite = ui.GetImage().sprite;
        viewZoomedNoteImage.sprite = ui.GetImage().sprite;

        notebookScrollViewGO.SetActive(false);
        viewNotePanelUIGO.SetActive(true);
    }

    public void UnViewNote()
    {
        viewNotePanelUIGO.SetActive(false);
        notebookScrollViewGO.SetActive(true);
    }
}
