using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(HandData))]
public class HandDataEditor : Editor
{
    private HandData handData = null;

    private void OnEnable()
    {
        handData = (HandData)target;
    }

    private void OnSceneGUI()
    {
        DrawJointButtons();
    }

    private void DrawJointButtons()
    {
        foreach (Transform joint in handData.fingerBones)
        {
            if (Handles.Button(joint.position, joint.rotation, 0.01f, 0.005f, Handles.SphereHandleCap))
            {

            }
        }
    }
}
