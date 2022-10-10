using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_RigidBody;
    [SerializeField] private float m_Speed = 2f;
    [SerializeField] private bool m_IsHorizontal;
    [SerializeField] private bool m_HitTrigger;
    [SerializeField] private bool m_IsMovingUp;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(!m_IsHorizontal) // vertical
        {
            // Moving up
            if(m_IsMovingUp && !m_HitTrigger)
            {
                m_RigidBody.velocity = Vector2.up * m_Speed;
            }

            // Moving down
            if(!m_IsMovingUp && !m_HitTrigger)
            {
                m_RigidBody.velocity = Vector2.down * m_Speed;
            }
        }

        if (m_IsHorizontal) // horizontal
        {
            // Moving right
            if (m_IsMovingUp && !m_HitTrigger)
            {
                m_RigidBody.velocity = Vector2.right * m_Speed;
            }

            // Moving left
            if (!m_IsMovingUp && !m_HitTrigger)
            {
                m_RigidBody.velocity = Vector2.left * m_Speed;
            }
        }
    }

    void ChangeDirection()
    {
        m_IsMovingUp = !m_IsMovingUp;
        m_HitTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.gameObject.tag == "Elevator Trigger")
        {
            m_HitTrigger = true;
            m_RigidBody.velocity = Vector2.zero;
            Invoke("ChangeDirection", 3);
        }
    }
}
