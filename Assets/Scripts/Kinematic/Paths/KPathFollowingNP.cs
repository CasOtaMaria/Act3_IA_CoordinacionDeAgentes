using IA26Online.Steering.Kynematic.Delegation;
using UnityEngine;

namespace IA26Online.Steering.Kynematic.Paths
{
    public class KPathFollowingNP : KSeek
    {
        [SerializeField] protected Path path;
        [SerializeField] private float phase_to_obj;
        
        private float index; //lo pongo float porque con int no puedo igualarlo a GetParam (devuelve un float)

        public override SteeringOutput GetSteering()
        {
            //1) el índice_actual será el parámetro correspondiente en el camino a la posición del Seek.agente
            index = path.GetParam(agent.position, index);

            //2) aplicar la fase_al_objetivo al índice_actual para obtener el índice_del_objetivo
            float obj_index = index + phase_to_obj;

            //3) Seek.objetivo.posición = la proyección sobre el camino del índice_del_objetivo
            target.position = path.GetPosition(obj_index);

            //return Seek.getSteering()
            return base.GetSteering();
        }
    }
}