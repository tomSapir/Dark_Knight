using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController m_Instance;

    void Awake()
    {
        m_Instance = this;
    }

    [SerializeField] private Slider m_BossHealthSlider;
    [SerializeField] private int m_CurrentHealth = 30;
    [SerializeField] private BossBattle m_Boss;

    void Start()
    {
        m_BossHealthSlider.maxValue = m_CurrentHealth;
        m_BossHealthSlider.value = m_CurrentHealth;
    }

    public void TakeDamage(int i_DamageAmount)
    {
        m_CurrentHealth -= i_DamageAmount;

        if(m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            m_Boss.EndBattle();
        }

        m_BossHealthSlider.value = m_CurrentHealth;
    }
}
