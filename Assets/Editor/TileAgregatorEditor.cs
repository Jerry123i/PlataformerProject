using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColliderAgregator))]
public class TileAgregatorEditor : Editor {

    public override void OnInspectorGUI()
    {
        var obj = target as ColliderAgregator;

        base.OnInspectorGUI();

        if (GUILayout.Button("Full Agregate", GUILayout.Height(35)))
        {
            obj.myCollider = obj.gameObject.GetComponent<BoxCollider2D>();
            obj.gameObject.layer = LayerMask.NameToLayer("Tile");
            GetChildrensBox(obj);
            CenterOnChildren(obj);
            AddCollidersList(obj.myCollider, obj.childColiders);

            foreach (BoxCollider2D child in obj.childColiders)
            {

                if (child.gameObject.GetInstanceID() != obj.gameObject.GetInstanceID())
                {
                    child.enabled = false;
                }
            }

            obj.myCollider.usedByEffector = true;

        }

        if (GUILayout.Button("Get"))
        {
            GetChildrensBox(obj);
        }

        if (GUILayout.Button("Center"))
        {
            CenterOnChildren(obj);
        }

        if (GUILayout.Button("AgregateLayers"))
        {
            AddCollidersList(obj.myCollider, obj.childColiders);
        }

        if(GUILayout.Button("Toggle Children"))
        {
            foreach(BoxCollider2D child in obj.childColiders)
            {

                if(child.gameObject.GetInstanceID() !=  obj.gameObject.GetInstanceID())
                {
                    child.enabled = !child.enabled;
                }                
            }
        }
        


    }
    
    void CenterOnChildren(ColliderAgregator obj)
    {
        List<Vector3> childrenPlaces =  new List<Vector3>();

        foreach (BoxCollider2D child in obj.childColiders)
        {
            if (child.gameObject.GetInstanceID() != obj.gameObject.GetInstanceID())
            {
                childrenPlaces.Add(child.gameObject.transform.position);
            }
        }

        obj.transform.position = (GetAvarageFromList(childrenPlaces));

        for(int i = 0; i<childrenPlaces.Count; i++)
        {
            obj.childColiders[i + 1].transform.position = childrenPlaces[i];
        }

    }

    Vector3 GetAvarageFromList(List<Vector3> l)
    {
        Vector3 v;
        v = new Vector3(0, 0, 0);

        for(int i = 0; i < l.Count; i++)
        {
            v += l[i];
        }

        return (v / l.Count);
    }


    public void AddCollidersList(BoxCollider2D target, BoxCollider2D[] lista)
    {

        if (lista.Length == 1)
        {
            Debug.Log("Collider Agregator error. List too short");
        }

        target.size = new Vector2(lista[1].size.x, lista[1].size.y);
        target.offset = new Vector2(lista[1].offset.x, lista[1].offset.y);

        for (int i = 2; i < lista.Length; i++)
        {
            if (lista[i].GetComponent<PlatformEffector2D>() != null)
            {
                target.GetComponent<PlatformEffector2D>().useOneWay = lista[i].GetComponent<PlatformEffector2D>().useOneWay;


                if (lista[i].GetComponent<PlatformEffector2D>().useOneWay)
                {
                    target.GetComponent<PlatformEffector2D>().surfaceArc = lista[i].GetComponent<PlatformEffector2D>().surfaceArc;
                }
            }
            AddColliders(target, target, lista[i]);
        }

    }

    public void AddColliders(BoxCollider2D target, BoxCollider2D a, BoxCollider2D b)
    {

        target.size = new Vector2((a.size.x + b.size.x), (b.size.y));

        if (a.offset == b.offset)
        {
            // target.offset = new Vector2(a.offset.x, a.offset.y);
        }

        target.offset = new Vector2((a.offset.x + b.offset.x) / 2, ((a.offset.y + b.offset.y) / 2));
    }

    void GetChildrensBox(ColliderAgregator t)
    {
        t.childColiders = t.gameObject.transform.GetComponentsInChildren<BoxCollider2D>();

    }

}
