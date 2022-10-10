using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController m_Instance;
    public GameObject m_GotHitEffect;

    void Awake()
    {
        m_Instance = this;
    }

    [SerializeField] private Slider m_BossHealthSlider;
    public int m_CurrentHealth = 30;
    [SerializeField] private GhostBossBattle m_BossBattle;

    void Start()
    {
        m_BossHealthSlider.maxValue = m_CurrentHealth;
        m_BossHealthSlider.value = m_CurrentHealth;
    }

    public void TakeDamage(int i_DamageAmount)
    {
        Instantiate(m_GotHitEffect, m_BossBattle.m_Boss.position, m_BossBattle.m_Boss.rotation);
        m_CurrentHealth -= i_DamageAmount;

        if(m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            m_BossBattle.EndBattle();
        }

        m_BossHealthSlider.value = m_CurrentHealth;
    }
}
