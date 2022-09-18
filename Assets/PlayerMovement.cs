using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static readonly string sr_AnimatorCrouchingParameterName = "IsCrouching";
    public static readonly string sr_AnimatorWalkingParameterName = "IsWalking";
    public static readonly string sr_AnimatorRunningParameterName = "IsRunning";
    public static readonly string sr_AnimatorJumpingParameterName = "IsJumping";

    public CharacterController2D m_Controller;

    public float m_WalkSpeed = 15f;
    public float m_RunSpeed = 25f;
    public float m_CurrentSpeed;
    private float m_HorizontalMove = 0f;
    private bool m_Jump = false;
    public bool Crouch { get; set; } = false;

    public Animator m_Animator;

    void Start()
    {
        m_CurrentSpeed = m_WalkSpeed;
    }

    void Update()
    {
        setCurrentSpeed();
        m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_CurrentSpeed;

        checkIfWalking();
        checkIfRunning();
        checkIfJumping();
        checkIfCrouching();
    }

    private void FixedUpdate()
    {
        m_Controller.Move(m_HorizontalMove * Time.deltaTime, Crouch, m_Jump);
        m_Jump = false;
    }

    private void setCurrentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_CurrentSpeed = m_RunSpeed;
        }
        else
        {
            m_CurrentSpeed = m_WalkSpeed;
        }
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
            m_WalkSpeed = 40f;
        }
        else
        {
            m_WalkSpeed = 20f;
        }
    }

    private void checkIfJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(m_Animator.GetBool(sr_AnimatorRunningParameterName))
            {
                m_WalkSpeed = 40f;
            }

            m_Jump = true;
            m_Animator.SetBool(sr_AnimatorJumpingParameterName, true);
        }
    }

    private void checkIfCrouching()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            Crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Crouch = false;
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
