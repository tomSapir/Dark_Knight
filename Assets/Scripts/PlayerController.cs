using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_RigidBody;

    [SerializeField] private float m_MoveSpeed = 8f;
    [SerializeField] private float m_JumpForce = 20f;

    [SerializeField] private Transform m_GroundCheckPoint;
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_IsOnGround;

    [SerializeField] private Animator m_Animator;

    private bool m_CanDoubleJump;

    [SerializeField] private float m_WallJumpTime = 0.2f;
    [SerializeField] private float m_WallSlideSpeed = 0.3f;
    [SerializeField] private float m_WallDistance = 0.5f;
    private bool m_IsWallSliding = false;
    private RaycastHit2D m_WallCheckHit;
    private float m_JumpTime;

    void Update()
    {
        handleSpeed();
        handleMoveSideways();
        handleDirectionChange();
        checkIfOnTheGround();
        handleJumping();
        updateAnimationsParameters();
        updateCanDoubleJump();


        // ********************************** wall jump **********************************
        float horizontalInput = Input.GetAxis("Horizontal");


        if (horizontalInput > 0)
        {
            m_WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(m_WallDistance, 0), m_WhatIsGround);

            Debug.DrawRay(transform.position, new Vector2(m_WallDistance, 0), Color.blue);

        }
        else
        {
            m_WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-m_WallDistance, 0), m_WhatIsGround);

            Debug.DrawRay(transform.position, new Vector2(-m_WallDistance, 0), Color.blue);
        }


        if(m_WallCheckHit && !m_IsOnGround && horizontalInput != 0)
        {
            m_IsWallSliding = true;
            m_JumpTime = Time.time + m_WallJumpTime;
        }
        else if(m_JumpTime < Time.time)
        {
            m_IsWallSliding = false;
        }

        if(m_IsWallSliding)
        {
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, Mathf.Clamp(m_RigidBody.velocity.y, m_WallSlideSpeed, float.MaxValue));
        }


        // ********************************************************************************
    }

    private void handleSpeed()
    {
        if (m_Animator.GetBool("IsCrouching"))
        {
            m_MoveSpeed = 0f;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift)) // if running
            {
                m_MoveSpeed = 15f;
            }
            else
            {
                m_MoveSpeed = 8f;
            }
        }
    }

    private void handleMoveSideways()
    {
        m_RigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * m_MoveSpeed, m_RigidBody.velocity.y);
    }

    private void handleDirectionChange()
    {
        if (m_RigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (m_RigidBody.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void checkIfOnTheGround()
    {
        m_IsOnGround = Physics2D.OverlapCircle(m_GroundCheckPoint.position, .2f, m_WhatIsGround);
    }

    private void handleJumping()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(m_IsOnGround || m_CanDoubleJump)
            {
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_JumpForce);
                m_CanDoubleJump = !m_CanDoubleJump;
            }
        }

        if(Input.GetButtonUp("Jump") && m_RigidBody.velocity.y > 0f) // for short time pressing space (smaller jump)
        {
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_RigidBody.velocity.y * 0.5f);
        }
    }

    private void updateAnimationsParameters()
    {
        m_Animator.SetBool("IsOnGround", m_IsOnGround);
        m_Animator.SetFloat("Speed", Mathf.Abs(m_RigidBody.velocity.x));

        if(m_MoveSpeed == 15f)
        {
            m_Animator.SetBool("IsRunning", true);
        }
        else 
        {
            m_Animator.SetBool("IsRunning", false);
        }


        if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && m_IsOnGround)
        {
            m_Animator.SetBool("IsCrouching", true);
        }
        else
        {
            m_Animator.SetBool("IsCrouching", false);
        }
    }

    private void updateCanDoubleJump()
    {
        if (m_IsOnGround && !Input.GetButton("Jump"))
        {
            m_CanDoubleJump = false;
        }
    }
}
