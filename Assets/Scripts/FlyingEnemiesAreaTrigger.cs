using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemiesAreaTrigger : MonoBehaviour
{
    public List<GameObject> m_FlyingEnemies = new List<GameObject>();
    private bool m_AlreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player" && !m_AlreadyTriggered)
        {
            foreach (GameObject enemy in m_FlyingEnemies)
            {
                enemy.SetActive(true);
            }

            m_AlreadyTriggered = true;
        }


    }
}
