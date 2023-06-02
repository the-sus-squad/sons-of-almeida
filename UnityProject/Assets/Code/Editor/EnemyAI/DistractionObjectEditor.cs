using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(DistractionObject))]
public class DistractionObjectEditor : Editor {
    private void OnSceneGUI() {
        DistractionObject obj = (DistractionObject)target;

        // Show radius circle

        Handles.color = Color.white;
        Handles.DrawWireArc(obj.transform.position, Vector3.up, Vector3.forward, 360, obj.soundRadius);

    }
}
