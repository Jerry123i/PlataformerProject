using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ColliderAgregator : MonoBehaviour {

    
    public BoxCollider2D myCollider;
    public BoxCollider2D[] childColiders;

    private void Awake()
    {
        //myCollider = this.GetComponent<BoxCollider2D>();
        //childColiders = transform.GetComponentsInChildren<BoxCollider2D>();
        //AddCollidersList(myCollider, childColiders);

                
    }
    
    public void AddColliders(BoxCollider2D target,BoxCollider2D a, BoxCollider2D b)
    {
        
        target.size = new Vector2((a.size.x + b.size.x), (b.size.y));

        if(a.offset == b.offset)
        {
           // target.offset = new Vector2(a.offset.x, a.offset.y);
        }

        target.offset =  new Vector2 ((a.offset.x + b.offset.x) / 2, ((a.offset.y + b.offset.y) / 2));

                
    }

    public void AddCollidersList(BoxCollider2D target, BoxCollider2D[] lista)
    {
        
        if (lista.Length == 0)
        {
            Debug.Log("Collider Agregator error. List too short");            
        }

        target.size = new Vector2(lista[0].size.x, lista[0].size.y);
        target.offset = new Vector2(lista[0].offset.x, lista[0].offset.y);

        for (int i = 1; i<lista.Length; i++)
        {
            AddColliders(target, target, lista[i]);
        }

    }

}
