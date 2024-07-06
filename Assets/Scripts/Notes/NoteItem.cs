using UnityEngine;
using NaughtyAttributes;

public class NoteItem : MonoBehaviour, IInteractionEffect
{
    [BoxGroup("UI")]
    [HideIf("isAutomaticallyCollected")]
    public GameObject interactionUIGO;
    [BoxGroup("Note Item Specifics")]
    public bool isAutomaticallyCollected = false;
    [BoxGroup("Note Item Specifics")]
    [HideIf("isAutomaticallyCollected")]
    [Range(0f, 15f)]
    public float range = 2f;
    [BoxGroup("Note Item Specifics")]
    public Sprite noteSprite;

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
        return enabled && (isAutomaticallyCollected || Vector3.Distance(transform.position, PlayerInfo.instance.GetPosition()) <= range);
    }
}
