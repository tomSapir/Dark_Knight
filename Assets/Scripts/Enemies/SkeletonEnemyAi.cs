using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyAi : MonoBehaviour
{
    public float m_AttackDistance; // minimum distance to attack
    public float m_MoveSpeed;
    public float m_Timer; // timer for cooldown between attacks
    public Transform m_LeftLimit, m_RightLimit;
    [HideInInspector] public Transform m_Target;
    [HideInInspector] public bool m_IsPlayerInRange; // if player is in range
    public GameObject m_HotZone;
    public GameObject m_TriggerArea;

    private Animator m_Animator;
    private float m_DistanceFromPlayer;
    private bool m_IsInAttackMode;
    private bool m_IsInCooling; // if enemy is cooling after attack
    private float m_InitTimer;

    private void Awake()
    {
        SelectTarget();
        m_InitTimer = m_Timer; // store the initial value of the timer
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!m_IsInAttackMode)
        {
            move();
        }

        if(!IsInsideTheLimits() && !m_IsPlayerInRange 
            && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack1") 
            && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack2"))
        {
            SelectTarget();
        }

        if(m_IsPlayerInRange)
        {
            enemyLogic();
        }
    }

    public void SelectTarget()
    {
        float distanceToLeftLimit = Vector2.Distance(transform.position, m_LeftLimit.position);
        float distanceToRightLimit = Vector2.Distance(transform.position, m_RightLimit.position);

        if(distanceToLeftLimit > distanceToRightLimit)
        {
            m_Target = m_LeftLimit;
        }
        else
        {
            m_Target = m_RightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if(transform.position.x > m_Target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    private void enemyLogic()
    {
        m_DistanceFromPlayer = Vector2.Distance(transform.position, m_Target.position);
        if (m_DistanceFromPlayer > m_AttackDistance)
        {
            stopAttack();
        }
        else if(m_AttackDistance >= m_DistanceFromPlayer && m_IsInCooling == false)
        {
            attack();
        }

        if(m_IsInCooling)
        {
            coolDown();
            m_Animator.SetBool("Attack", false);
        }
    }

    private void move()
    {
        m_Animator.SetBool("CanWalk", true);
        if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack1") &&
            !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack2"))
        {
            Vector2 targetPosition = new Vector2(m_Target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_MoveSpeed * Time.deltaTime);
        }
    }

    private void attack()
    {
        m_Timer = m_InitTimer; // reset timer when player enter attack range
      
        m_Animator.SetBool("CanWalk", false);
        m_Animator.SetBool("Attack", true);
      
    }

    private void stopAttack()
    {
        m_IsInCooling = false;
        m_IsInAttackMode = false;
        m_Animator.SetBool("Attack", false);
    }

    private void coolDown()
    {
        m_Timer -= Time.deltaTime;

        if(m_Timer <= 0 && m_IsInCooling && m_IsInAttackMode)
        {
            m_IsInCooling = false;
            m_Timer = m_InitTimer;

        }
    }


    public void TriggerCooling()
    {
        m_IsInCooling = true;
    }


    private bool IsInsideTheLimits()
    {
        return transform.position.x > m_LeftLimit.position.x && transform.position.x < m_RightLimit.position.x;
    }
}
