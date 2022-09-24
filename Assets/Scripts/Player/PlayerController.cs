using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ******************* Main Components *******************
    [SerializeField] private Rigidbody2D m_RigidBody;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private PlayerAbillityTracker m_PlayerAbillityTracker;

    [SerializeField] private float m_MoveSpeed = 8f;
    [SerializeField] private float m_JumpForce = 20f;

    [SerializeField] private Transform m_GroundCheckPoint;
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_IsOnGround;

    // ******************* Double Jump *******************
    private bool m_CanDoubleJump;

    // ******************* Attack Flags *******************
    private bool m_IsAttacking = false;
    private bool m_IsAirAttacking = false;
    private bool m_IsGroundAttacking = false;

    // ******************* Dash *******************
    [SerializeField] private float m_DashSpeed;
    [SerializeField] private float m_DashTime;
    private float m_DashCounter;
    private bool m_IsDashing = false;
    [SerializeField] private SpriteRenderer m_AfterImage;
    [SerializeField] private float m_AfterImageLifeTime;
    [SerializeField] private float m_TimeBetweenAfterImages;
    private float m_AfterImageCounter;
    [SerializeField] private Color m_AfterImageColor;
    [SerializeField] private float m_TimeToWaitAfterDashing;
    private float m_DashRechargeCounter;

    private void Start()
    {
        m_PlayerAbillityTracker = GetComponent<PlayerAbillityTracker>();
    }

    void Update()
    {
        if(m_DashRechargeCounter > 0)
        {
            m_DashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2") && m_PlayerAbillityTracker.m_CanDash) // dashing
            {
                m_Animator.SetTrigger("Dash");
                m_IsDashing = true;
                m_DashCounter = m_DashTime;
                showAfterImage();
            }
        }

        if(m_DashCounter > 0) // in the middle of a dash
        {
            m_DashCounter = m_DashCounter - Time.deltaTime;

            m_AfterImageCounter -= Time.deltaTime;
            if(m_AfterImageCounter <= 0)
            {
                showAfterImage();
            }

            m_DashRechargeCounter = m_TimeToWaitAfterDashing;
        }
        else
        {
            m_IsDashing = false;
        }

        updateIsAttacking();
        handleSpeed();
        handleMoveSideways();
        handleDirectionChange();
        checkIfOnTheGround();
        handleJumping();
        updateAnimationsParameters();
        updateCanDoubleJump();
    }

    private void updateIsAttacking()
    {
        m_IsAttacking = m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        updateIsAirAttacking();
        updateIsGroundAttacking();
    }

    private void updateIsAirAttacking()
    {
        m_IsAirAttacking = !m_IsOnGround && m_IsAttacking;
    }

    private void updateIsGroundAttacking()
    {
        m_IsGroundAttacking = m_IsOnGround && m_IsAttacking;
    }

    private void handleSpeed()
    {
        if (m_Animator.GetBool("IsCrouching") || m_IsGroundAttacking)
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
        if(m_IsDashing)
        {
            m_RigidBody.velocity = new Vector2(m_DashSpeed * transform.localScale.x, m_RigidBody.velocity.y);
        }
        else
        {
            m_RigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * m_MoveSpeed, m_RigidBody.velocity.y);
        }
    }

    private void handleDirectionChange()
    {
        if(!m_IsDashing)
        {
            if (m_RigidBody.velocity.x < 0)
            {
                transform.localScale = new Vector3(-(MathF.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
            }
            else if (m_RigidBody.velocity.x > 0)
            {
                transform.localScale = new Vector3((MathF.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
            }
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
            if(m_IsOnGround || (m_CanDoubleJump && m_PlayerAbillityTracker.m_CanDoubleJump))
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

    private void showAfterImage()
    {
        SpriteRenderer afterImage = Instantiate(m_AfterImage, transform.position, transform.rotation);

        afterImage.sprite = m_SpriteRenderer.sprite;
        afterImage.transform.localScale = transform.localScale;
        afterImage.color = m_AfterImageColor;
        Destroy(afterImage.gameObject, m_AfterImageLifeTime);
        m_AfterImageCounter = m_TimeBetweenAfterImages;
    }
}