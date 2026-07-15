using System;
using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KAlign : SteeringBehaviour
    {
        protected Rigidbody2D agent;
        [SerializeField] protected Rigidbody2D target;

        [SerializeField] protected float max_rotation;
        [SerializeField] protected float max_angular_ac;

        [SerializeField] protected float entry_margin;
        [SerializeField] protected float desac_margin;

        [SerializeField] protected float time_to_target;


        private void Awake()
        {
            agent = GetComponent<Rigidbody2D>();
        }
        public override SteeringOutput GetSteering() //override para que herede si ningun problema
        {
            //1) el error_angular es la resta de la orientación del agente a la orientación del objetivo
            float angular_error = target.rotation - agent.rotation;
            //2) se mapea el error_angular a un rango entre - 180º y 180º(-pi y pi)
            angular_error = ((angular_error + 180f) % 360f + 360f) % 360f - 180f;

            //3) el valor absoluto del error_angular, por lo tanto, será la magnitud_de_rotación
            float rotation_mag = Mathf.Abs(angular_error);

            //4) si la magnitud_de_rotación es menor que el margen_de_entrada, no devolver nada
            if (rotation_mag < entry_margin)
                return new SteeringOutput();

            //5) si la magnitud_de_rotación es menor que el margen_de_desaceleración, a rotación_objetivo es proporcional a la magnitud_de_rotación
            float target_rotation;
            if (rotation_mag < desac_margin)
            {
                target_rotation = max_rotation * (rotation_mag / desac_margin);
            }
            else //si no, la rotación_objetivo será igual a la rotación_máxima
            {
                target_rotation = max_rotation;
            }

            //6) aplicar a la rotación_objetivo el mismo símbolo(positivo o negativo) que el error_angular
            target_rotation *= Mathf.Sign(angular_error);

            //7) se crea un objeto sOut = new SteeringOutput()
            SteeringOutput sOut = new SteeringOutput();
            sOut.angular = target_rotation;

            //8) Diferencia con la rotación del agente, ajustada para llegar en el tiempo_al_objetivo
            sOut.angular = (sOut.angular - agent.angularVelocity) / time_to_target;

            //9) si la magnitud de la aceleración ideal es mayor que la aceleración_angular_máxima,
                //mantengo el símbolo de la rotación y luego se multiplica por la aceleración_angular_máxima
            if (Mathf.Abs(sOut.angular) > max_angular_ac)
            {
                sOut.angular = Mathf.Sign(sOut.angular) * max_angular_ac;
            }

            return sOut;
        }
    }
}

