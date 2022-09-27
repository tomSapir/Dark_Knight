using System.Collections.Generic;
using UnityEngine;

public class GroundPatrolEnemyAi : MonoBehaviour
{
    [SerializeField] private float m_AttackDistance; // minimum distance to attack
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Transform m_LeftLimit, m_RightLimit;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_BloodEffect;
    [SerializeField] private List<string> m_AttackAnimationsNames;

    public Transform m_Target;
    public bool m_IsPlayerInRange; // if player is in range
    public GameObject m_HotZone;
    public GameObject m_TriggerArea;
    public bool m_IsInAttackMode;

    private float m_Timer;
    private float m_InitTimer;
    private float m_DistanceFromPlayer;
    private bool m_IsInCooling;
    private EnemyHealthController m_EnemyHealthController;

    void Awake()
    {
        SelectTarget();
        m_InitTimer = m_Timer; 
        m_Animator = GetComponent<Animator>();
        m_EnemyHealthController = GetComponent<EnemyHealthController>();
    }


    void Update()
    {
        if (m_EnemyHealthController.m_CurrentHealth > 0)
        {
            if (!m_IsInAttackMode)
            {
                move();
            }

            if(!IsInsideTheLimits() && !m_IsPlayerInRange && !IsInAttackAnimation())
            {
                    SelectTarget();
            }

            if (m_IsPlayerInRange)
            {
                enemyLogic();
            }
        }

        Flip();
    }

    public void SelectTarget()
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

    public void Flip()
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

    private void enemyLogic()
    {
        m_DistanceFromPlayer = Vector2.Distance(transform.position, m_Target.position);
        if (m_DistanceFromPlayer > m_AttackDistance)
        {
            stopAttack();
        }
        else if (m_AttackDistance >= m_DistanceFromPlayer && m_IsInCooling == false)
        {
            attack();
        }

        if (m_IsInCooling)
        {
            coolDown();
            m_Animator.SetBool("Attack", false);
        }
    }

    private void move()
    {
        m_Animator.SetBool("CanWalk", true);
        if (!IsInAttackAnimation())
        {
            Vector2 targetPosition = new Vector2(m_Target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_MoveSpeed * Time.deltaTime);
        }
    }

    public bool IsInAttackAnimation()
    {
        foreach (string attackAnimationName in m_AttackAnimationsNames)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName))
            {
                return true;
            }
        }

        return false;
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
        if (m_Timer <= 0 && m_IsInCooling && m_IsInAttackMode)
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

    public void SetTarget(Transform i_Target)
    {
        m_Target = i_Target;
    }

    public void SetPlayerInRange(bool i_IsInRange)
    {
        m_IsPlayerInRange = i_IsInRange;
    }

    public void SetHotZoneActive(bool i_IsActive)
    {
        m_HotZone.SetActive(i_IsActive);
    }
}
