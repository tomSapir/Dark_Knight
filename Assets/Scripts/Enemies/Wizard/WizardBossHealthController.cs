using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardBossHealthController : MonoBehaviour
{
    public static WizardBossHealthController m_Instance;
    public GameObject m_GotHitEffect;

    void Awake()
    {
        m_Instance = this;
    }

    [SerializeField] private Slider m_BossHealthSlider;
    public int m_CurrentHealth = 60;
    [SerializeField] private WizardBossBattle m_TheBoss;
    [SerializeField] private Animator m_Animator;

    void Start()
    {
        m_BossHealthSlider.maxValue = m_CurrentHealth;
        m_BossHealthSlider.value = m_CurrentHealth;
    }

    public void TakeDamage(int i_DamageAmount)
    {
        if (m_TheBoss.m_State == WizardBossBattle.eWizardState.Cooldown)
        {
            Instantiate(m_GotHitEffect, m_TheBoss.m_TheBoss.position, m_TheBoss.m_TheBoss.rotation);
            m_Animator.SetTrigger("Take_Hit");
            m_CurrentHealth -= i_DamageAmount;

            if (m_CurrentHealth <= 0)
            {
                m_CurrentHealth = 0;
                m_TheBoss.EndBattle();
            }

            m_BossHealthSlider.value = m_CurrentHealth;
        }
    }
}
