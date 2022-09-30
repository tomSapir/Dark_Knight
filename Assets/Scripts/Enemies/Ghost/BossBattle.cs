using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private CameraController m_Camera;
    [SerializeField] private Transform m_CameraPosition;
    [SerializeField] private float m_CameraSpeed;

    void Start()
    {
        m_Camera = FindObjectOfType<CameraController>();
        m_Camera.enabled = false;
    }

    void Update()
    {
        m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, m_CameraPosition.position, m_CameraSpeed * Time.deltaTime);
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
