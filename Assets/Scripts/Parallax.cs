using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float m_Length;
    private float m_StartPosition;
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private float m_ParallaxEffect;

    void Start()
    {
        m_StartPosition = transform.position.x;
        m_Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

   
    void Update()
    {
        float temp = m_Camera.transform.position.x * (1 - m_ParallaxEffect);
        float distance = m_Camera.transform.position.x * m_ParallaxEffect;

        transform.position = new Vector3(m_StartPosition + distance, transform.position.y, transform.position.z);

        if(temp > m_StartPosition + m_Length)
        {
            m_StartPosition += m_Length;
        }
        else if(temp < m_StartPosition - m_Length)
        {
            m_StartPosition -= m_Length;
        }
    }
}
