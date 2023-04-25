using UnityEditor;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

[CustomEditor(typeof(FieldOfView))]
public class NewBehaviourScript : Editor
{
    private void OnSceneGUI() {
        FieldOfView fov = (FieldOfView)target;

        // Show radius circle

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // Cursed math shenanigans to show cone area

        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.radius);

        // Target is visible indication

        if (fov.canSeePlayer) {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.targetObject.transform.position);
        }

    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegress) {
        angleInDegress += eulerY;

        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
}
