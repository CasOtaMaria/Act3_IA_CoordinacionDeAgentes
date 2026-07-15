using System;
using UnityEngine;
namespace IA26Online.Blackboard.Formations
{
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
}
    