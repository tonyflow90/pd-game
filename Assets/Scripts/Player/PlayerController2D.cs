using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    public GameManager gameManager;

    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;            // Whether or not the player is grounded.
    private bool isJumping = false;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public Animator animator;

    public AudioSource jumpAudio;
    public AudioSource stepsAudio;

    public AudioSource deathAudio;
    public AudioSource victoryAudio;

    public float volume = .3f;
    public float speed = 40f;
    float hMove = 0f;

    bool spawned = false;
    bool death = false;
    bool victory = false;
    bool jump = false;
    bool steps = false;

    bool stepsAudioRunning = false;

    void Update()
    {
        if (gameManager.CheckCompleteGame() && !victory)
        {
            StartCoroutine(Victory());
        }

        if (!death && !victory)
        {

            hMove = Input.GetAxisRaw("Horizontal") * speed;

            animator.SetFloat("speed", Mathf.Abs(hMove));

            animator.SetBool("spawned", true);

            if (spawned)
            {
                spawned = true;
                // animator.SetBool("spawned", spawned);
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;

                animator.SetBool("jump", jump);

                // Sound
                // AudioSource audio = gameObject.GetComponent<AudioSource>();
                jumpAudio.Play();
                stepsAudio.Stop();
                stepsAudioRunning = false;
            }

            if (!isJumping)
                animator.SetBool("jump", jump);

            if (hMove != 0 && !isJumping)
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
        else
        {
            hMove = 0;
        }

    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        // StartAllAudio();

        death = false;
        victory = false;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    isJumping = false;
                    OnLandEvent.Invoke();
                }
            }
        }
        Move(hMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }


    public void Move(float move, bool crouch, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            isJumping = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private AudioSource[] allAudioSources;

    void StartAllAudio()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    void StopAllAudio()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            // audioSource.volume = 0;
            if (audioSource)
                audioSource.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        bool enemyHit = false;

        if (hitInfo.name.Contains("Enemy") || hitInfo.name.Contains("Deadzone"))
        {
            enemyHit = true;
        }

        if (!death && enemyHit)
        {
            StartCoroutine(Death());
        }

    }

    IEnumerator Death()
    {
        death = true;
        StopAllAudio();
        animator.SetBool("death", death);

        // Sound
        deathAudio.Play();

        // Destroy(hitInfo);
        float animationTime = (float)animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        yield return new WaitForSeconds(2);
        gameManager.GameEnd();
    }

    IEnumerator Victory()
    {
        victory = true;
        StopAllAudio();
        animator.SetBool("victory", victory);

        // Sound
        victoryAudio.Play();

        float animationTime = (float)animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        yield return new WaitForSeconds(2);
        gameManager.LevelComplete();
    }

}
