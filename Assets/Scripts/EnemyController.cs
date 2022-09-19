using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Header("Health")]
    [SerializeField] private int m_MaxHealth = 100;
    [SerializeField] private int m_CurrentHealth;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void TakeDamage(int i_Damage)
    {
        m_CurrentHealth -= i_Damage;
        m_Animator.SetTrigger("Hurt");

        if(m_CurrentHealth <= 0)
        {
            die();
        }
    }


    private void die()
    {
        m_Animator.SetBool("IsDead", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        this.enabled = false;
    }
}
