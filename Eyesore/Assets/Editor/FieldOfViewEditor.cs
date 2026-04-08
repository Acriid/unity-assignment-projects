using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward,Vector3.up,360,fov.GetRadius());

        Vector3 viewAngle01 = DirectionFromAnlge(fov.transform.eulerAngles.y,-fov.GetAngle()/2f);
        Vector3 viewAngle02 = DirectionFromAnlge(fov.transform.eulerAngles.y, fov.GetAngle()/2f);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.GetRadius());
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.GetRadius());

        if(fov.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.GetPlayerReference().transform.position);
        }
    }

    private Vector3 DirectionFromAnlge(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees* Mathf.Deg2Rad) ,0);
    }
}
