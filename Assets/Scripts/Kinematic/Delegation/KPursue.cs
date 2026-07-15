using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KPursue : KSeek
    {
        [SerializeField] private float prediction_limit;
        [SerializeField] private Rigidbody2D pursue_target;

        public override SteeringOutput GetSteering()
        {
            //1) el vector direccion es la resta de la posicion del agente a la posicion del Pursue.objetivo
            Vector2 direction = pursue_target.position - agent.position;
            //la distancia entre agentes es la longitud del vector direccion
            float distance = direction.magnitude; 

            //2) el modulo_de_velocidad es la longitud de la velocidad actual del agente
            float speed = agent.linearVelocity.magnitude;

            //3) si el modulo_de_velocidad es un valor por encima de cero,
            //el tiempo_de_prediccion debe ser la proporcion de distancia sobre el modulo_de_velocidad        
            float prediction_time;
            if (speed > 0f)
            {
                prediction_time = distance / speed;
                if (prediction_time > prediction_limit) //y en caso de que el tiempo_de_prediccion sea superior al limite_prediccion, restringirlo
                {
                    prediction_time = prediction_limit;
                }
            }
            else //si no, el tiempo_de_prediccion será exactamente igual que el limite_prediccion
            {
                prediction_time = prediction_limit;
            }

            //5) Seek.objetivo.posicion = la posicion del Pursue.objetivo +
            //+la velocidad del Pursue.objetivo × tiempo_de_prediccion
            target.position = pursue_target.position + pursue_target.linearVelocity * prediction_time; //el target de Seek, heredado

            //return Seek.getSteering()
            return base.GetSteering(); //despues de que Pursue prepare los datos, se ejecuta el metodo base de KSeek
        }
    }
}

