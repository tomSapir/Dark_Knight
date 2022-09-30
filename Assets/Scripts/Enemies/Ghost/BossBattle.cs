using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private CameraController m_Camera;
    [SerializeField] private Transform m_CameraPosition;
    [SerializeField] private float m_CameraSpeed;

    [SerializeField] private int m_TreshHold1, m_TreshHold2;

    [SerializeField] private float m_ActiveTime, m_FadeoutTime, m_InactiveTime;
    private float m_ActiveCounter, m_FadoutCounter, m_InactiveCounter;

    [SerializeField] private Transform[] m_SpawnPoints;
    private Transform m_TargetPoint;
    [SerializeField] private float m_MoveSpeed;

    public Animator m_Animator;
    public Transform m_Boss;

    void Start()
    {
        m_Camera = FindObjectOfType<CameraController>();
        m_Camera.enabled = false;

        m_ActiveCounter = m_ActiveTime;
    }

    void Update()
    {
        m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, m_CameraPosition.position, m_CameraSpeed * Time.deltaTime);

        if(BossHealthController.m_Instance.m_CurrentHealth > m_TreshHold1) // in phase 1
        {
            if(m_ActiveCounter > 0)
            {
                m_ActiveCounter -= Time.deltaTime;
                if(m_ActiveCounter <= 0)
                {
                    m_FadoutCounter = m_FadeoutTime;
                    m_Animator.SetTrigger("Vanish");
                }
            }
            else if(m_FadoutCounter > 0)
            {
                m_FadoutCounter -= Time.deltaTime;
                if(m_FadoutCounter <= 0)
                {
                    m_Boss.gameObject.SetActive(false);
                    m_InactiveCounter = m_InactiveTime;
                }
            }
            else if(m_InactiveCounter > 0)
            {
                m_InactiveCounter -= Time.deltaTime;
                if(m_InactiveCounter <= 0)
                {
                    m_Boss.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
                    m_Boss.gameObject.SetActive(true);
                    m_ActiveCounter = m_ActiveTime;
                }
            }
        }
        else
        {
            if(m_TargetPoint == null)
            {
                m_TargetPoint = m_Boss;
                m_FadoutCounter = m_FadeoutTime;
                m_Animator.SetTrigger("Vanish");
            }
            else
            {
                if (Vector3.Distance(m_Boss.position, m_TargetPoint.position) > .02f)
                {
                    m_Boss.position = Vector3.MoveTowards(m_Boss.position, m_TargetPoint.position, m_MoveSpeed * Time.deltaTime);

                    if (Vector3.Distance(m_Boss.position, m_TargetPoint.position) <= .02f)
                    {
                        m_FadoutCounter = m_FadeoutTime;
                        m_Animator.SetTrigger("Vanish");
                    }
                }
                else if (m_FadoutCounter > 0)
                {
                    m_FadoutCounter -= Time.deltaTime;
                    if (m_FadoutCounter <= 0)
                    {
                        m_Boss.gameObject.SetActive(false);
                        m_InactiveCounter = m_InactiveTime;
                    }
                }
                else if (m_InactiveCounter > 0)
                {
                    m_InactiveCounter -= Time.deltaTime;
                    if (m_InactiveCounter <= 0)
                    {
                        m_Boss.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;

                        m_TargetPoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)];

                        int whileBreaker = 0;
                        while(m_TargetPoint.position == m_Boss.position && whileBreaker < 100)
                        {
                            m_TargetPoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)];
                            whileBreaker++;
                        }

                        m_Boss.gameObject.SetActive(true);
                        
                    }
                }
            }
        }
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
