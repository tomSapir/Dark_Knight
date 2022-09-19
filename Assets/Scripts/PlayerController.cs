using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;

    public float m_MoveSpeed = 8f;
    public float m_JumpForce = 20f;

    public Transform m_GroundCheckPoint;
    private bool m_IsOnGround;
    public LayerMask m_WhatIsGround;

    public Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleSpeed();
        handleMoveSideways();
        handleDirectionChange();
        checkIfOnTheGround();
        handleJumping();
        updateAnimationsParameters();
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
        if (Input.GetButtonDown("Jump") && m_IsOnGround)
        {
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_JumpForce);
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
}
