using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAi : MonoBehaviour
{
    public Transform m_RayCast;
    public LayerMask m_RaycastMask;
    public float m_RayCastLength;
    public float m_AttackDistance; // minimum distance to attack
    public float m_MoveSpeed;
    public float m_Timer; // timer for cooldown between attacks

    private RaycastHit2D m_Hit;
    private GameObject m_Target;
    private Animator m_Animator;
    private float m_DistanceFromPlayer;
    private bool m_IsInAttackMode;
    private bool m_IsPlayerInRange; // if player is in range
    private bool m_IsInCooling; // if enemy is cooling after attack
    private float m_InitTimer;


    private void Awake()
    {
        m_InitTimer = m_Timer; // store the initial value of the timer
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(m_IsPlayerInRange)
        {
            m_Hit = Physics2D.Raycast(m_RayCast.position, Vector2.left, m_RayCastLength, m_RaycastMask);
            RaycastDebugger();
        }

        // when player is detected:
        if(m_Hit.collider != null)
        {
            enemyLogic();
        }
        else if(m_Hit.collider == null)
        {
            m_IsPlayerInRange = false;
        }

        if(m_IsPlayerInRange == false)
        {
            m_Animator.SetBool("CanWalk", false);
            stopAttack();
        }
    }

    // to check if player entered the trigger zone:
    private void OnTriggerEnter2D(Collider2D other)
    {
       
       if(other.gameObject.tag == "Player")
        {
            m_Target = other.gameObject;
            m_IsPlayerInRange = true;
        }
    }

    private void enemyLogic()
    {
        Debug.Log("Distance from player: " + m_DistanceFromPlayer + "    ||   Attack distance: " + m_AttackDistance);
        m_DistanceFromPlayer = Vector2.Distance(transform.position, m_Target.transform.position);
        if (m_DistanceFromPlayer > m_AttackDistance)
        {
            move();
            stopAttack();
        }
        else if(m_AttackDistance >= m_DistanceFromPlayer && m_IsInCooling == false)
        {
            attack();
        }

        if(m_IsInCooling)
        {
            coolDown();
            m_Animator.SetBool("Attack", false);
        }
    }

    private void move()
    {
        m_Animator.SetBool("CanWalk", true);
        if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack1") &&
            !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack2"))
        {
            Vector2 targetPosition = new Vector2(m_Target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_MoveSpeed * Time.deltaTime);
        }
    }

    private void attack()
    {
        m_Timer = m_InitTimer; // reset timer when player enter attack range
        m_IsInAttackMode = true;

        m_Animator.SetBool("CanWalk", false);
        m_Animator.SetBool("Attack", true);
    }

    private void stopAttack()
    {
        m_IsInCooling = false;
        m_IsInAttackMode = false;
        m_Animator.SetBool("Attack", false);
    }

    private void coolDown()
    {
        m_Timer -= Time.deltaTime;

        if(m_Timer <= 0 && m_IsInCooling && m_IsInAttackMode)
        {
            m_IsInCooling = false;
            m_Timer = m_InitTimer;
        }
    }

    void RaycastDebugger()
    {
        if(m_DistanceFromPlayer >= m_AttackDistance)
        {
            Debug.DrawRay(m_RayCast.position, Vector2.left * m_RayCastLength, Color.red);
        }
        else if(m_AttackDistance > m_DistanceFromPlayer)
        {
            Debug.DrawRay(m_RayCast.position, Vector2.left * m_RayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        m_IsInCooling = true;
    }

}
