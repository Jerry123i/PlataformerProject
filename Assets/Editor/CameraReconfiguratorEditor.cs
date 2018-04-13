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

        if (obj.follow)
        {
            Handles.color = Color.red;
            Vector3 pos = obj.transform.position;

            Handles.DrawLine(new Vector3((pos.x + obj.offsetMin), (pos.y - 3.0f)), new Vector3((pos.x + obj.offsetMin), (pos.y + 3.0f)));
            Handles.DrawLine(new Vector3((pos.x + obj.offsetMax), (pos.y - 3.0f)), new Vector3((pos.x + obj.offsetMax), (pos.y + 3.0f)));
        }

    }

}



