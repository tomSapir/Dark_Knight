using System.Collections;
using UnityEngine;

public class WizardBossBattle : MonoBehaviour
{
    public enum eWizardState { Cooldown, Walking, Attacking }

    private CameraController m_Camera;
    private Transform m_Player;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_MoveSpeed;
    public Transform m_TheBoss;

    [SerializeField] private Transform m_CameraPosition;
    [SerializeField] private float m_CameraMovementSpeed;

    public eWizardState m_State;

    [SerializeField] private int m_Treshold1, m_Treshold2;

    [SerializeField] private float m_CooldownTime, m_AttackCooldownTime;
    private float m_CooldownCounter, m_AttackCooldownCounter;

    private Vector2 m_TargetPoint;

    [SerializeField] private int m_AmountOfAttacks;
    [SerializeField] private float m_DistanceFromPlayerToAttack = 4f;
    private int m_AttackCounter;
    private int m_AttackIndex = 1;

    void Start()
    {
        m_Player = GameObject.Find("Player").transform;
        m_Camera = FindObjectOfType<CameraController>();
        m_Camera.enabled = false;
        m_State = eWizardState.Cooldown;
        m_CooldownCounter = m_CooldownTime;
    }

    void Update()
    {
        m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, m_CameraPosition.position, m_CameraMovementSpeed * Time.deltaTime);

        if(WizardBossHealthController.m_Instance.m_CurrentHealth > m_Treshold1) // Phase 1
        {
            switch(m_State)
            {
                case eWizardState.Cooldown:
                    {
                        m_CooldownCounter -= Time.deltaTime;
                        if(m_CooldownCounter <= 0)
                        {
                            findNextTarget();
                            flipIfNeed();
                            m_State = eWizardState.Walking;
                        }

                        break;
                    }
                case eWizardState.Walking:
                    {
                        m_Animator.SetBool("Walk", true);
                        m_TheBoss.position = Vector2.MoveTowards(m_TheBoss.position, m_TargetPoint, m_MoveSpeed * Time.deltaTime);
                        if (Mathf.Abs(m_TheBoss.position.x - m_TargetPoint.x) < .1f)
                        {
                            m_Animator.SetBool("Walk", false);
                            m_State = eWizardState.Attacking;
                            m_AttackCounter = m_AmountOfAttacks;
                        }

                        break;
                    }
                case eWizardState.Attacking:
                    {
                        if(m_AttackCounter > 0)
                        {
                            m_AttackCooldownCounter -= Time.deltaTime;
                            if (m_AttackCooldownCounter <= 0)
                            {
                                m_AttackCounter--;
                                m_Animator.SetTrigger("Attack");
                                ChangeAttackIndex();
                                m_AttackCooldownCounter = m_AttackCooldownTime;
                            }
                        }
                        else
                        {
                            m_State = eWizardState.Cooldown;
                            m_CooldownCounter = m_CooldownTime;
                        }

                        break;
                    }
            }
        }
    }

    private void ChangeAttackIndex()
    {
        if(m_AttackIndex == 1)
        {
            m_AttackIndex = 2;
        }
        else
        {
            m_AttackIndex = 1;
        }

        m_Animator.SetInteger("AttackIndex", m_AttackIndex);
    }

    private void findNextTarget()
    {
        if (m_Player.transform.position.x > m_TheBoss.position.x)
        {
            m_TargetPoint = new Vector3(m_Player.position.x - m_DistanceFromPlayerToAttack, m_TheBoss.position.y);
        }
        else
        {
            m_TargetPoint = new Vector3(m_Player.position.x + m_DistanceFromPlayerToAttack, m_TheBoss.position.y);
        }
    }

    private void flipIfNeed()
    {
        if (m_TargetPoint.x > m_TheBoss.position.x)
        {
            m_TheBoss.localScale = new Vector3(Mathf.Abs(m_TheBoss.localScale.x), m_TheBoss.localScale.y, m_TheBoss.localScale.z);
        }
        else if (m_TargetPoint.x < m_TheBoss.position.x)
        {
            m_TheBoss.localScale = new Vector3(-Mathf.Abs(m_TheBoss.localScale.x), m_TheBoss.localScale.y, m_TheBoss.localScale.z);
        }
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
