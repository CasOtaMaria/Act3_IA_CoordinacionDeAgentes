using VectorAI = UnityEngine.Vector2;

namespace IA26Online.Steering
{ 
    public struct SteeringOutput
    {
        //estructura 
        public VectorAI linear;
        public float angular;

        public SteeringOutput(VectorAI linear, float angular)
        {
            this.linear = linear;
            this.angular = angular;
        }

        //vamos a sobrecargar un operador
        public static SteeringOutput operator+(SteeringOutput left, SteeringOutput right)
        {
            return new SteeringOutput
                (
                left.linear + right.linear,
                left.angular + right.angular
                );      
        }
        public static SteeringOutput operator *(SteeringOutput a, float value)
        {
            return new SteeringOutput
                (
                a.linear * value,
                a.angular * value
                );
        }
    }
}
