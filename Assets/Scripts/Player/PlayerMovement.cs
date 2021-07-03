using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController2D controller;

    public Animator animator;

    public AudioSource jumpAudio;
    public AudioSource stepsAudio;

    public float speed = 40f;
    float hMove = 0f;
    float vMove = 0f;

    bool jump = false;
    bool steps = false;

    bool stepsAudioRunning = false;

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("speed", Mathf.Abs(hMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

            // Sound
            // AudioSource audio = gameObject.GetComponent<AudioSource>();
            jumpAudio.Play();
        }

        if (hMove != 0 && vMove == 0)
        {
            steps = true;
        }
        else
        {
            steps = false;
        }

        if (steps && !stepsAudioRunning)
        {
            stepsAudio.Play();
            stepsAudioRunning = true;
        }

        if (!steps)
        {
            stepsAudio.Stop();
            stepsAudioRunning = false;
        }
    }

    void FixedUpdate()
    {
        // Move Player
        controller.Move(hMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
