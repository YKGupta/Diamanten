using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyPointer : MonoBehaviour
{
    [BoxGroup("Calculation Settings")]
    [Tooltip("The point in-reference to which the angles for all targets shall be computed(player)")]
    public Transform reference;
    [BoxGroup("Calculation Settings")]
    [Tooltip("The targets'(which need to be rendered on the HUD) parent")]
    public Transform targetsParent;
    [BoxGroup("UI Settings")]
    [Tooltip("The HUD element for the target to be displayed")]
    public GameObject targetHUDElement;
    [BoxGroup("UI Settings")]
    [Tooltip("The indicator's parent for the target HUD elements")]
    public Transform targetHUDElementParent;

    class Target
    {
        public class HUDElement
        {
            public GameObject obj;
            public UI_Initialiser uI_Initialiser;

            public HUDElement(GameObject a)
            {
                obj = a;
                uI_Initialiser = obj.GetComponent<UI_Initialiser>();
            }

            public Sprite GetSprite(TargetState state)
            {
                return obj.GetComponent<StateSpriteMap>().map[state];
            }
        }

        public Transform targetTransform;
        public HUDElement hudElement;
        public ITargetStateHandler stateHandler;

        public Target(Transform a, GameObject b, ITargetStateHandler c)
        {
            targetTransform = a;
            hudElement = new HUDElement(b);
            stateHandler = c;
        }
    }

    private List<Target> _targets;

    private void Start()
    {
        _targets = new List<Target>();

        for(int i = 0; i < targetsParent.childCount; i++)
        {
            Transform child = targetsParent.GetChild(i);
            GameObject hudElement = Instantiate(targetHUDElement, targetHUDElementParent);
            ITargetStateHandler stateHandler = child.GetComponent<ITargetStateHandler>();
            _targets.Add(new Target(child, hudElement, stateHandler));
        }
    }

    private void Update()
    {
        foreach(Target target in _targets)
        {
            if(!target.targetTransform.gameObject.activeSelf)
            {
                target.hudElement.obj.SetActive(false);
                continue;
            }
            
            // Compute the angle between the target and the reference
            float angle = GetAngle(reference, target.targetTransform);
            target.hudElement.obj.SetActive(true);
            target.hudElement.obj.transform.rotation = Quaternion.Euler(0f, 0f, -angle);

            if(target.hudElement.uI_Initialiser == null)
                continue;
            target.hudElement.uI_Initialiser.GetImage().sprite = target.hudElement.GetSprite(target.stateHandler.GetState());
        }
    }

    /// <summary>Get the angle(around y-axis) between the two transforms</summary>
    /// <param name="a">The reference from which the angle is computed</param>
    /// <param name="b">The target to which the angle shall be computed</param>
    /// <returns>Returns the signed angle betwern the two, +ve being clockwise from <paramref name="a">a</paramref> and -ve for anti-clockwise</returns>
    private float GetAngle(Transform a, Transform b)
    {
        Vector3 _a = a.forward;
        Vector3 _b = b.position - a.position;
        _a.y = _b.y = 0f;
        return Vector3.SignedAngle(_a, _b, Vector3.up);
    }
}
