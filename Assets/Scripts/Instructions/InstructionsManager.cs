using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [Tooltip("Order determines the order of instructions display(for automatically activated Instructions)")]
    public Instruction[] instructions;
    [Tooltip("These instructions are added to the queue on trigger basis")]
    public Instruction[] triggerBasedInstructions;

    private Queue<Instruction> queue;
    private HashSet<int> triggerInstructionsPresentInQueue;
    private bool isAnInstructionBeingDisplayed = false;

    private void Start()
    {
        queue = new Queue<Instruction>();
        triggerInstructionsPresentInQueue = new HashSet<int>();
        isAnInstructionBeingDisplayed = false;

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
        int x = Array.IndexOf(triggerBasedInstructions, instruction);
        bool wasInstructionActive = instruction.IsActive();
        if(!wasInstructionActive && x == -1)
            return;
        
        instruction.Deactivate();
        instruction.onEnd -= OnInstructionEnd;
        if(wasInstructionActive)
        {
            isAnInstructionBeingDisplayed = false;
            SetNextAutomaticInstruction();
        }
        if(x != -1)
        {
            triggerInstructionsPresentInQueue.Remove(x);
            Queue<Instruction> temp = new Queue<Instruction>();
            while(queue.Count != 0)
            {
                Instruction ins = queue.Dequeue();
                if(ins == instruction)
                    continue;
                temp.Enqueue(ins);
            }
            queue = temp;
        }
    }

    public void Enqueue(int index)
    {
        if(index >= triggerBasedInstructions.Length || triggerInstructionsPresentInQueue.Contains(index))
            return;

        triggerInstructionsPresentInQueue.Add(index);
        queue.Enqueue(triggerBasedInstructions[index]);
        triggerBasedInstructions[index].onEnd += OnInstructionEnd;
        
        if(queue.Count == 1 && !isAnInstructionBeingDisplayed)
            SetNextAutomaticInstruction();
    }

    public void NotifyEndOfInstruction(int index)
    {
        if(index >= triggerBasedInstructions.Length || !triggerInstructionsPresentInQueue.Contains(index))
            return;
        triggerBasedInstructions[index].NotifyEndOfInstruction();
    }

    private void SetNextAutomaticInstruction()
    {
        if(queue.Count > 0)
        {
            StartCoroutine(ActivateInstruction(queue.Dequeue()));
        }
    }

    private IEnumerator ActivateInstruction(Instruction instruction)
    {
        isAnInstructionBeingDisplayed = true;
        yield return new WaitForSeconds(instruction.delay);
        instruction.Activate();
        StartCoroutine(instruction.Update());
    }
}
