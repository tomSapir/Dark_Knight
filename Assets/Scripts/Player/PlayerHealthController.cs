using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int m_MaxHealth = 100;
    [SerializeField] private int m_CurrentHealth;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        UIController.m_Instance.UpdateHealth(m_CurrentHealth, m_MaxHealth);
    }

    public void TakeDamage(int i_Damage)
    {
        UIController.m_Instance.UpdateHealth(m_CurrentHealth, m_MaxHealth);

        m_CurrentHealth -= i_Damage;
        m_Animator.SetTrigger("Hurt");
        if (m_CurrentHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        throw new NotImplementedException();
    }
}
