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
        obj.moverType = (MoverType)EditorGUILayout.EnumPopup("Mover Type", obj.moverType);

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

            case MoverMode.ONCE:
                OnceMenu(obj);
                break;

            default:
                break;

                
        }

        EditorGUILayout.EndVertical();

        lines = EditorGUILayout.Toggle("Show Lines", lines);

    }

    protected void RotationMenu(MoverScript obj)
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

    protected void OnceMenu(MoverScript obj)
    {
        string butonText;

        //Incia listas
        if (obj.points == null)
        {
            obj.points = new List<Vector3>();
            obj.points.Add(obj.transform.position);
        }
        if (lockCycleTransforms == null || lockCycleTransforms.Count < obj.points.Count)
        {

            if (lockCycleTransforms == null)
            {
                lockCycleTransforms = new List<bool>();
            }
            for (int i = 0; i < obj.points.Count; i++)
            {
                lockCycleTransforms.Add(false);
            }
        }
        if (transformsDiference == null || transformsDiference.Count < obj.points.Count)
        {
            if (transformsDiference == null)
            {
                transformsDiference = new List<Vector3>();
            }
            for (int i = 0; i < obj.points.Count; i++)
            {
                transformsDiference.Add(obj.transform.position - obj.points[i]);
            }
        }

        EditorGUILayout.BeginVertical();
        for (int i = 0; i < obj.points.Count; i++)
        {

            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.BeginHorizontal();
            lockCycleTransforms[i] = EditorGUILayout.Toggle(lockCycleTransforms[i], GUILayout.Width(20));


            EditorGUILayout.TextField(i.ToString(), GUILayout.MaxWidth(20));
            

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
                transformsDiference.RemoveAt(i);
                lockCycleTransforms.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Point"))
        {
            obj.points.Add(obj.transform.position);
            lockCycleTransforms.Add(false);
            transformsDiference.Add(Vector3.zero);
        }

        EditorGUILayout.EndVertical();

    }

    protected void CycleMenu(MoverScript obj)
    {

        string butonText;

        //Incia listas
        if (obj.points == null)
        {
            obj.points = new List<Vector3>();
            obj.points.Add(obj.transform.position);
        }
        if(lockCycleTransforms == null || lockCycleTransforms.Count < obj.points.Count){

            if(lockCycleTransforms == null)
            {
                lockCycleTransforms = new List<bool>();
            }
            for(int i = 0; i<obj.points.Count; i++){
                lockCycleTransforms.Add(false);
            }
        }
        if(transformsDiference == null || transformsDiference.Count < obj.points.Count){
            if(transformsDiference == null)
            {
                transformsDiference = new List<Vector3>();
            }
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

            //Teleporta para o ponto
            if (GUILayout.Button(i.ToString()))
            {
                ToggleAllLocks(false);
                obj.transform.position = new Vector3(obj.points[i].x, obj.points[i].y, obj.points[i].z);
            }

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
                transformsDiference.RemoveAt(i);
                lockCycleTransforms.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Point"))
        {
            obj.points.Add(obj.transform.position);
            lockCycleTransforms.Add(false);
            transformsDiference.Add(Vector3.zero);
        }

        EditorGUILayout.EndVertical();
    }

    protected void RotationOnScene(MoverScript obj){
        
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

    protected void OnceOnScene(MoverScript obj)
    {
        if (obj.points == null)
        {
            obj.points = new List<Vector3>();
            obj.points.Add(obj.transform.position);
        }
        if (lockCycleTransforms == null || lockCycleTransforms.Count < obj.points.Count)
        {

            if (lockCycleTransforms == null)
            {
                lockCycleTransforms = new List<bool>();
            }
            for (int i = 0; i < obj.points.Count; i++)
            {
                lockCycleTransforms.Add(false);
            }
        }

        if (transformsDiference == null || transformsDiference.Count < obj.points.Count)
        {
            if (transformsDiference == null)
            {
                transformsDiference = new List<Vector3>();
            }

            for (int i = 0; i < obj.points.Count; i++)
            {
                transformsDiference.Add(obj.transform.position - obj.points[i]);
            }
        }


        for (int i = 0; i < obj.points.Count; i++)
        {
            obj.points[i] = Handles.PositionHandle(obj.points[i], Quaternion.identity);

            var labelStyle = new GUIStyle();
            labelStyle.fontSize = 18;

            if (obj.targetN == i)
            {
                labelStyle.fontSize = 25;
                labelStyle.fontStyle = FontStyle.BoldAndItalic;
            }

            //Numera as handles
            Handles.Label(obj.points[i], i.ToString(), labelStyle);

            //Lock
            if (lockCycleTransforms[i])
            {
                obj.points[i] = obj.transform.position - transformsDiference[i];
            }
            else
            {
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

    protected void CycleOnScene(MoverScript obj){

        if (obj.points == null)
        {
            obj.points = new List<Vector3>();
            obj.points.Add(obj.transform.position);
        }
        if (lockCycleTransforms == null || lockCycleTransforms.Count < obj.points.Count)
        {

            if (lockCycleTransforms == null)
            {
                lockCycleTransforms = new List<bool>();
            }
            for (int i = 0; i < obj.points.Count; i++)
            {
                lockCycleTransforms.Add(false);
            }
        }

        if (transformsDiference == null || transformsDiference.Count < obj.points.Count)
        {
            if(transformsDiference == null)
            {
                transformsDiference = new List<Vector3>();
            }

            for (int i = 0; i < obj.points.Count; i++)
            {
                transformsDiference.Add(obj.transform.position - obj.points[i]);
            }
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

    protected void OnSceneGUI()
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

            case MoverMode.ONCE:
                OnceOnScene(obj);
                break;

            default:
                break;                
        }
    }

    protected void ToggleAllLocks(bool x)
    {
        for(int i = 0; i < lockCycleTransforms.Count; i++)
        {
            lockCycleTransforms[i] = x;
        }
    }

}
