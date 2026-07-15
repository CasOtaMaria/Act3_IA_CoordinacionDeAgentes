using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;
public abstract class Expert : MonoBehaviour
{
    //Los expertos determinan qué acciones ejecutar,
    //sólo cuando se alcanza el mayor nivel de insistencia en la pizarra
    //La implementación de un experto requiere una implementación propia que encapsule su lógica


    //abstract float GetInsistence(Blackboard)
    public abstract float GetInsistence(Blackboard blackboard);

    //abstract Action[] Run(Blackboard)
    public abstract void Run(Blackboard blackboard);
}
