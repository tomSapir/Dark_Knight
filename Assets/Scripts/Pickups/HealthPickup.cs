using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int m_HealthAmount;
    [SerializeField] private GameObject m_PickUpEffect;

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player")
        {
            PlayerHealthController.m_Instance.HealPlayer(m_HealthAmount);
            if(m_PickUpEffect != null)
            {
                Instantiate(m_PickUpEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
