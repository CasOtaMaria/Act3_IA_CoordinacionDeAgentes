using UnityEngine;

namespace IA26Online.Steering.Kynematic.Evasion
{
    public class KSeparation : SteeringBehaviour
    {
        protected Rigidbody2D agent;

        [SerializeField] protected float max_aceleration;

        [SerializeField] public Rigidbody2D[] obstacles;

        [SerializeField] protected float satisfaction_rad;
        [SerializeField] protected float slow_coef;

        private void Awake()
        {
            agent = GetComponent<Rigidbody2D>();
        }

        public override SteeringOutput GetSteering() //override para que herede si ningun problema
        {
            //1) se crea un objeto sOut = new SteeringOutput()
            SteeringOutput sOut = new SteeringOutput();

            //2)por cada obstáculo en la lista de obstáculos,
            //a) el vector dirección es la resta de la posición del agente a la posición del obstáculo
            //b) la distancia entre agentes es la longitud del vector dirección
            float distance;
            Vector2 direction;
            for (int i = 0; i < obstacles.Length; i++)
            {
                direction = agent.position - obstacles[i].position; //2.a
                distance = direction.magnitude; //2.b

                //3) si la distancia es menor que el radio_de_satisfacción,
                //sOut.lineal += calcular la fuerza_de_repulsión y multiplicarla por la dirección normalizada
                if (distance < satisfaction_rad)
                {
                    sOut.linear += Mathf.Min((slow_coef / (distance * distance)), max_aceleration) * direction.normalized;
                }
            }

            //4) return sOut
            return sOut;
        }
    }
}