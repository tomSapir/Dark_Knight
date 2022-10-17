using System.Collections;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_CheckPointAlert;
    private bool m_AlreadySaved = false;

    void Update()
    {
        if(m_CheckPointAlert == null)
        {
            m_CheckPointAlert = GameObject.Find("Check Point Alert Text");
        }
    }
    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player" && !m_AlreadySaved)
        {
            m_AlreadySaved = true;
            AudioManager.m_Instance.PlaySFX(5);
            StartCoroutine(CheckPointAlertCorutine());
            RespawnController.m_Instance.SetSpawn(transform.position);
        }
    }

    IEnumerator CheckPointAlertCorutine()
    {
        m_CheckPointAlert.GetComponent<TMPro.TextMeshProUGUI>().text = "Check Point Saved";
        // TODO: put sound
        yield return new WaitForSeconds(3);
        m_CheckPointAlert.GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
    }
}
