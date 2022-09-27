using System.Collections;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    [SerializeField] private int m_DamageToPlayer;
    private GroundPatrolEnemyAi m_EnemyParent;

    void Start()
    {
        m_EnemyParent = GetComponentInParent<GroundPatrolEnemyAi>();
    }

    void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.gameObject.CompareTag("Player"))
        {
            i_Other.gameObject.GetComponentInParent<PlayerHealthController>().TakeDamage(15);
        }
    }
}
