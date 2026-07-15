using UnityEngine;
using IA26Online.Steering;


namespace IA26Online.Steering
{
    //clase superior que nos asegura, que tengan siempre
    //el método get steering con el steering output como respuesta
    public abstract class SteeringBehaviour : MonoBehaviour //dinamico o kinematic
    {
        public abstract SteeringOutput GetSteering();
    }
}