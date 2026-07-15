using System;
using UnityEngine;

public class SlotAssigment
{
    //agente: Static
    public FormationAgent agent;
    //índice: int
    public int index;

    public SlotAssigment(FormationAgent agent, int index)
    {
        this.agent = agent;
        this.index = index;
    }
}