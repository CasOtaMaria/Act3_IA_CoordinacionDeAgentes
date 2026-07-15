using IA26Online.Blackboard.Formations;
using UnityEngine;

namespace IA26Online.Blackboard.Experts
{
    public class ObstacleExpert : Expert
    {
        public const string OBSTACLE = "obstacle";

        [SerializeField] private FormationAgent[] agents;
        [SerializeField] private float escapeDistance = 3f;
        [SerializeField] private float clearDistance = 4f;

        public override float GetInsistence(BlackboardManager blackboard)
        {
            if (blackboard.HasKey(OBSTACLE))
                return 2f;
            return 0f;
        }
        public override void Run(BlackboardManager blackboard)
        {
            Vector2 obstacle = (Vector2)blackboard.GetDataByKey<Vector2>(OBSTACLE);
            bool obstacleStillClose = false;

            for (int i = 0; i < agents.Length; i++)
            {
                Vector2 direction = agents[i].GetPosition() - obstacle;
                float distance = direction.magnitude;

                if (distance < clearDistance)
                    obstacleStillClose = true;

                if (direction == Vector2.zero)
                    direction = Vector2.right;

                Vector2 escapePosition = agents[i].GetPosition() + direction.normalized * escapeDistance;
                agents[i].SetFormationTarget(escapePosition);
            }

            if (!obstacleStillClose)
            {
                blackboard.Clear(OBSTACLE);
                for (int i = 0; i < agents.Length; i++)
                    agents[i].ClearFormationTarget();
            }
        }
    }
}
    