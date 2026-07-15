using UnityEngine;
using IA26Online.Steering.Kynematic.Delegation;

public class FormationAgent : MonoBehaviour
{
    private KSeek seek; //ira a su posicion de formacion

    private void Awake()
    {
            seek = GetComponent<KSeek>();
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void SetFormationTarget(Vector2 target)
    {
        seek.SetFormationTarget(target);
    }

    public void ClearFormationTarget()
    {
        seek.ClearFormationTarget();
    }
}
