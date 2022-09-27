using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private GroundPatrolEnemyAi m_EnemyParent;
    private bool m_PlayerInRange;
    private Animator m_Animator;

    void Awake()
    {
        m_EnemyParent = GetComponentInParent<GroundPatrolEnemyAi>();
        m_Animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if(m_PlayerInRange && !m_EnemyParent.IsInAttackAnimation())
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

