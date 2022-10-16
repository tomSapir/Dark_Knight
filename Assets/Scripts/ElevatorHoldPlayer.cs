using UnityEngine;

public class ElevatorHoldPlayer : MonoBehaviour
{
    [SerializeField] private bool m_PlayerOnPlatform = false;
    private GameObject m_Player;
    private Vector3 m_PrevPosition;

    private void Start()
    {
        m_Player = GameObject.Find("Player");
        m_PrevPosition = transform.position;
    }

    void Update()
    {
        if(m_PlayerOnPlatform)
        {
            m_Player.transform.position -= (m_PrevPosition - transform.position);
        }

        m_PrevPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D i_Other)
    {
        if(i_Other.gameObject.tag == "Player")
        {
            m_PlayerOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D i_Other)
    {
        if (i_Other.gameObject.tag == "Player")
        {
            m_PlayerOnPlatform = false;
        }
    }
}
