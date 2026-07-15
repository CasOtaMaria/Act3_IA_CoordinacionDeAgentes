using IA26Online.Blackboard.Formations;
using IA26Online.Blackboard;
using UnityEngine;
namespace IA26Online.Blackboard.Experts
{
    //se encarga de leer la posicion publicada en la blackboard y hacer que los agentes vayan al objetivo
    //para la escena 2
    public class CommonTargetExpert : Expert
    {
        public const string COMMON_TARGET = "common_target";

        [SerializeField] private FormationAgent[] agents;
        [SerializeField] private float arrivalDistance = 0.5f;

        public override float GetInsistence(BlackboardManager blackboard)
        {
            if (blackboard.HasKey(COMMON_TARGET))
                return 1f;
            return 0f;
        }

        public override void Run(BlackboardManager blackboard)
        {
            Vector2 target = (Vector2)blackboard.GetDataByKey<Vector2>(COMMON_TARGET);

            bool allAgentsArrived = true;

            for (int i = 0; i < agents.Length; i++)
            {
                agents[i].SetFormationTarget(target);

                float distance = Vector2.Distance(agents[i].GetPosition(), target);

                if (distance > arrivalDistance)
                    allAgentsArrived = false;
            }
            if (allAgentsArrived)
            {
                blackboard.Clear(COMMON_TARGET);

                for (int i = 0; i < agents.Length; i++)
                    agents[i].ClearFormationTarget();
            }
        }
    }
}
    