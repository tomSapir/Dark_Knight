using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject m_BossToActivate;

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player")
        {
            m_BossToActivate.SetActive(true);

            // After we active the boss we dont want to active again, so:
            gameObject.SetActive(false);
        }
    }
}
