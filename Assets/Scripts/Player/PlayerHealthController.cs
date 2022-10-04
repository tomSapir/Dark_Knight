using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController m_Instance;

    void Awake()
    {
        if(m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_BloodEffect;
    public int m_MaxHealth = 100;
    public int m_CurrentHealth;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void TakeDamage(int i_Damage)
    {
        if(m_CurrentHealth > 0)
        {
            UIController.m_Instance.UpdateHealth(m_CurrentHealth, m_MaxHealth);
            m_CurrentHealth -= i_Damage;
            m_Animator.SetTrigger("Hurt");
            Instantiate(m_BloodEffect, transform.position, transform.rotation);
            if (m_CurrentHealth <= 0)
            {
                die();
            }
        }
    }

    private void die()
    {
        AudioManager.m_Instance.PlaySFX(8);
        m_Animator.SetTrigger("Die");
        RespawnController.m_Instance.Respawn();
    }

    public void FillHealth()
    {
        m_CurrentHealth = m_MaxHealth;
        UIController.m_Instance.UpdateHealth(m_CurrentHealth, m_MaxHealth);
    }

    public void HealPlayer(int i_HealAmount)
    {
        m_CurrentHealth += i_HealAmount;
        if(m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }

        UIController.m_Instance.UpdateHealth(m_CurrentHealth, m_MaxHealth);
    }
}
