using System.Collections;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyAi : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Header("Main Settings")]
    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_NextWayPointDistance = 3f;
    [SerializeField] private Transform m_EnemyGFX;
    [SerializeField] private Rigidbody2D m_RigidBody;

    [Header("Path Settings")]
    [SerializeField] private Path m_Path;
    [SerializeField] private int m_CurrentWayPoint = 0;
    [SerializeField] private bool m_ReachedEndOfPath = false;
    [SerializeField] private Seeker m_Seeker;

    [Header("Attack Settings")]
    [SerializeField] private float m_EnemyAttackCoolDown = 1f;
    [SerializeField] private int m_Damage = 10;
    [SerializeField] private bool m_PlayerInRange = false;
    [SerializeField] private bool m_CanAttack = true;
    [SerializeField] private int m_MaxAttackIndex;
    [SerializeField] private int m_AttackIndex = 1;

    public int AttackIndex
    {
        get
        {
            return m_AttackIndex;
        }
        set
        {
            if(value == m_MaxAttackIndex + 1)
            {
                m_AttackIndex = 1;
            }
            else if(value == 0)
            {
                m_AttackIndex = m_MaxAttackIndex;
            }
            else
            {
                m_AttackIndex = value;
            }
        }
    }

    void Start()
    {
        m_Seeker = GetComponent<Seeker>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        InvokeRepeating("updatePath", 0f, .5f);
    }

    void Update()
    {
        if(m_PlayerInRange && m_CanAttack)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_PlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_PlayerInRange = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        m_CanAttack = false;
        m_Animator.SetTrigger("Attack");
        m_Animator.SetInteger("AttackIndex", AttackIndex++);
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Player").GetComponent<PlayerController>().TakeDamage(m_Damage);
        yield return new WaitForSeconds(m_EnemyAttackCoolDown);
        m_CanAttack = true;
    }

    private void updatePath()
    {
        if(m_Seeker.IsDone())
        {
            m_Seeker.StartPath(m_RigidBody.position, m_Target.position, onPathComplete);
        }
    }

    private void onPathComplete(Path path)
    {
        if(!path.error)
        {
            m_Path = path;
            m_CurrentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(m_Path == null)
        {
            return;
        }    

        if(m_CurrentWayPoint >= m_Path.vectorPath.Count)
        {
            m_ReachedEndOfPath = true;
            return;
        }
        else
        {
            m_ReachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)m_Path.vectorPath[m_CurrentWayPoint] - m_RigidBody.position).normalized;
        Vector2 force = direction * m_Speed * Time.deltaTime;

        m_RigidBody.AddForce(force);

        float distance = Vector2.Distance(m_RigidBody.position, m_Path.vectorPath[m_CurrentWayPoint]);
        if(distance < m_NextWayPointDistance)
        {
            m_CurrentWayPoint++;
        }

        updateFlip();
    }

    private void updateFlip()
    {
        if (m_RigidBody.velocity.x >= 0.01f)
        {
            m_EnemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (m_RigidBody.velocity.x <= -0.01f)
        {
            m_EnemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
