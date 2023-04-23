using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GrabPoint))]
public class GrabPointInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GrabPoint grabPoint = (GrabPoint)target;

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Mirror Left Hand"))
        {
            grabPoint.MirrorHandPose(grabPoint.leftHandPose);
        }
        
        if (GUILayout.Button("Mirror Right Hand"))
        {
            grabPoint.MirrorHandPose(grabPoint.rightHandPose);
        }
        GUILayout.EndHorizontal();
    }
}
