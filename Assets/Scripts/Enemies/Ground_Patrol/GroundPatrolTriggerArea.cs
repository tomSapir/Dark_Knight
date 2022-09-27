using UnityEngine;

public class GroundPatrolTriggerArea : MonoBehaviour
{
    private GroundPatrolEnemyAi m_EnemyParent;

    void Awake()
    {
        m_EnemyParent = GetComponentInParent<GroundPatrolEnemyAi>();
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            m_EnemyParent.SetTarget(i_Other.transform);
            m_EnemyParent.SetPlayerInRange(true);
            m_EnemyParent.SetHotZoneActive(true);
        }
    }
}
