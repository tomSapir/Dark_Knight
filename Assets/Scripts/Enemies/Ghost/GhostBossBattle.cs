using UnityEngine;

public class GhostBossBattle : MonoBehaviour
{
    public Animator m_Animator;
    public Transform m_Boss;
    private GameObject m_Player;

    [SerializeField] private CameraController m_Camera;
    [SerializeField] private Transform m_CameraPosition;
    [SerializeField] private float m_CameraSpeed;

    [SerializeField] private int m_TreshHold1, m_TreshHold2;

    [SerializeField] private float m_ActiveTime, m_FadeoutTime, m_InactiveTime;
    private float m_ActiveCounter, m_FadoutCounter, m_InactiveCounter;

    [SerializeField] private Transform[] m_SpawnPoints;
    [SerializeField] private float m_MoveSpeed;
    private Transform m_TargetPoint;

    [SerializeField] private float m_TimeBetweenShots1, m_TimeBetweenShots2;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private Transform m_ShotPoint;
    private float m_ShotCounter;

    [SerializeField] private GameObject m_WinObjects;
    private bool m_BattleEnded;

    void Start()
    {
        m_Player = GameObject.Find("Player");
        AudioManager.m_Instance.PlayBossMusic();
        m_Camera = FindObjectOfType<CameraController>();
        m_Camera.enabled = false;
        m_ActiveCounter = m_ActiveTime;
        m_ShotCounter = m_TimeBetweenShots1;
    }

    void Update()
    {
        m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, m_CameraPosition.position, m_CameraSpeed * Time.deltaTime);
        flipIfNeed();

        if (!m_BattleEnded)
        {
            if (GhostBossHealthController.m_Instance.m_CurrentHealth > m_TreshHold1) // in phase 1
            {
                handleUpdatePhase1();
            }
            else
            {
                handleUpdatePhase2();
            }
        }
        else
        {
            handleUpdateOnBattleEnd();
        }
    }

    private void flipIfNeed()
    {
        if (m_Boss.position.x < m_Player.transform.position.x)
        {
            m_Boss.localScale = new Vector3(-Mathf.Abs(m_Boss.localScale.x), m_Boss.localScale.y, m_Boss.localScale.z);
        }
        else
        {
            m_Boss.localScale = new Vector3(Mathf.Abs(m_Boss.localScale.x), m_Boss.localScale.y, m_Boss.localScale.z);
        }
    }

    private void handleUpdatePhase1()
    {
        if (m_ActiveCounter > 0)
        {
            m_ActiveCounter -= Time.deltaTime;
            if (m_ActiveCounter <= 0)
            {
                m_FadoutCounter = m_FadeoutTime;
                m_Animator.SetTrigger("Vanish");
            }

            m_ShotCounter -= Time.deltaTime;
            if (m_ShotCounter <= 0)
            {
                m_ShotCounter = m_TimeBetweenShots1;
                Instantiate(m_Bullet, m_ShotPoint.position, Quaternion.identity);
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
                m_Boss.gameObject.SetActive(true);
                m_ActiveCounter = m_ActiveTime;
            }

            m_ShotCounter = m_TimeBetweenShots1;
        }
    }

    private void handleUpdatePhase2()
    {
        if (m_TargetPoint == null)
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

                m_ShotCounter -= Time.deltaTime;
                if (m_ShotCounter <= 0)
                {
                    if (GhostBossHealthController.m_Instance.m_CurrentHealth > m_TreshHold2)
                    {
                        m_ShotCounter = m_TimeBetweenShots1;
                    }
                    else
                    {
                        m_ShotCounter = m_TimeBetweenShots2;
                    }

                    Instantiate(m_Bullet, m_ShotPoint.position, Quaternion.identity);
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

                    while (m_TargetPoint.position == m_Boss.position)
                    {
                        m_TargetPoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)];
                    }

                    m_Boss.gameObject.SetActive(true);
                    if (GhostBossHealthController.m_Instance.m_CurrentHealth > m_TreshHold2)
                    {
                        m_ShotCounter = m_TimeBetweenShots1;
                    }
                    else
                    {
                        m_ShotCounter = m_TimeBetweenShots2;
                    }
                }
            }
        }
    }

    private void handleUpdateOnBattleEnd()
    {
        m_FadoutCounter -= Time.deltaTime;
        if (m_FadoutCounter < 0)
        {
            if (m_WinObjects != null)
            {
                m_WinObjects.SetActive(true);
                m_WinObjects.transform.SetParent(null);
            }

            m_Camera.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void EndBattle()
    {
        AudioManager.m_Instance.PlaySFX(0);
        AudioManager.m_Instance.PlayLevelMusic();

        m_BattleEnded = true;
        m_FadoutCounter = m_FadeoutTime;
        m_Animator.SetTrigger("Vanish");
        m_Boss.GetComponent<Collider2D>().enabled = false;

        BossBullet[] bulletsOnScene = FindObjectsOfType<BossBullet>();
        if(bulletsOnScene.Length > 0)
        {
            foreach(BossBullet bullet in bulletsOnScene)
            {
                Destroy(bullet.gameObject);
            }
        }
    }
}
