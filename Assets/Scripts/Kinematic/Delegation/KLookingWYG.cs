using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KLookingWYG : KAlign
    {
        public override SteeringOutput GetSteering()
        {
            //1) si la velocidad del agente es igual a cero, no devolver nada
            if (agent.linearVelocity.magnitude == 0)
            {
                return new SteeringOutput();
            }
            //2) la orientacion_objetivo del agente es el arco tangente de la velocidad del agente; un angulo
            //Align.objetivo.orientación = orientacion_objetivo
            float agent_rotation = Mathf.Atan2(agent.linearVelocity.y, agent.linearVelocity.x) 
                * Mathf.Rad2Deg; //vectores. IMP el orden 'y' 'x'. El arctang me lo hace en rad y quiero grados
            target.rotation = agent_rotation;

            //3) return Align.getSteering()
            return base.GetSteering();
        }
    }
}
