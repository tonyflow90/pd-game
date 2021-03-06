using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public AudioSource flipSound;
    public float Speed = 1;

    public float Distance = 1;

    public float SpeedMax = 20;

    public bool changeSpeed = false;
    public float changeSpeedTime = 5;
    private float csTime = 2;
    public float changeSpeedMin = -1;
    public float changeSpeedMax = 1;

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


    void playFlipSound()
    {
        if (flipSound)
            flipSound.Play();
    }
    void Update()
    {
        // if (changeSpeed)
        // {
        //     csTime -= Time.deltaTime;
        //     if (csTime < 0)
        //     {
        //         csTime = changeSpeedTime;
        //         System.Random random = new System.Random();
        //         Speed += (float)(random.NextDouble() * (changeSpeedMax - changeSpeedMin) + changeSpeedMin);
        //         if (Speed > SpeedMax)
        //             Speed = changeSpeedMax;

        //         if (Speed < 0)
        //             Speed = 0;
        //     }
        // }

        CurrentPosition = gameObject.transform.position;
        if (MaxDistance <= CurrentPosition.x)
        {
            MoveRight = false;
            playFlipSound();

        }
        if (StartPosition.x >= CurrentPosition.x)
        {
            MoveRight = true;
            playFlipSound();
        }

        if (MoveRight)
        {
            transform.Translate(Time.deltaTime * Speed, 0, 0);
            transform.localScale = new Vector2(Scale.x, Scale.y);
        }
        else
        {
            transform.Translate(-1 * Time.deltaTime * Speed, 0, 0);
            transform.localScale = new Vector2(-1 * Scale.x, Scale.y);
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