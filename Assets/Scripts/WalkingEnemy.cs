using UnityEngine;
using System.Collections;

public class WalkingEnemy : EnemyScript
{
    public float speed;
    private Transform feeler;
    public Vector2 direction;

    private void Awake()
    {
        feeler = transform.GetChild(0);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        //Verifica buracos
        if (Physics2D.Raycast(feeler.position, Vector3.down, 0.2f).collider == null)
        {
            TurnAround();
        }

        //Verifica paredes
        else if(Physics2D.Raycast(new Vector3(feeler.position.x, feeler.position.y + 0.1f, feeler.position.z), Vector3.left, 0.2f).collider != null)
        {
            TurnAround();
        }

    }

    private void TurnAround()
    {
        transform.Rotate(Vector3.up * 180.0f);
    }
}
