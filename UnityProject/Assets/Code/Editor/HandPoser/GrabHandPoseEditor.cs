using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GrabHandPose))]
public class GrabHandPoseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GrabHandPose grabHandPose = (GrabHandPose)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Grab Point"))
        {
            grabHandPose.AddGrabPoint();
        }
        GUILayout.EndHorizontal();
    }
}
