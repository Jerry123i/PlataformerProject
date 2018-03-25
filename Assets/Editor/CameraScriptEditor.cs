using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraScript))]
public class CameraScriptEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        var obj = target as CameraScript;
                

        if (obj.followPlayer)
        {
            Handles.color = Color.red;
            Vector3 pos = obj.transform.position;

            Handles.DrawLine(new Vector3((pos.x + obj.offsetMin),(pos.y - 3.0f)), new Vector3((pos.x + obj.offsetMin), (pos.y + 3.0f)));
            Handles.DrawLine(new Vector3((pos.x + obj.offsetMax), (pos.y - 3.0f)), new Vector3((pos.x + obj.offsetMax), (pos.y + 3.0f)));
        }

    }

}
