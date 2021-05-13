using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController2D controller;

    public Animator animator;

    public float speed = 40f;
    float hMove = 0f;

    bool jump = false;

    bool jump_event = false;

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("speed", Mathf.Abs(hMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            // animator.SetBool("jump", true);
        }

    }

    // public void OnLanding() 
    // {
    //     jump_event = animator.GetBool("jump");
    //     Debug.Log("is Landing! Jump: " + jump_event);
    //     if(jump_event) {
    //         animator.SetBool("jump", false);
    //     } else {
    //         animator.SetBool("jump", true);
    //     }
    // }

    void FixedUpdate()
    {
        // Move Player
        controller.Move(hMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
