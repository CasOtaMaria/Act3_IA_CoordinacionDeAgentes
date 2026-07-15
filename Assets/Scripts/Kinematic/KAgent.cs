using IA26Online.Steering;
using UnityEngine;

public class KAgent : MonoBehaviour //Consultara que tipo de movimiento hara
{
    protected Rigidbody2D agent; //se podria omitir y no verlo en editor quitando el serialize
    SteeringBehaviour behaviour;

    private void Awake()
    {
        agent = GetComponent<Rigidbody2D>();
    }
    //consultar el kagent que tipo de movimiento va a hacer ese agente por el hecho de ser un agente.

    private void Update()
    {
        
    }
    /* //solucionar en actividad, tener algoritmo agentes 
    private void Update()
    {
        if (target != null)
        {
            SteeringOutput sOut = GetSteering();

            agent.linearVelocity = sOut.linear;
            agent.angularVelocity = sOut.angular;
        }
    }*/
}
