using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[CustomEditor(typeof(GrabPoint))]
public class GrabPointEditor : Editor
{
    GrabPoint grabPoint = null;
    List<GrabPoint> grabPoints = null;

    private void OnEnable()
    {
        grabPoint = (GrabPoint)target;
        grabPoints = grabPoint.transform.parent.GetComponent<GrabHandPose>().grabPoints;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

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

    private void OnDestroy()
    {
        grabPoints.RemoveAll(item => item == null);
    }
}
