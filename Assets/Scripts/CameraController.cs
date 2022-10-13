using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_BoundsBox;
    [SerializeField]  private Animator m_Animator;
    private PlayerController m_PlayerController;
    private float m_HalfHeight, m_HalfWidth;

    [SerializeField] private float m_BeforeFollowPlayerTime;
    private float m_BeforeFollowPlayerCounter;

    void Start()
    {
        m_BeforeFollowPlayerCounter = m_BeforeFollowPlayerTime;
        m_PlayerController = FindObjectOfType<PlayerController>();
        m_HalfHeight = Camera.main.orthographicSize;
        m_HalfWidth = m_HalfHeight * Camera.main.aspect;
        AudioManager.m_Instance.PlayLevelMusic();

        GameObject alertText = GameObject.Find("Check Point Alert Text");

        if(alertText != null)
        {
            alertText.SetActive(false);
        }
    }

    void Update()
    {
        if(m_BeforeFollowPlayerCounter < 0)
        {
            if (m_PlayerController != null)
            {
                Vector3 playerPosition = m_PlayerController.transform.position;
                float newPositionX = Mathf.Clamp(playerPosition.x, m_BoundsBox.bounds.min.x + m_HalfWidth, m_BoundsBox.bounds.max.x - m_HalfWidth);
                float newPositionY = Mathf.Clamp(playerPosition.y, m_BoundsBox.bounds.min.y + m_HalfHeight, m_BoundsBox.bounds.max.y - m_HalfHeight);
                float newPositionZ = transform.position.z;

                transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
            }
            else
            {
                m_PlayerController = FindObjectOfType<PlayerController>();
            }
        }
        else
        {
            m_BeforeFollowPlayerCounter -= Time.deltaTime;
        }
    }

    public void ShakeCamera()
    {
        m_Animator.SetTrigger("Shake");
    }
}
