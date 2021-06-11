using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public float Speed = 1;

    public float Distance = 1;

    public bool MoveRight = true;
    private bool Flip = false;

    private Vector3 Scale;
    private Vector3 StartPosition;

    private Vector3 CurrentPosition;
    private float MaxDistance = 1;

    void Start()
    {
        Scale = gameObject.transform.localScale;
        StartPosition = gameObject.transform.position;
        CurrentPosition = gameObject.transform.position;
        MaxDistance = StartPosition.x + Distance;
    }
    void Update()
    {
        CurrentPosition = gameObject.transform.position;
        if (MaxDistance <= CurrentPosition.x)
        {
            MoveRight = false;
       }
        if (StartPosition.x >= CurrentPosition.x)
        {
            MoveRight = true;
        }

        if (MoveRight)
        {
            transform.Translate(Time.deltaTime * Speed, 0, 0);
            transform.localScale = new Vector2(Scale.x,Scale.y);
        }
        else
        {
            transform.Translate(-1 * Time.deltaTime * Speed, 0, 0);
            transform.localScale = new Vector2(-1*Scale.x,Scale.y);
        }

        // if(Flip) {
        //     Flip = false;
        //     Debug.Log("Flipped");
        // }
    }

    void FixedUpdate()
    {
        // if (MaxDistance < CurrentPosition.x)
        // {
        //     transform.Rotate(0, 180, 0);
        // }
        // if (StartPosition.x > CurrentPosition.x)
        // {
        //     transform.Rotate(0, 0, 0);
        // }
    }
}