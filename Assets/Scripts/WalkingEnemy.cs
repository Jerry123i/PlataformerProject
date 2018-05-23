using UnityEngine;
using System.Collections;

public class WalkingEnemy : EnemyScript
{
    public float speed;
    private Transform feeler;
    public Vector2 direction;
    public LayerMask layerMask;


    private void Awake()
    {
        feeler = transform.GetChild(0);
    }

    void Update()
    {
        RaycastHit2D RHDown = Physics2D.Raycast(feeler.position, Vector3.down, 0.6f, layerMask);
        RaycastHit2D RHLeft = Physics2D.Raycast(new Vector3(feeler.position.x, feeler.position.y + 0.05f, feeler.position.z), Vector3.left, 0.6f, layerMask);
        

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        //Verifica buracos
        if (RHDown.collider == null)
        {
            TurnAround();
        }

        //Verifica paredes
        else if(RHLeft.collider != null)
        {   
            TurnAround();
        }

    }

    private void TurnAround()
    {
        transform.Rotate(Vector3.up * 180.0f);
    }

    public override void BeforeDeath()
    {
        base.BeforeDeath();
        speed = 0;

    }

}
