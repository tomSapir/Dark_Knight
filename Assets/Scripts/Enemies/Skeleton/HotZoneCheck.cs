using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private SkeletonEnemyAi m_EnemyParent;
    private bool m_PlayerInRange;
    private Animator m_Animator;

    void Awake()
    {
        m_EnemyParent = GetComponentInParent<SkeletonEnemyAi>();
        m_Animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if(m_PlayerInRange && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack1") 
            && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack2"))
        {
            m_EnemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.gameObject.CompareTag("Player"))
        {
            m_PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D i_Other)
    {
        if (i_Other.gameObject.CompareTag("Player"))
        {
            m_PlayerInRange = false;
            gameObject.SetActive(false);
            m_EnemyParent.m_TriggerArea.SetActive(true);
            m_EnemyParent.m_IsPlayerInRange = false;
            m_EnemyParent.SelectTarget();
        }
    }
}

