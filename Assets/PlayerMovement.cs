using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly string sr_AnimatorCrouchingParameterName = "IsCrouching";
    private static readonly string sr_AnimatorWalkingParameterName = "IsWalking";
    private static readonly string sr_AnimatorRunningParameterName = "IsRunning";
    private static readonly string sr_AnimatorJumpingParameterName = "IsJumping";

    public CharacterController2D m_Controller;

    public float m_RunSpeed = 20f;
    private float m_HorizontalMove = 0f;
    private bool  m_Jump = false;
    private bool  m_Crouch = false;

    public Animator m_Animator;

    void Update()
    {
        m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
        m_Animator.SetFloat("Speed", Mathf.Abs(m_HorizontalMove));

        checkIfWalking();
        checkIfRunning();
        checkIfJumping();
        checkIfCrouching();
    }

    private void FixedUpdate()
    {
        m_Controller.Move(m_HorizontalMove * Time.deltaTime, m_Crouch, m_Jump);
        m_Jump = false;
    }

    private void checkIfWalking()
    {
        bool isWalking = !Input.GetKey(KeyCode.LeftShift) && m_HorizontalMove != 0 && !m_Animator.GetBool(sr_AnimatorJumpingParameterName);

        m_Animator.SetBool(sr_AnimatorWalkingParameterName, isWalking);
    }

    private void checkIfRunning()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && m_HorizontalMove != 0 && !m_Animator.GetBool(sr_AnimatorJumpingParameterName);

        m_Animator.SetBool(sr_AnimatorRunningParameterName, isRunning);
        if (isRunning)
        {
            m_RunSpeed = 40f;
        }
        else
        {
            m_RunSpeed = 20f;
        }
    }

    private void checkIfJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_Jump = true;
            m_Animator.SetBool(sr_AnimatorJumpingParameterName, true);
        }
    }

    private void checkIfCrouching()
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
        m_Animator.SetBool(sr_AnimatorJumpingParameterName, false);
    }

    public void OnPlayerCrouching(bool i_IsCrouching)
    {
        m_Animator.SetBool(sr_AnimatorCrouchingParameterName, i_IsCrouching);
    }
}
