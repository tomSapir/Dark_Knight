using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_DistanceToOpen;
    private PlayerController m_Player;
    private bool m_PlayerExiting = false;

    [SerializeField] private Transform m_ExitPoint;
    [SerializeField] private float m_MovePlayerSpeed;

    [SerializeField] private string m_LevelToLoad;

    void Start()
    {
        m_Player = PlayerHealthController.m_Instance.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, m_Player.transform.position) < m_DistanceToOpen)
        {
            m_Animator.SetBool("DoorOpen", true);
        }
        else
        {
            m_Animator.SetBool("DoorOpen", false);
        }

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
        yield return new WaitForSeconds(1.5f);

        RespawnController.m_Instance.SetSpawn(m_ExitPoint.position);
        m_Player.CanMove = true;

        SceneManager.LoadScene(m_LevelToLoad);
    }
}
