using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController2D controller;

    public float speed = 40f;
    float hMove = 0f;

    bool jump = false;


    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Move Player
        controller.Move(hMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
