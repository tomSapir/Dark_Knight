using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemyAi : MonoBehaviour
{
    private Animator m_Animator;

    [SerializeField] private Transform m_LeftLimit, m_RightLimit;

    [HideInInspector] public Transform m_Target;
    [HideInInspector] public bool m_IsPlayerInRange; // if player is in range

    public GameObject m_HotZone;
    public GameObject m_TriggerArea;

    private float m_DistanceFromPlayer;

    [SerializeField] private float m_AttackDistance; // minimum distance to attack
    [SerializeField] private float m_MoveSpeed;
    private bool m_IsInAttackMode;

    void Awake()
    {
        SelectTarget();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!m_IsInAttackMode)
        {
            move();
        }

        if (!IsInsideTheLimits() && !m_IsPlayerInRange)
        {
            SelectTarget();
        }

        if (m_IsPlayerInRange)
        {
            enemyLogic();
        }
    }

    private void enemyLogic()
    {
        throw new NotImplementedException();
    }

    private bool IsInsideTheLimits()
    {
        return transform.position.x > m_LeftLimit.position.x && transform.position.x < m_RightLimit.position.x;
    }

    private void move()
    {
        m_Animator.SetTrigger("Patrolling");

        Vector2 targetPosition = new Vector2(m_Target.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_MoveSpeed * Time.deltaTime);
    }

    private void SelectTarget()
    {
        float distanceToLeftLimit = Vector2.Distance(transform.position, m_LeftLimit.position);
        float distanceToRightLimit = Vector2.Distance(transform.position, m_RightLimit.position);

        if (distanceToLeftLimit > distanceToRightLimit)
        {
            m_Target = m_LeftLimit;
        }
        else
        {
            m_Target = m_RightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > m_Target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }


}
