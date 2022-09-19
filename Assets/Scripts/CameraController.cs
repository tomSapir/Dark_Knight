using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController m_PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerController = FindObjectOfType<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PlayerController != null)
        {
            transform.position = new Vector3(m_PlayerController.transform.position.x, m_PlayerController.transform.position.y, -10);
        }
    }
}
