using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static TMPro.TMP_Compatibility;

namespace IA26Online.Blackboard.Formations
{
    public class FormationManager : MonoBehaviour
    {
        //slots: SlotAssignment[]
        private SlotAssigment[] slots = new SlotAssigment[0];
        //patrón: FormationPattern
        private IFormationPattern pattern;
        //ajuste_de_formación: Static
        [SerializeField]
        private Vector2 referencePosition; //desde aqui se formara el patron. Ej: el circulo 

        public void SetPattern(IFormationPattern newPattern)
        {
            pattern = newPattern;
            UpdateSlotAssignments();
        }

        public void MoveReference(Vector2 movementOffset)
        {
            referencePosition += movementOffset;
        }
        public void UpdateSlotAssignments()
        {
            //recorriendo la lista de slots haciendo uso de un iterador,
            for (int i = 0; i < slots.Length; i++)
                slots[i].index = i; //el índice de slots[iterador] = iterador

            //ajuste_de_formación = patrón.UpdateSlotAssignments(slots)
        }
        public bool AddAgent(FormationAgent agent)
        {
            int newTotalSlots = slots.Length + 1;
            //si el patrón.SupportsSlots(tamaño de la lista de slots + 1) no se cumple -> false
            //el enunciado pide que las formaciones sean de mas de 1 agente asi que newTotalSlots > 1
            if (pattern != null && newTotalSlots > 1 && !pattern.SupportsSlots(newTotalSlots))
                return false;

            //se crea un objeto slot = new SlotAssignment()
            SlotAssigment slot = new SlotAssigment(agent, slots.Length);
            //se relaciona el slot con el agente
            System.Array.Resize(ref slots, slots.Length + 1);
            //se añade el slot a la lista de slots
            slots[slots.Length - 1] = slot;
            //UpdateSlotAssignments()
            UpdateSlotAssignments();
            
            return true;
        }

        public bool RemoveAgent(FormationAgent agent) //como uso arrays doy mas pasos
        { 
            int index = -1; //la posicion que quiero quitar
            //por cada slot en la lista de slots,
            for (int i = 0; i < slots.Length; i++) 
            {
                //si el agente se encuentra en el slot,
                if (slots[i].agent == agent)
                {                
                    index = i;
                    break;
                }
            }
            if (index == -1) //si no encuentra al agente entonces sale
                return false;

            //eliminar el slot de la lista de slots
            for (int i = index; i < slots.Length - 1; i++)
                slots[i] = slots[i + 1];

            System.Array.Resize(ref slots, slots.Length - 1);
            //UpdateSlotAssignments()
            UpdateSlotAssignments();
            return true;
        }

        private void Update()
        {
            if (pattern == null || slots.Length < 2)
                return;
            //por cada slot en la lista de slots,
            for (int i = 0; i < slots.Length; i++)
            {
                //se obtiene la transformación_en_patrón con patrón.GetSlotTransform(índice de slot)
                SlotAssigment slot = slots[i];
                Vector2 offset = pattern.GetSlotOffset(slot.index, slots.Length);

                //reajusto la localización del punto de referencia de formacion
                Vector2 targetPosition = referencePosition + offset;
                //por último, se establece la localización como el objetivo del agente que se encuentra en slot
                slot.agent.SetFormationTarget(targetPosition);
            }
        }
    }
}
