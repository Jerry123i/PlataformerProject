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


        if (obj.followPlayerX)
        {
            Handles.color = Color.red;
            Vector3 pos = obj.transform.position;

            Handles.DrawLine(new Vector3((pos.x + obj.offsetMinX), (pos.y - 3.0f)), new Vector3((pos.x + obj.offsetMinX), (pos.y + 3.0f)));
            Handles.DrawLine(new Vector3((pos.x + obj.offsetMaxX), (pos.y - 3.0f)), new Vector3((pos.x + obj.offsetMaxX), (pos.y + 3.0f)));
        }

        if (obj.followPlayerY)
        {
            Handles.color = Color.red;
            Vector3 pos = obj.transform.position;

            Handles.DrawLine(new Vector3((pos.x - 3.0f), (pos.y + obj.offsetMinY)), new Vector3((pos.x + 3.0f), (pos.y + obj.offsetMinY)));
            Handles.DrawLine(new Vector3((pos.x - 3.0f), (pos.y + obj.offsetMaxY)), new Vector3((pos.x + 3.0f), (pos.y + obj.offsetMaxY)));
        }

    }

}
