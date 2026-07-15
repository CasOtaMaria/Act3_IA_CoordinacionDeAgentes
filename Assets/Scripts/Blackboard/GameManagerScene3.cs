using UnityEngine;
using IA26Online.Blackboard.Formations;
using IA26Online.Blackboard.Experts;

namespace IA26Online.Blackboard
{
    public class GameManagerScene3 : MonoBehaviour
    {
        [SerializeField] private FormationManager formationManager;
        [SerializeField] private FormationAgent[] agents;

        [Header("Formation movement")]
        [SerializeField] private Vector2 movementDirection = Vector2.right;
        [SerializeField] private float movementSpeed = 1.5f;

        private void Awake()
        {
            for (int i = 0; i < agents.Length; i++)
                formationManager.AddAgent(agents[i]);

            formationManager.SetPattern(new CircleFormation());
        }

        private void Update()
        {
            Vector2 movement = movementDirection.normalized * movementSpeed * Time.deltaTime;
            formationManager.MoveReference(movement);
        }
    }
}