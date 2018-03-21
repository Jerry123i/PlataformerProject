using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoverScript))]
public class MoverScriptEditor : Editor {

    bool antiHorario;
    bool horario;

    public override void OnInspectorGUI()
    {
        var obj = target as MoverScript;

        obj.working = EditorGUILayout.Toggle("Starts Active", obj.working);
        obj.moverMode = (MoverMode) EditorGUILayout.EnumPopup("Mover Mode", obj.moverMode);

        obj.speed = EditorGUILayout.FloatField("Speed", obj.speed);

        switch (obj.moverMode)
        {
            case MoverMode.ROTATION:
                obj.rotationCenter = EditorGUILayout.Vector2Field("Rotation Center", obj.rotationCenter);

                EditorGUILayout.BeginHorizontal();
                antiHorario = EditorGUILayout.Toggle("C.Clockwise", antiHorario);
                horario = !antiHorario;
                horario = EditorGUILayout.Toggle("Clockwise", horario);
                antiHorario = !horario;

                
                obj.reverse = horario;
                EditorGUILayout.EndHorizontal();


                break;

            case MoverMode.CYCLE:

                string butonText;

                if(obj.points == null)
                {
                    obj.points = new List<Vector3>();
                    obj.points.Add(obj.transform.position);
                }

                EditorGUILayout.BeginVertical();
                for(int i = 0; i<obj.points.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal("Box");
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.TextArea(i.ToString() + ":", GUILayout.MaxWidth(25));
                    obj.points[i] = EditorGUILayout.Vector2Field("",obj.points[i]);
                    EditorGUILayout.EndHorizontal();
                    if (obj.targetN == i)
                    {
                        butonText = "(T)";
                    }
                    else
                    {
                        butonText = "( )";
                    }

                    if (GUILayout.Button(butonText, GUILayout.Width(20)))
                    {
                        obj.targetN = i;
                    }

                    if (GUILayout.Button("X", GUILayout.Width(10), GUILayout.Height(10)))
                    {
                        obj.points.RemoveAt(i);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Point"))
                {
                    obj.points.Add(obj.transform.position);
                }

                EditorGUILayout.EndVertical();

                break;

            default:
                break;
        }

    }

    private void OnSceneGUI()
    {
        var obj = target as MoverScript;



        if(obj.moverMode == MoverMode.ROTATION)
        {
            obj.rotationCenter = Handles.PositionHandle(obj.rotationCenter, Quaternion.identity);
        }

        if(obj.moverMode == MoverMode.CYCLE)
        {
            if (obj.points == null)
            {
                obj.points = new List<Vector3>();
                obj.points.Add(obj.transform.position);
            }

            for (int i = 0; i< obj.points.Count; i++)
            {
                obj.points[i] = Handles.PositionHandle(obj.points[i], Quaternion.identity);

                var labelStyle = new GUIStyle();
                labelStyle.fontSize = 18;

                if(obj.targetN == i)
                {
                    labelStyle.fontSize = 25;
                    labelStyle.fontStyle = FontStyle.BoldAndItalic;
                }

                Handles.Label(obj.points[i], i.ToString(), labelStyle);

            }
        }
    }

}
