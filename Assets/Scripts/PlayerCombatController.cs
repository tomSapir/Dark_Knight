using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Header("Attack Settings")]
    [SerializeField] private Transform m_AttackPoint;
    [SerializeField] private float m_AttackRange = .5f;
    [SerializeField] private LayerMask m_EnemyLayers;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // play attack animation:
        m_Animator.SetTrigger("Attack");

        // detect enemies in the range of the attack:
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(m_AttackPoint.position, m_AttackRange, m_EnemyLayers);

        // damage them:
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
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
