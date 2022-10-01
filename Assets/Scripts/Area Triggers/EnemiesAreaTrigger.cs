using System.Collections.Generic;
using UnityEngine;

public class EnemiesAreaTrigger : MonoBehaviour
{
    public List<GameObject> m_Enemies = new List<GameObject>();
    private bool m_AlreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player" && !m_AlreadyTriggered)
        {
            foreach (GameObject enemy in m_Enemies)
            {
                enemy.SetActive(true);
            }

            m_AlreadyTriggered = true;
        }
    }
}
