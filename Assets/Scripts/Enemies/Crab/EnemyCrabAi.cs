using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrabAi : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Transform[] m_PatrolPoints;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_TimeToWaitAtPoints;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private Rigidbody2D m_RigidBody;

    private float m_WaitCounter;
    private int m_CurrentPoint;

    public int CurrentPoint
    {
        get { return m_CurrentPoint; }
        set
        {
            if(value == m_PatrolPoints.Length)
            {
                m_CurrentPoint = 0;
            }
            else
            {
                m_CurrentPoint = value;
            }
        }
    }

    void Start()
    {
        m_WaitCounter = m_TimeToWaitAtPoints;

        foreach(Transform point in m_PatrolPoints)
        {
            point.SetParent(null);
        }
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.x - m_PatrolPoints[m_CurrentPoint].position.x) > .2f)
        {
            if(transform.position.x < m_PatrolPoints[m_CurrentPoint].position.x) // point is on the right
            {
                m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else // point is on the left
            {
                m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            if(transform.position.y < (m_PatrolPoints[CurrentPoint].position.y - .5f) && m_RigidBody.velocity.y < .1f)
            {
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_JumpForce);
            }
        }
        else // enemy got to the point
        {
            // stop moving:
            m_RigidBody.velocity = new Vector2(0f, m_RigidBody.velocity.y);

            // start counting down:
            m_WaitCounter -= Time.deltaTime;

            // if we finish counting:
            if(m_WaitCounter <= 0f)
            {
                m_WaitCounter = m_TimeToWaitAtPoints;
                CurrentPoint++;
            }
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(m_RigidBody.velocity.x));
    }
}

