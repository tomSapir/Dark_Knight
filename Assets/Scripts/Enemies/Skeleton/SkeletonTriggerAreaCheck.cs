using UnityEngine;

public class SkeletonTriggerAreaCheck : MonoBehaviour
{
    private SkeletonEnemyAi m_EnemyParent;

    void Awake()
    {
        m_EnemyParent = GetComponentInParent<SkeletonEnemyAi>();
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            m_EnemyParent.m_Target = i_Other.transform;
            m_EnemyParent.m_IsPlayerInRange = true;
            m_EnemyParent.m_HotZone.SetActive(true);
        }
    }
}
