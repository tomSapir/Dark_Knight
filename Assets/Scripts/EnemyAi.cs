using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAi : MonoBehaviour
{

    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_NextWayPointDistance = 3f;


    [SerializeField] private Transform m_EnemyGFX;

    private Path m_Path;
    private int m_CurrentWayPoint = 0;
    private bool m_ReachedEndOfPath = false;

    private Seeker m_Seeker;
    private Rigidbody2D m_RigidBody;

    void Start()
    {
        m_Seeker = GetComponent<Seeker>();
        m_RigidBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("updatePath", 0f, .5f);
    }

    private void updatePath()
    {
        if(m_Seeker.IsDone())
        {
            m_Seeker.StartPath(m_RigidBody.position, m_Target.position, onPathComplete);
        }
    }

    private void onPathComplete(Path p)
    {
        if(!p.error)
        {
            m_Path = p;
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

        //m_EnemyGFX
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
