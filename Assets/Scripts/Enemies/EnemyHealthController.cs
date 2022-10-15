using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int m_MaxHealth = 100;
    public int m_CurrentHealth;
    [SerializeField] private GameObject m_BloodEffect;
    [SerializeField] private Slider m_HealthSlider;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        UpdateHealth(m_CurrentHealth);
    }

    public void DamageEnemy(int i_DamageAmount)
    {
        if(m_CurrentHealth > 0)
        {
            m_CurrentHealth -= i_DamageAmount;
            UpdateHealth(m_CurrentHealth);
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
        this.enabled = false;

        if (gameObject.name == "Flying_Eye_Enemy")
        {
            Destroy(m_HealthSlider);
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(int m_CurrentHealth)
    {
        m_HealthSlider.maxValue = m_MaxHealth;
        m_HealthSlider.value = m_CurrentHealth;
    }
}

