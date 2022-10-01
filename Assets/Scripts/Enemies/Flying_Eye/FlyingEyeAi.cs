using System.Collections;
using UnityEngine;
using Pathfinding;

public class FlyingEyeAi : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Speed = 200f;
    [SerializeField] private float m_NextWayPointDistance = 1.5f;
    [SerializeField] private Transform m_EnemyGFX;
    private Rigidbody2D m_RigidBody;

    [SerializeField] private int m_CurrentWayPoint = 0;
    private Path m_Path;
    private bool m_ReachedEndOfPath = false;
    private Seeker m_Seeker;

    [SerializeField] private float m_EnemyAttackCoolDown = 1f;
    [SerializeField] private int m_Damage = 10;
    [SerializeField] private bool m_PlayerInRange = false;
    [SerializeField] private bool m_CanAttack = true;

    [SerializeField] private GameObject m_BloodEffect;
    [SerializeField] private Transform m_BitePoint;

    void Start()
    {
        m_Target = GameObject.Find("Player").transform;
        m_Animator = gameObject.GetComponentInChildren<Animator>();
        m_Seeker = GetComponent<Seeker>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        InvokeRepeating("updatePath", 0f, .5f);
    }

    void Update()
    {
        if (m_PlayerInRange && m_CanAttack && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Flying_Eye_Take_Hit"))
        {
            StartCoroutine(AttackPlayer());
        }

        checkIfDied();
    }

    private void checkIfDied()
    {
        if(transform.childCount == 1)
        {
            Destroy(gameObject);
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
        yield return new WaitForSeconds(0.3f);
        AudioManager.m_Instance.PlaySFX(12);
        GameObject.Find("Player").GetComponent<PlayerHealthController>().TakeDamage(m_Damage);
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

    private void onPathComplete(Path i_Path)
    {
        if(!i_Path.error)
        {
            m_Path = i_Path;
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

        updateFlip(force);
    }

    private void updateFlip(Vector2 i_Force)
    {
        if (i_Force.x >= 0.001f)
        {
            m_EnemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (i_Force.x <= -0.001f)
        {
            m_EnemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
