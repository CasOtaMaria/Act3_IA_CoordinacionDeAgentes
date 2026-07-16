using UnityEngine;
namespace IA26Online.Blackboard.Formations
{
    public interface IFormationPattern
    {
        //Independientemente de la geometría, todos los patrones deben implementar la siguiente interfaz
        //Cada formación necesitará una instancia del patrón que quiera replicar

        //Static GetDriftOffset(SlotAssignment)
        //Static GetAnchorPoint()
        //Static GetSlotTransform(int)
        //bool SupportsSlots(int)

        //lo voy a simplificar todo al ser 2D
        Vector2 GetSlotOffset(int index, int totalAgents);

        //para entenderme voy a llamar al num de slots totales = totalAgents
        bool SupportsSlots(int totalAgents);

    }

    //linea recta donde los agentes se separan con la misma dist
    public class LineFormation : IFormationPattern
    {
        private readonly float spacing;
        public LineFormation(float spacing = 3f) //por ejemplo 2f, podria hacer una variable y cambiarla
        {
            this.spacing = spacing;
        }
        public bool SupportsSlots(int totalAgents) //el enunciado quiere formaciones de >1
        {
            return totalAgents > 1;
        }
        public Vector2 GetSlotOffset(int index, int totalAgents)
        {
            float centeredIndex = index - (totalAgents - 1) / 2f;
            return new Vector2(centeredIndex * spacing, 0f);
        }
    }

    //patron de V
    public class VFormation : IFormationPattern
    {
        private readonly float spacingX;
        private readonly float spacingY;
        public VFormation(float spacingX = 1.5f, float spacingY = 1.5f) //igual que antes, por ejemplo 1.5f
        {
            this.spacingX = spacingX;
            this.spacingY = spacingY;
        }
        //misma comprobacion que antes
        public bool SupportsSlots(int totalAgents) //el enunciado quiere formaciones de >1
        {
            return totalAgents > 1;
        }
        public Vector2 GetSlotOffset(int index, int totalAgents)
        {
            if (index == 0)
                return Vector2.zero; //punta V

            //row = distancia que habra dese la punta, side = si va a izq o der
            int row = (index + 1) / 2; //divido en dos grupos a traves de su indice en el array de agentes
            int side = (index % 2 == 0) ? -1 : 1; //los mando a izq o der

            return new Vector2(side * row * spacingX, -row * spacingY);
        }
    }

    //circulo que se forma en torno a un punto concreto
    public class CircleFormation : IFormationPattern
    {
        private readonly float circle_rad;

        public CircleFormation(float rad = 5f)
        {
            this.circle_rad = rad;
        }

        //misma comprobacion que antes
        public bool SupportsSlots(int totalAgents) //el enunciado quiere formaciones de >1
        {
            return totalAgents > 1;
        }
        public Vector2 GetSlotOffset(int index, int totalAgents)
        {
            //divido la circunf. segun el num de agentes
            float angleStep = 360f / totalAgents;
            float angleRad = angleStep * index * Mathf.Deg2Rad;
            //devuelvo la posicion donde debe ir el agente
            return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * circle_rad;
        }
    }
}

