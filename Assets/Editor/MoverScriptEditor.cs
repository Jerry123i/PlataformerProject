using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoverScript))]
public class MoverScriptEditor : Editor {

    bool antiHorario;
    bool horario;

    bool lockRotationTransform;
    Vector3 radiusDiference;

    List<bool> lockCycleTransforms;
    List<Vector3> transformsDiference; 

    bool lines;

    public override void OnInspectorGUI()
    {
        var obj = target as MoverScript;

        obj.working = EditorGUILayout.Toggle("Starts Active", obj.working);
        obj.moverMode = (MoverMode) EditorGUILayout.EnumPopup("Mover Mode", obj.moverMode);

        obj.speed = EditorGUILayout.FloatField("Speed", obj.speed);

        EditorGUILayout.BeginVertical("Box");

        switch (obj.moverMode)
        {
            

            case MoverMode.ROTATION:
                RotationMenu(obj);
                break;

            case MoverMode.CYCLE:
                CycleMenu(obj);
                break;

            default:
                break;

                
        }

        EditorGUILayout.EndVertical();

        lines = EditorGUILayout.Toggle("Show Lines", lines);

    }

    private void RotationMenu(MoverScript obj)
    {


        obj.rotationCenter = EditorGUILayout.Vector2Field("Rotation Center", obj.rotationCenter);
        lockRotationTransform = EditorGUILayout.Toggle("LOCK",lockRotationTransform);

        //Raio
        obj.radius = (obj.transform.position - obj.rotationCenter).magnitude;
        EditorGUILayout.FloatField("Radius", obj.radius);

        //Horario/AntiHorario
        EditorGUILayout.BeginHorizontal();
        antiHorario = EditorGUILayout.Toggle("C.Clockwise", antiHorario);
        horario = !antiHorario;
        horario = EditorGUILayout.Toggle("Clockwise", horario);
        antiHorario = !horario;
        obj.reverse = horario;
        EditorGUILayout.EndHorizontal();

        
    }

    private void CycleMenu(MoverScript obj)
    {

        string butonText;

        //Incia listas
        if (obj.points == null)
        {
            obj.points = new List<Vector3>();
            obj.points.Add(obj.transform.position);
        }
        if(lockCycleTransforms == null || lockCycleTransforms.Count < obj.points.Count){
            for(int i = 0; i<obj.points.Count; i++){
                lockCycleTransforms.Add(true);
            }
        }
        if(transformsDiference == null || transformsDiference.Count < obj.points.Count){
            for(int i = 0; i<obj.points.Count; i++){
                transformsDiference.Add(obj.transform.position - obj.points[i]);
            }
        }

        EditorGUILayout.BeginVertical();
        for (int i = 0; i < obj.points.Count; i++)
        {
            
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.BeginHorizontal();
            lockCycleTransforms[i] = EditorGUILayout.Toggle(lockCycleTransforms[i], GUILayout.Width(20));
            EditorGUILayout.TextArea(i.ToString() + ":", GUILayout.MaxWidth(25));
            obj.points[i] = EditorGUILayout.Vector2Field("", obj.points[i]);
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
            lockCycleTransforms.Add(true);
            transformsDiference.Add(Vector3.zero);
        }

        EditorGUILayout.EndVertical();
    }

    private void RotationOnScene(MoverScript obj){
        
        obj.rotationCenter = Handles.PositionHandle(obj.rotationCenter, Quaternion.identity);

        if (lockRotationTransform)
        {
            Vector3 newPos = new Vector3(radiusDiference.x + obj.rotationCenter.x, radiusDiference.y + obj.rotationCenter.y);
            obj.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
        }
        else
        {
            radiusDiference = obj.transform.position - obj.rotationCenter;
        }

        Handles.DrawWireDisc(obj.rotationCenter, Vector3.forward, obj.radius);

    }

    private void CycleOnScene(MoverScript obj){

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

                //Numera as handles
                Handles.Label(obj.points[i], i.ToString(), labelStyle);

                //Lock
                if(lockCycleTransforms[i]){
                    obj.points[i] = obj.transform.position - transformsDiference[i];
                }
                else{
                    transformsDiference[i] = obj.transform.position - obj.points[i];
                }

                //Desenha linha
                if (lines)
                {
                    if (i == obj.points.Count - 1)
                    {
                        Handles.DrawLine(obj.points[i], obj.points[0]);
                    }
                    else
                    {
                        Handles.DrawLine(obj.points[i], obj.points[i + 1]);
                    }
                }

            }

    }

    private void OnSceneGUI()
    {
        
        var obj = target as MoverScript;

        switch (obj.moverMode)
        {
            case MoverMode.ROTATION:
                RotationOnScene(obj);
                break;

            case MoverMode.CYCLE:
                CycleOnScene(obj);
                break;

            default:
                break;

                
        }

    }

}
