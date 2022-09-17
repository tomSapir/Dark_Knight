using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D m_Controller;

    public float m_RunSpeed = 30f;

    private float m_HorizontalMove = 0f;
    private bool  m_Jump = false;
    private bool  m_Crouch = false;

    public Animator m_Animator;

    void Update()
    {
        m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
        m_Animator.SetFloat("Speed", Mathf.Abs(m_HorizontalMove));

        checkIfNeedToJump();
        checkIfNeedToCrouch();
    }

    private void FixedUpdate()
    {
        m_Controller.Move(m_HorizontalMove * Time.deltaTime, m_Crouch, m_Jump);
        m_Jump = false;
    }

    private void checkIfNeedToJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_Jump = true;
            m_Animator.SetBool("IsJumping", true);
        }
    }

    private void checkIfNeedToCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            m_Crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            m_Crouch = false;
        }
    }

    public void OnPlayerLanding()
    {
        m_Animator.SetBool("IsJumping", false);
    }

    public void OnPlayerCrouching(bool i_IsCrouching)
    {
        m_Animator.SetBool("IsCrouching", i_IsCrouching);
    }
}
