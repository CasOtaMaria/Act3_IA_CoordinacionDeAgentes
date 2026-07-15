using UnityEngine;

namespace IA26Online.Steering.Kynematic.Paths
{
    public class Path : MonoBehaviour
    {
        protected Vector2[] vertex;

        private void Awake() //necesito el Awake para identificar los puntos del camino
        {
            vertex = new Vector2[transform.childCount];

            for(int i = 0; i < transform.childCount; i++) //pilla los objs hijos del Empty que hemos pueto de path
            {
                vertex[i] = transform.GetChild(i).position;
            }                
        }

        public float GetParam(Vector2 agent_position, float last_param)
        {
            float closest_distance = Mathf.Infinity;
            float closest_param = 0f;

            //1) iterando desde el ultimo_parametro, por cada vertice en la lista de vertices,
            for (int i = (int)last_param; i < vertex.Length - 1; i++)
            {
                //2) obtener el vector direccion entre el vertice actual y el vertice + 1                
                Vector2 vertex_a = vertex[i];
                Vector2 vertex_b = vertex[i + 1];
                Vector2 direction = vertex_b - vertex_a; //mas facil de entender para luego

                //3) obtener la proyeccion la posicion_del_agente sobre el vector direccion
                float projection = Vector2.Dot(agent_position - vertex_a, direction.normalized);
                projection = Mathf.Clamp(projection, 0f, direction.magnitude);
                Vector2 projected_point = vertex_a + direction.normalized * projection;

                //4) se calcula la distancia que hay entre el agente y la proyeccion
                //si la distancia es menor que la distancia_mas_cercana, guardar informacion
                float distance = Vector2.Distance(agent_position, projected_point);
                if (distance < closest_distance)
                {
                    closest_distance = distance;
                    closest_param = i + projection / direction.magnitude;
                }
            }
            //return parametro_mas_cercano
            return closest_param;
        }
        public Vector2 GetPosition(float param)
        {
            int digit_part = (int)param;  //le hago un parseo para quedarme la parte entera
            //1) si la parte entera del parametro es mayor que el número de vertices en la lista,
                //devolver la posición del último vértice en la lista vértices
            if (digit_part >= vertex.Length - 1)
            {
                return vertex[vertex.Length - 1]; //en los apuntes pone que la funcion es float pero no podria ser asi. se devuelve una posicion
            }

            //2) obtener el vertice_A y vertice_B de la lista de vertices segun la parte entera del parametro
            Vector2 vertex_a = vertex[digit_part];
            Vector2 vertex_b = vertex[digit_part + 1];

            //3) devolver la ecuacion parametrica del segmento(Lerp) entre:
            //el vertice_A, el vertice_B y la parte decimal del parametro
            float decimal_part = param - digit_part;

            return Vector2.Lerp(vertex_a, vertex_b, decimal_part);
        }
    }
}