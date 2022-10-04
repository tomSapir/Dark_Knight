using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float m_DistanceToOpen;
    [SerializeField] private Transform m_ExitPoint;
    [SerializeField] private float m_MovePlayerSpeed;
    [SerializeField] private string m_LevelToLoad;
    private PlayerController m_Player;
    private bool m_PlayerExiting = false;

    void Start()
    {
        m_Player = PlayerHealthController.m_Instance.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(m_PlayerExiting)
        {
            m_Player.transform.position = Vector3.MoveTowards(m_Player.transform.position, m_ExitPoint.position, m_MovePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player")
        {
            if(!m_PlayerExiting)
            {
                m_Player.CanMove = false;
                StartCoroutine(UseDoorCorutine());
            }
        }
    }

    IEnumerator UseDoorCorutine()
    {
        m_PlayerExiting = true;
        UIController.m_Instance.StartFadeToBlack();
        yield return new WaitForSeconds(2f);
        RespawnController.m_Instance.SetSpawn(m_ExitPoint.position);
        m_Player.CanMove = true;
        UIController.m_Instance.StartFadeFromBlack();
        saveNewLevelData();
        SceneManager.LoadScene(m_LevelToLoad);
    }

    private void saveNewLevelData()
    {
        PlayerPrefs.SetString("ContinueLevel", m_LevelToLoad);
        PlayerPrefs.SetFloat("PosX", m_ExitPoint.position.x);
        PlayerPrefs.SetFloat("PosY", m_ExitPoint.position.y);
        PlayerPrefs.SetFloat("PosZ", m_ExitPoint.position.z);
    }
}
