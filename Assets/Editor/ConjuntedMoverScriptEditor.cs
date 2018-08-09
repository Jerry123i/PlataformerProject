using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConjointedMoverScript))]
public class ConjuntedMoverScriptEditor : MoverScriptEditor {

    public override void OnInspectorGUI()
    {
        var obj = target as ConjointedMoverScript;

       obj.jointedMoverScript = (MoverScript)EditorGUILayout.ObjectField(obj.jointedMoverScript, typeof(MoverScript), true);

        base.OnInspectorGUI();
    }

}
