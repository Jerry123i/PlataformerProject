using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerScript))]
public class PlayerScriptEditor : Editor
{
    private bool isShowing = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        isShowing = EditorGUILayout.Toggle("Show", isShowing);
    }

    private void OnSceneGUI()
    {
        var obj = target as PlayerScript;
        if (isShowing)
            obj.hitZoneOffset = Handles.PositionHandle(obj.hitZoneOffset + obj.transform.position, Quaternion.identity) -obj.transform.position;
    }
}
