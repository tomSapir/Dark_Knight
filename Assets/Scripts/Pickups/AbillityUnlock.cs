using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbillityUnlock : MonoBehaviour
{
    [SerializeField] private bool m_UnlockDoubleJump, m_UnlockDash;
    [SerializeField] private GameObject m_PickUpEffect;
    [SerializeField] private string m_UnlockMessage;
    [SerializeField] private TMP_Text m_UnlockText;

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player")
        {
            AudioManager.m_Instance.PlaySFX(13);
            PlayerAbillityTracker playerAbillityTracker = i_Other.gameObject.GetComponentInParent<PlayerAbillityTracker>();

            if(m_UnlockDoubleJump)
            {
                playerAbillityTracker.m_CanDoubleJump = true;
            }

            if (m_UnlockDash)
            {
                playerAbillityTracker.m_CanDash = true;
            }

            Instantiate(m_PickUpEffect, transform.position, transform.rotation);
            m_UnlockText.transform.parent.SetParent(null);
            m_UnlockText.transform.parent.position = transform.position;
            m_UnlockText.text = m_UnlockMessage;
            m_UnlockText.gameObject.SetActive(true);
            Destroy(m_UnlockText.transform.parent.gameObject, 5f);
            Destroy(gameObject); 
        }
    }
}
