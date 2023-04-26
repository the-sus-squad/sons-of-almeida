using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(HandData))]
public class HandDataEditor : Editor
{
    private HandData handData = null;
    private Transform activeJoint = null;
    private Color defaultColor = new Color(1, 1, 1);
    private Color activeColor = new Color(1, 0, 0);

    private void OnEnable()
    {
        handData = (HandData)target;
    }

    private void OnSceneGUI()
    {
        DrawJointButtons();
        if (activeJoint)
        {
            DrawJointRotationGizmo();
        }
    }

    private void DrawJointButtons()
    {
        foreach (Transform joint in handData.fingerBones)
        {
            Handles.color = defaultColor;
            if (joint == activeJoint)
            {
                Handles.color = activeColor;
            }

            if (Handles.Button(joint.position, joint.rotation, 0.01f, 0.005f, Handles.SphereHandleCap))
            {
                // if the same joint is selected then turn off the rotation gizmo
                activeJoint = IsSelected(joint) ? null : joint;
            }
        }
    }

    private bool IsSelected(Transform joint)
    {
        return joint == activeJoint;
    }

    private void DrawJointRotationGizmo()
    {
        Quaternion newRotation = Handles.RotationHandle(activeJoint.rotation, activeJoint.position);

        if (newRotation != activeJoint.rotation)
        {
            Undo.RecordObject(activeJoint, "Rotated joint");
            activeJoint.rotation = newRotation;
        }
    }
}
