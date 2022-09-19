using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Header("Attack Settings")]
    [SerializeField] private Transform m_AttackPoint;
    [SerializeField] private float m_AttackRange = .5f;
    [SerializeField] private LayerMask m_EnemyLayers;

    [Header("Ground Attacks")]
    [SerializeField] private int m_GroundAttackIndex = 1;

    [Header("Air Attacks")]
    [SerializeField] private int m_AirAttackIndex = 1;

    private int m_AttackDamage = 40;

    public int GroundAttackIndex
    {
        get
        {
            return m_GroundAttackIndex;
        }
        set
        {
            if(value == 4)
            {
                m_GroundAttackIndex = 1;
            }
            else if(value == 0)
            {
                m_GroundAttackIndex = 3;
            }
            else
            {
                m_GroundAttackIndex = value;
            }
        }
    }

    public int AirAttackIndex
    {
        get
        {
            return m_AirAttackIndex;
        }
        set
        {
            if (value == 4)
            {
                m_AirAttackIndex = 1;
            }
            else if (value == 0)
            {
                m_AirAttackIndex = 3;
            }
            else
            {
                m_AirAttackIndex = value;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
            if (!m_Animator.GetBool("IsOnGround"))
            {
                m_Animator.SetInteger("AirAttackIndex", AirAttackIndex++);
            }
            else
            {
                m_Animator.SetInteger("GroundAttackIndex", GroundAttackIndex++);
            }
        }
    }

    private void Attack()
    {

        m_Animator.SetTrigger("Attack");

        // detect enemies in the range of the attack:
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(m_AttackPoint.position, m_AttackRange, m_EnemyLayers);

        // damage them:
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(m_AttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(m_AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(m_AttackPoint.position, m_AttackRange);
    }
}
