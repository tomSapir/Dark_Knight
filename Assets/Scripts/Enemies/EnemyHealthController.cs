using System;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int m_MaxHealth = 100;
    public int m_CurrentHealth;
    [SerializeField] private GameObject m_BloodEffect;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void DamageEnemy(int i_DamageAmount)
    {
        if(m_CurrentHealth > 0)
        {
            m_CurrentHealth -= i_DamageAmount;
            Instantiate(m_BloodEffect, transform.position, transform.rotation);

            try
            {
                m_Animator.SetTrigger("Hurt");
            }
            catch (Exception exp)
            {
                Debug.Log(exp.Message);
            }

            if (m_CurrentHealth <= 0)
            {
                die();
            }
        }
    }

    private void die()
    {
        m_Animator.SetBool("IsDead", true);
    }
}

