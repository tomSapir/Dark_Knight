using System;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int m_MaxHealth = 100;
    [SerializeField] private int m_CurrentHealth;
    [SerializeField] private GameObject m_DeathEffect;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void DamageEnemy(int i_DamageAmount)
    {
        m_CurrentHealth -= i_DamageAmount;

        try
        {
            m_Animator.SetTrigger("Hurt");
        }
        catch(Exception exp)
        {
            Debug.Log(exp.Message);
        }

        if(m_CurrentHealth <= 0)
        {
            if(m_DeathEffect != null)
            {
                Instantiate(m_DeathEffect, transform.position, transform.rotation);
            }

            die();
        }
    }

    private void die()
    {
        m_Animator.SetBool("IsDead", true);
    }
}

