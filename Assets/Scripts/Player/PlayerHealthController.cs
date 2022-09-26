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
    [SerializeField] private int m_MaxHealth = 100;
    [SerializeField] private int m_CurrentHealth;
    [SerializeField] private GameObject m_BloodEffect;

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
        Instantiate(m_BloodEffect, transform.position, transform.rotation);
        if (m_CurrentHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Debug.Log("m_Instance = " + RespawnController.m_Instance + "  in PlayerHealthController");
        RespawnController.m_Instance.Respawn();
    }

    public void FillHealth()
    {
        m_CurrentHealth = m_MaxHealth;
        UIController.m_Instance.UpdateHealth(m_CurrentHealth, m_MaxHealth);
    }
}
