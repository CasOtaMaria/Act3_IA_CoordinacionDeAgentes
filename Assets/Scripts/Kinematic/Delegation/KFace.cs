using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KFace : KAlign
    {
        [SerializeField] protected Rigidbody2D orientation_target;
        public override SteeringOutput GetSteering()
        {
            //1) el vector dirección es la resta de la posición del agente a la posición del Face.objetivo
            Vector2 direction = orientation_target.position - agent.position;
            //2)la distancia entre agentes es la longitud del vector dirección
            float distance = direction.magnitude;

            //3) si la distancia es igual a cero, no devolver nada
            if (distance == 0f)
                return new SteeringOutput();

            //4) la orientación_objetivo del agente es el arco tangente del vector dirección; un ángulo
            //5) Align.objetivo.orientación = orientación_objetivo
            float target_rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            target.rotation = target_rotation;

            //6) return Align.getSteering()
            return base.GetSteering();
        }
    }
}
