using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBossBattle : MonoBehaviour
{
    private CameraController m_Camera;
    [SerializeField] private Transform m_CameraPositionToPut;
    [SerializeField] private float m_CameraMovementSpeed;


    void Start()
    {
        m_Camera = FindObjectOfType<CameraController>();
        m_Camera.enabled = false;
    }

    void Update()
    {
        m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, m_CameraPositionToPut.position, m_CameraMovementSpeed * Time.deltaTime);
    }
}
