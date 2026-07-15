using UnityEngine;
using IA26Online.Blackboard.Formations;

namespace IA26Online.Blackboard
{
    public class GameManagerScene1 : MonoBehaviour
    {
        [SerializeField] private FormationManager formationManager;
        [SerializeField] private FormationAgent[] agents;

        [Header("Formation parametres")]
        [Header("Line")]
        [SerializeField] private float lineSpacing = 2f;
        [Header("V")]
        [SerializeField] private float vSpacingX = 1.5f;
        [SerializeField] private float vSpacingY = 1.5f;
        [Header("Circle")]
        [SerializeField] private float circleRadius = 5f;

        private LineFormation lineFormation;
        private VFormation vFormation;
        private CircleFormation circleFormation;

        private void Awake()
        {
            lineFormation = new LineFormation(lineSpacing);
            vFormation = new VFormation(vSpacingX, vSpacingY);
            circleFormation = new CircleFormation(circleRadius);

            for (int i = 0; i < agents.Length; i++)
                formationManager.AddAgent(agents[i]);

            formationManager.SetPattern(lineFormation); //les pongo en fila por defecto
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                formationManager.SetPattern(lineFormation);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                formationManager.SetPattern(vFormation);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                formationManager.SetPattern(circleFormation);
        }
    }
}