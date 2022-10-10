using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardBossHealthController : MonoBehaviour
{
    public static WizardBossHealthController m_Instance;

    void Awake()
    {
        m_Instance = this;
    }

    [SerializeField] private Slider m_BossHealthSlider;
    [SerializeField] private int m_CurrentHealth = 60;
    [SerializeField] private WizardBossBattle m_TheBoss;

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
            m_TheBoss.EndBattle();
        }

        m_BossHealthSlider.value = m_CurrentHealth;
    }
}
