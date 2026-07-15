using IA26Online.Steering;
using UnityEngine;

namespace IA26Online.Steering.Kynematic.Delegation
{
    public class KFlee : KSeek //es lo mismo que kseek pero el movimiento cambia
    {
        public override SteeringOutput GetSteering()
        {
            //se crea objeto sOut
            SteeringOutput sOut = base.GetSteering();

            sOut.linear *= -1;

           return sOut; //devuelve el como se movera
        }
    }
}