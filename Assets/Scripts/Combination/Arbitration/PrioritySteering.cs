using UnityEngine;
using IA26Online.Steering.Combination.Arbitration;
using IA26Online.Steering.Combination.Blending;
using UnityEngine.Rendering;

namespace IA26Online.Steering.Combination.Arbitration
{
    public class PrioritySteering : MonoBehaviour
    {
        protected Rigidbody2D agent;

        [SerializeField] private BlenderSteering[] groups;
        [SerializeField] private float epsilon;

        [SerializeField] private float max_linear_vel;
        [SerializeField] private float max_angular_vel;

        private void Awake()
        {
            agent = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            SteeringOutput sOut = GetSteering();
            agent.linearVelocity = sOut.linear;
            agent.angularVelocity = sOut.angular;
        }

        public SteeringOutput GetSteering()
        {
            //se crea un objeto sOut = new SteeringOutput()
            SteeringOutput sOut = new SteeringOutput();

            //por cada grupo en la lista de grupos, el sOut es igual al resultado del grupo
            foreach (BlenderSteering group in groups)
            {
                sOut = group.GetSteering();

                //si la magnitud de sOut.lineal o el valor absoluto de sOut.angular superan el valor épsilon, return sOut
                if (sOut.linear.magnitude > epsilon || Mathf.Abs(sOut.angular) > epsilon)
                {
                    
                    sOut.linear = Vector2.ClampMagnitude(sOut.linear, max_linear_vel);
                    sOut.angular = Mathf.Clamp(sOut.angular, -max_angular_vel, max_angular_vel);
                    return sOut;
                }      
            }
            return new SteeringOutput();
        }
    }
}
