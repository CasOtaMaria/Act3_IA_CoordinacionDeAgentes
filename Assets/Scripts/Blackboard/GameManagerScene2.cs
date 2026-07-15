using UnityEngine;
using IA26Online.Blackboard.Formations;
using IA26Online.Blackboard.Experts;

namespace IA26Online.Blackboard
{
    public class GameManagerScene2 : MonoBehaviour
    {
        [Header("Refrences")]
        [SerializeField] private BlackboardManager blackboard;
        [SerializeField] private FormationManager formationManager;
        [SerializeField] private FormationAgent[] agents;

        [Header("Scene")]
        [SerializeField] private Camera sceneCamera;
        [SerializeField] private float clickThreshold = 3f;

        private void Awake()
        {
            if (sceneCamera == null)
                sceneCamera = Camera.main;

            for (int i = 0; i < agents.Length; i++)
                formationManager.AddAgent(agents[i]);

            formationManager.SetPattern(new LineFormation());
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            Vector2 clickPosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < agents.Length; i++)
            {
                float distance = Vector2.Distance(agents[i].GetPosition(), clickPosition);
                if (distance <= clickThreshold)
                {
                    blackboard.Publish(CommonTargetExpert.COMMON_TARGET, clickPosition);
                    return;
                }
            }
        }
    }
}