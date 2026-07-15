using System;
using System.Collections.Generic;
using UnityEngine;

namespace IA26Online.Steering.Combination.Blending
{
    public class BlenderSteering : MonoBehaviour //lo puedo agregar como componente
    {
        //Pondremos aqui el BehaviourAndWeight porque solo se usa dentro de BlenderSteering
        [Serializable] //de esta forma nos aseguramos que Unity puede mostrarlo
        public struct BehaviourAndWeight
        {
            public SteeringBehaviour behaviour; //con esto nos referimos a un algor. (seek, flee, avoidance...)
            [Range(0, 1)] public float weight; //debera ser entre 1 y 0

            public BehaviourAndWeight(SteeringBehaviour behaviour, float weight) //creamos el constructor
            {
                this.behaviour = behaviour;
                this.weight = weight;
            }
        }

        [SerializeField] private BehaviourAndWeight[] behaviours;

        [SerializeField] private float max_lineal_velocity;
        [SerializeField] private float max_angular_velocity;

        private Rigidbody2D agent;

        private void Awake()
        {
            agent = GetComponent<Rigidbody2D>(); //templatizacion
        }

        private void Update() //con esto se mueve el agente
        {
            SteeringOutput sOut = GetSteering();

            agent.linearVelocity = sOut.linear;
            agent.angularVelocity = sOut.angular;
        }
        public SteeringOutput GetSteering()
        {
            SteeringOutput sOut = new SteeringOutput();

            foreach (BehaviourAndWeight baw in behaviours)
            {
                sOut += baw.behaviour.GetSteering() * baw.weight; //podemos hacerlo gracias a que hemos sobrecargado los operadores en SteeringOutput
            }
            //sOut.linear = valor min entre el valor lineal actual o aceleracion lin max
            sOut.linear = Vector2.Min(sOut.linear, sOut.linear.normalized * max_lineal_velocity);
            sOut.angular = Mathf.Min(sOut.angular, max_angular_velocity);

            return sOut;
        }
    }
}
