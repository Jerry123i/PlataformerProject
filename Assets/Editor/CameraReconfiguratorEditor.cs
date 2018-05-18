using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraAreaReconfigurator))]
public class CameraReconfiguratorEditor : Editor {

    public float cameraLadoX = 20f;
    public float cameraLadoY = 12f;

    private void OnSceneGUI()
    {
        var obj = target as CameraAreaReconfigurator;

        Handles.color = Color.white;
        Handles.DrawLine(new Vector3(obj.transform.position.x - (cameraLadoX / 2), obj.transform.position.y + (cameraLadoY / 2)), new Vector3(obj.transform.position.x + (cameraLadoX / 2), obj.transform.position.y + (cameraLadoY / 2)));
        Handles.DrawLine(new Vector3(obj.transform.position.x - (cameraLadoX / 2), obj.transform.position.y - (cameraLadoY / 2)), new Vector3(obj.transform.position.x + (cameraLadoX / 2), obj.transform.position.y - (cameraLadoY / 2)));
        Handles.DrawLine(new Vector3(obj.transform.position.x - (cameraLadoX / 2), obj.transform.position.y + (cameraLadoY / 2)), new Vector3(obj.transform.position.x - (cameraLadoX / 2), obj.transform.position.y - (cameraLadoY / 2)));
        Handles.DrawLine(new Vector3(obj.transform.position.x + (cameraLadoX / 2), obj.transform.position.y + (cameraLadoY / 2)), new Vector3(obj.transform.position.x + (cameraLadoX / 2), obj.transform.position.y - (cameraLadoY / 2)));

        if (obj.followX)
        {
            Handles.color = Color.red;
            Vector3 pos = obj.transform.position;

            float offsetYMiddle = (obj.offsetMaxY + obj.offsetMinY) / 2;

            Handles.DrawLine(new Vector3((pos.x + obj.offsetMinX), (pos.y - 3.0f + offsetYMiddle)), new Vector3((pos.x + obj.offsetMinX), (pos.y + 3.0f + offsetYMiddle)));
            Handles.DrawLine(new Vector3((pos.x + obj.offsetMaxX), (pos.y - 3.0f + offsetYMiddle)), new Vector3((pos.x + obj.offsetMaxX), (pos.y + 3.0f + offsetYMiddle)));
        }

        if (obj.followY)
        {
            Handles.color = Color.red;
            Vector3 pos = obj.transform.position;

            float offsetXMiddle = (obj.offsetMaxX + obj.offsetMinX) / 2;
            

            Handles.DrawLine(new Vector3((pos.x - 3.0f + offsetXMiddle), (pos.y + obj.offsetMinY)), new Vector3((pos.x + 3.0f + offsetXMiddle), (pos.y + obj.offsetMinY)));
            Handles.DrawLine(new Vector3((pos.x - 3.0f + offsetXMiddle), (pos.y + obj.offsetMaxY)), new Vector3((pos.x + 3.0f + offsetXMiddle), (pos.y + obj.offsetMaxY)));
        }

    }

}



