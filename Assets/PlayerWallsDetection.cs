using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallsDetection : MonoBehaviour
{
    public PlayerController m_PlayerController;

    private void OnCollisionEnter2D(Collision2D i_Other)
    {
        m_PlayerController.StopDashing();
    }
}
