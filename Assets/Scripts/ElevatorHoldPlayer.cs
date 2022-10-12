using UnityEngine;

public class ElevatorHoldPlayer : MonoBehaviour
{
    private bool m_PlayerOnPlatform = false;
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
            m_Player.transform.position -= (m_PrevPosition - transform.position) / 2;
        }

        m_PrevPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D i_Other)
    {
        m_PlayerOnPlatform = true;
        /*
        Debug.LogError(i_Other.gameObject.name);
        if(i_Other.gameObject.tag == "Player")
        {
            Debug.Break();
            i_Other.gameObject.transform.SetParent(gameObject.transform, true);
        }
        */
    }

    void OnCollisionExit2D(Collision2D i_Other)
    {
        m_PlayerOnPlatform = false;
        /*
        if (i_Other.gameObject.tag == "Player")
        {
            i_Other.gameObject.transform.parent = null;
        }
        */
    }
}
