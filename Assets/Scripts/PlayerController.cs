using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] private Rigidbody2D m_RigidBody;

    [Header("Main Settings")]
    [SerializeField] private float m_MoveSpeed = 8f;
    [SerializeField] private float m_JumpForce = 20f;

    [Header("Ground Settings")]
    [SerializeField] private Transform m_GroundCheckPoint;
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_IsOnGround;

    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    private bool m_CanDoubleJump;

    void Update()
    {
        handleSpeed();
        handleMoveSideways();
        handleDirectionChange();
        checkIfOnTheGround();
        handleJumping();
        updateAnimationsParameters();
        updateCanDoubleJump();
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
