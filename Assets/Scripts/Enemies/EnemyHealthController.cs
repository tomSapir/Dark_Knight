using System;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int m_MaxHealth = 100;
    [SerializeField] private int m_CurrentHealth;

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
            die();
        }
    }

    private void die()
    {
        m_Animator.SetBool("IsDead", true);

        // play animation one time
        // fall down
        // dont move sideways
        // when on the ground destroy
    }
}

