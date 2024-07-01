using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [Tooltip("Order determines the order of instructions display(for automatically activated Instructions)")]
    public Instruction[] instructions;

    private Queue<Instruction> queue;

    private void Start()
    {
        queue = new Queue<Instruction>();

        foreach(Instruction i in instructions)
        {
            if(i.activateAutomatically)
                queue.Enqueue(i);
            i.onEnd += OnInstructionEnd;
        }
        
        SetNextAutomaticInstruction();
    }

    public void OnInstructionEnd(Instruction instruction)
    {
        instruction.Deactivate();
        instruction.onEnd -= OnInstructionEnd;
        SetNextAutomaticInstruction();
    }

    private void SetNextAutomaticInstruction()
    {
        if(queue.Count > 0)
        {
            queue.Peek().Activate();
            StartCoroutine(queue.Dequeue().Update());
        }
    }
}
