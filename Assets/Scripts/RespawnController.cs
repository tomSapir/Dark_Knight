using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController m_Instance;

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 m_RespawnPoint;
    [SerializeField] private float m_WaitToRespawn;

    private GameObject m_Player;

    void Start()
    {
        m_Player = PlayerHealthController.m_Instance.gameObject;

        m_RespawnPoint = m_Player.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCorutine());
    }

    IEnumerator RespawnCorutine()
    {
        m_Player.GetComponent<PlayerController>().CanMove = false;
        yield return new WaitForSeconds(1f);
        m_Player.SetActive(false);

        yield return new WaitForSeconds(m_WaitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        m_Player.transform.position = m_RespawnPoint;

        m_Player.SetActive(true);
        m_Player.GetComponent<PlayerController>().CanMove = true;

        PlayerHealthController.m_Instance.FillHealth();
    }

    public void SetSpawn(Vector3 i_NewPosition)
    {
        m_RespawnPoint = i_NewPosition;
    }
}
