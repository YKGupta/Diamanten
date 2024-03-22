using UnityEngine;

[RequireComponent(typeof(MouseEvents))]
public class InteractiveHover : MonoBehaviour
{
    private IInteractionEffect effect;

    private void Start()
    {
        MouseEvents m = GetComponent<MouseEvents>();
        effect = GetComponent<IInteractionEffect>();
        m.onMouseOver += OnMouseOver_IH;
        m.onMouseExit += OnMouseExit_IH;
    }

    public void OnMouseOver_IH(GameObject obj)
    {
        if(!effect.isInteractable())
        {
            effect.EndEffect();
            return;
        }
            
        effect.StartEffect();
    }

    public void OnMouseExit_IH(GameObject obj)
    {
        if(!effect.isInteractable())
        {
            effect.EndEffect();
            return;
        }
            
        effect.EndEffect();
    }
}
