using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTrap : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField] private float m_CooldownTime;
    private float m_CooldownCounter;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_CooldownCounter = m_CooldownTime;
    }


    void Update()
    {
        m_CooldownCounter -= Time.deltaTime;

        if(m_CooldownCounter <= 0)
        {
            m_Animator.SetTrigger("Drop");
            m_CooldownCounter = m_CooldownTime;
        }
    }
}
