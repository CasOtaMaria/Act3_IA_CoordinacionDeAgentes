using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation //namespace explicado en steering 
{

    [RequireComponent(typeof(KAgent))] //hace depender este script del otro, y se asigna el otro automaticamente
    public class KSeek : SteeringBehaviour
    {
        protected Rigidbody2D agent;
        [SerializeField] protected Rigidbody2D target;
        [SerializeField] public float max_speed;

        //Cambios para la act3
        private Vector2 formationTarget;
        private bool hasFormationTarget;

        private void Awake()
        {
            agent = GetComponent<Rigidbody2D>();
        }
        public void SetFormationTarget(Vector2 targetPosition)
        {
            formationTarget = targetPosition;
            hasFormationTarget = true;
        }
        public void ClearFormationTarget()
        {
            hasFormationTarget = false;
        }

        public override SteeringOutput GetSteering() //override para que herede si ningun problema
        {
            //1) se crea objeto sOut
            SteeringOutput sOut = new SteeringOutput();

            //para poder pasarle la posicion de la formacion
            Vector2 targetPosition;
            if (hasFormationTarget)
                targetPosition = formationTarget;
            else
            {
                if (target == null)
                    return sOut;
                targetPosition = target.position;
            }

            //2) s.Out.lineal = resta de posicion Agente a la posicion Obj
            sOut.linear = target.position - agent.position;
            //3) se normaliza vector y luego se multiplica por v max
            if (sOut.linear.magnitude > 0.1f)
                sOut.linear = sOut.linear.normalized * max_speed;
            return sOut; //devuelve el como se movera
        }
    }
}

