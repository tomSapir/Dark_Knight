using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController m_PlayerController;

    public BoxCollider2D m_BoundsBox;

    private float m_HalfHeight, m_HalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = FindObjectOfType<PlayerController>();

        m_HalfHeight = Camera.main.orthographicSize;
        m_HalfWidth = m_HalfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PlayerController != null)
        {
            Vector3 playerPosition = m_PlayerController.transform.position;
            float newPositionX = Mathf.Clamp(playerPosition.x, m_BoundsBox.bounds.min.x + m_HalfWidth, m_BoundsBox.bounds.max.x - m_HalfWidth);
            float newPositionY = Mathf.Clamp(playerPosition.y, m_BoundsBox.bounds.min.y + m_HalfHeight, m_BoundsBox.bounds.max.y - m_HalfHeight);
            float newPositionZ = transform.position.z;

            transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
        }
    }
}
