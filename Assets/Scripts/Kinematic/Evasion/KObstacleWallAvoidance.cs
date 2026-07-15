using UnityEngine;
using IA26Online.Blackboard;
namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KObstacleWallAvoidance : KSeek
    {
        CollisionDetectionMode2D collision_detector;

        //[SerializeField] private Rigidbody2D seek_target;
        [SerializeField] protected float evade_distance;
        [SerializeField] protected float raycast_length;
        [SerializeField] private LayerMask obstacle_layer;

        private Vector2 last_direction;

        [SerializeField] private BlackboardManager blackboard;

        private void Awake()
        {
            agent = GetComponent<Rigidbody2D>();
        }
        public override SteeringOutput GetSteering()
        {/*
            //1) el raycast que se usa para detectar colisiones es el vector velocidad del Seek.agente
            //como el raycast tiene un tamanio finito, se multiplica por la longitud_de_raycast definida
            Vector2 raycast_direction;

            if (seek_target != null) // para que siga al target y no vaya para abajo 
            {
                // Apunta hacia donde el agente quiere ir (target)
                Vector2 toTarget = seek_target.position - agent.position;
                if (toTarget.magnitude > 0.01f)
                    raycast_direction = toTarget.normalized;
                else
                    return new SteeringOutput(); // ya está en el objetivo
            }

            else if (agent.linearVelocity.magnitude > 0.01f)
            {
                raycast_direction = agent.linearVelocity.normalized; // * raycast_length; //ver si hay que normalizar el vector

            }
            else //si es null y no se mueve el agent que use la rotacion del rigid body para mostrar el raycast 
            {
                float rad = agent.rotation * Mathf.Deg2Rad;
                raycast_direction  = new Vector2(-Mathf.Sin(rad), Mathf.Cos(rad));
            }
            //hay que normalizarlo para evitar los problemas vel-distancia
            */

            //2) guardar la Collision desde el detector_de_colisiones; posicion del Seek.agente y el raycast
            RaycastHit2D raycast_collision = Physics2D.Raycast(agent.position, agent.linearVelocity.normalized, raycast_length, obstacle_layer); //dir y layer para solo obstacle

            Debug.DrawRay(agent.position, agent.linearVelocity.normalized * raycast_length,
                raycast_collision ? Color.red : Color.green); //para verlo 

            if (raycast_collision)
                blackboard.Publish("obstacle", raycast_collision.point);

            //3) si no hay colision, no devolver nada
            if (!raycast_collision)
                return base.GetSteering();

            //4) Seek.objetivo.posicion = la posicion de la colision + el vector normal de la colision x la distancia_de_evasion
            //los raycast tienen point, no position

            //5) return Seek.getSteering()            
           target.position = raycast_collision.point + raycast_collision.normal *evade_distance;
           return base.GetSteering();
        }
    }
}