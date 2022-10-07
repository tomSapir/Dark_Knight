using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator m_Animator;
    [SerializeField] private Rigidbody2D m_RigidBody;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private PlayerAbillityTracker m_PlayerAbillityTracker;

    [SerializeField] private float m_MoveSpeed = 4f;
    [SerializeField] private float m_JumpForce = 15f;

    [SerializeField] private Transform m_GroundCheckPoint;
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_IsOnGround;
    private bool m_IsFallingDown;

    private bool m_CanDoubleJump;
    [SerializeField] private float m_FallingDownTreshold = -8f;

    private bool m_IsAttacking = false;
    private bool m_IsAirAttacking = false;
    private bool m_IsGroundAttacking = false;

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

    [SerializeField] private float m_TrampolineLaunchForce;


    public bool CanMove { get; set; }

    private void Start()
    {
        m_PlayerAbillityTracker = GetComponent<PlayerAbillityTracker>();
        CanMove = true;
    }

    void Update()
    {
        if(CanMove && Time.timeScale != 0f)
        {
            updateIsFallingDown();
            handleDash();
            updateIsAttacking();
            handleSpeed();
            handleMoveSideways();
            handleDirectionChange();
            checkIfOnTheGround();
            handleJumping();
            updateAnimationsParameters();
            updateCanDoubleJump();
        }
        else
        {
            m_RigidBody.velocity = Vector2.zero;
        }
    }

    private void updateIsFallingDown()
    {
        if(!m_IsOnGround && m_RigidBody.velocity.y < m_FallingDownTreshold)
        {
            m_IsFallingDown = true;
        }
        else
        {
            m_IsFallingDown = false;
        }

        m_Animator.SetBool("IsFallingDown", m_IsFallingDown);
    }

    private void handleDash()
    {
        // if we are in the middle of cooling from last dash:
        if (m_DashRechargeCounter > 0)
        {
            m_DashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2") && m_PlayerAbillityTracker.m_CanDash)
            {
                dash();
            }
        }

        if (m_DashCounter > 0) // in the middle of a dash
        {
            m_DashCounter = m_DashCounter - Time.deltaTime;
            m_AfterImageCounter -= Time.deltaTime;
            if (m_AfterImageCounter <= 0)
            {
                showAfterImage();
            }

            m_DashRechargeCounter = m_TimeToWaitAfterDashing;
        }
        else
        {
            m_IsDashing = false;
        }
    }

    private void dash()
    {
        m_Animator.SetTrigger("Dash");
        m_IsDashing = true;
        m_DashCounter = m_DashTime;
        showAfterImage();
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
                m_MoveSpeed = 10f;
            }
            else
            {
                m_MoveSpeed = 4f;
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

        if(Input.GetKey(KeyCode.LeftShift) && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Crouch") 
            && !m_IsDashing && m_RigidBody.velocity.x != 0f)
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

    public void OnCollisionEnter2D(Collision2D i_Other)
    {
        if(i_Other.gameObject.tag == "Trampoline")
        {
            m_RigidBody.velocity = Vector2.up * m_TrampolineLaunchForce;
        }
    }
}
