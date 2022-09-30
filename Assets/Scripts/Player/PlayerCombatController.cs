using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Header("Attack Settings")]
    [SerializeField] private Transform m_AttackPoint;
    [SerializeField] private float m_AttackRange = .5f;
    [SerializeField] private LayerMask m_EnemyLayers;
    [SerializeField] private LayerMask m_DestructibleLayers;
    [SerializeField] private int m_AttackDamage = 20;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left click on mouse
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(m_AttackPoint.position, m_AttackRange, m_EnemyLayers);

        m_Animator.SetTrigger("Attack");
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Boss")
            {
                BossHealthController.m_Instance.TakeDamage(2);
            }
            else
            {
                EnemyHealthController enemyHealthController = enemy.GetComponent<EnemyHealthController>();
                if (enemyHealthController == null)
                {
                    enemy.GetComponentInParent<EnemyHealthController>().DamageEnemy(m_AttackDamage);
                }
                else
                {
                    enemyHealthController.DamageEnemy(m_AttackDamage);
                }

            }

            AudioManager.m_Instance.PlaySFXAdjusted(15);
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
