using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    private Vector3 m_Offset;

    void Start()
    {
        m_Offset.z = transform.position.z;
    }

    void Update()
    {
        transform.position = PlayerHealthController.m_Instance.transform.position + m_Offset;
    }
}
