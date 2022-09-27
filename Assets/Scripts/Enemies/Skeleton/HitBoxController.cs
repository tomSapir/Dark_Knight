using System.Collections;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    [SerializeField] private int m_DamageToPlayer;
    private MushroomAi m_MushroomAi;
    private SkeletonEnemyAi m_SkeletonEnemyAi;

    void Start()
    {
        m_MushroomAi = GetComponentInParent<MushroomAi>();
        m_SkeletonEnemyAi = GetComponentInParent<SkeletonEnemyAi>();
    }

    void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (i_Other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackPlayer());
            i_Other.gameObject.GetComponentInParent<PlayerHealthController>().TakeDamage(15);
        }
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Player").GetComponent<PlayerHealthController>().TakeDamage(m_DamageToPlayer);
    }
}
