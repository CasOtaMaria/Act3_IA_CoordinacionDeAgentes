using Unity.VisualScripting;
using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KWander : KFace
    {
        [SerializeField] float area_distance;
        [SerializeField] float area_rad;

        [SerializeField] float max_aceleration;

        [SerializeField] float max_angular_var;
        [SerializeField] float angular_desviation;
        private void OnDrawGizmos()
        {
            if (agent == null) return;

            Vector2 agentDir = new Vector2
            (
                Mathf.Sin(agent.rotation * Mathf.Deg2Rad),
                Mathf.Cos(agent.rotation * Mathf.Deg2Rad)
            );

            //centro del círculo
            Vector2 center = (Vector2)agent.position + agentDir * area_distance;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(center, area_rad);

            //linea desde el agente al centro
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(agent.position, center);

            //dibuja el punto objetivo actual
            if (orientation_target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(orientation_target.position, 0.15f);
            }
        }
        public override SteeringOutput GetSteering()
        {
            /*Primero desliza al objetivo sobre el perímetro del círculo en base a la variación angular máxima definida*/
            //1) la variación_angular de esta iteración es la variación_angular_máxima con un signo al azar
            //se añade la variación_angular al valor de desviación_angular_acumulada actual
            float angular_var = max_angular_var * (Random.Range(-1f, 1f) >= 0 ? 1f : -1f);
            angular_desviation += angular_var;

            //2) la orientación_objetivo es la desviación_angular_acumulada más la Face.agente.orientación
            float target_rotation = angular_desviation + agent.rotation;

            /*Como el círculo siempre se encuentra a la misma distancia frente al agente, es posible calcular el centro*/
            //3) el centro_del_círculo se encuentra a una distancia_del_círculo frente al Face.agente
            //la nueva_posición_del_objetivo se obtiene en base al centro_del_círculo y la orientación_objetivo
            Vector2 agent_direction = new Vector2(Mathf.Sin(agent.rotation * Mathf.Deg2Rad), Mathf.Cos(agent.rotation * Mathf.Deg2Rad));
            Vector2 area_centre = (Vector2)agent.position + agent_direction * area_distance;
            Vector2 target_position = area_centre + new Vector2(Mathf.Sin(target_rotation * Mathf.Deg2Rad),
                Mathf.Cos(target_rotation * Mathf.Deg2Rad)) * area_rad;

            //4) Face.objetivo.posición = nueva_posición_del_objetivo
            //sOut = Face.GetSteering()
            orientation_target.position = target_position;
            SteeringOutput sOut = base.GetSteering();

            //5) sOut.lineal = vector orientación actual del Face.agente multiplicado por la aceleración_máxima
            sOut.linear = agent_direction * max_aceleration;
            return sOut;
        }
        
    }
}