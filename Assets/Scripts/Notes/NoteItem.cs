using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class NoteItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    [HideIf("isAutomaticallyCollected")]
    public GameObject interactionUIGO;
    [BoxGroup("Note Item Specifics")]
    public bool isAutomaticallyCollected = false;
    [BoxGroup("Note Item Specifics")]
    public Sprite noteSprite;
    [BoxGroup("Note Item Specifics")]
    public UnityEvent onNoteCollect;

    private MouseEvents mouseEvents;

    private void Start()
    {
        if(isAutomaticallyCollected)
            return;

        mouseEvents = GetComponent<MouseEvents>();
        mouseEvents.onMouseDown += CollectNote;
    }

    public void CollectNote(GameObject obj)
    {
        if(!isInteractable())
            return;
        
        onNoteCollect.Invoke();
        NotesManager.instance.AddNote(this);
        gameObject.transform.SetParent(NotesManager.instance.transform);
        gameObject.SetActive(false);
    }

    public void StartEffect()
    {
        interactionUIGO.SetActive(true);
    }

    public void EndEffect()
    {
        interactionUIGO.SetActive(false);
    }

    public bool isInteractable()
    {
        return enabled && (isAutomaticallyCollected || Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= Constants.instance.RANGE);
    }
}
