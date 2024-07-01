using System;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class Instruction
{
    [BoxGroup("Settings")]
    public string name;
    [BoxGroup("Settings")]
    public GameObject UI_GO;
    [BoxGroup("Settings")]
    public bool activateAutomatically = false;
    [BoxGroup("Settings")]
    public bool slowDown = false;
    [ShowIf("slowDown")]
    [BoxGroup("Settings")]
    public float timeScale = 0.5f;
    [BoxGroup("Settings")]
    public InstructionType type = InstructionType.InputBound;

    [ShowIf("type", InstructionType.InputBound)]
    [BoxGroup("Settings")]
    public KeyCode[] keys;

    [ShowIf("type", InstructionType.TimeBound)]
    [BoxGroup("Settings")]
    public float duration;

    [HideInInspector]
    public event Action<Instruction> onEnd;

    private bool active = false;

    public void Activate()
    {
        if(slowDown)
            Time.timeScale = timeScale;
        
        active = true;
        
        UI_GO.SetActive(true);
    }

    public IEnumerator Update()
    {
        while(active)
        {
            switch(type)
            {
                case InstructionType.InputBound:
                {
                    foreach(KeyCode key in keys)
                        if(Input.GetKeyDown(key))
                            onEnd?.Invoke(this);
                    break;
                }
                case InstructionType.TimeBound:
                {
                    duration -= Time.deltaTime;

                    if(duration <= 0f)
                        onEnd?.Invoke(this);
                    break;
                }
            }
            yield return null;
        }
    }

    public void Deactivate()
    {
        if(slowDown)
            Time.timeScale = 1f;
        
        active = false;

        UI_GO.SetActive(false);
    }
}
