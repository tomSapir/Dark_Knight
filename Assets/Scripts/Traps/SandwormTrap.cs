using UnityEngine;

public class SandwormTrap : MonoBehaviour
{
    [SerializeField] private int m_DamageAmount;
    [SerializeField] private GameObject m_ImpactEffect;


    private void OnCollisionEnter2D(Collision2D i_Other)
    {
        if (i_Other.gameObject.tag == "Player")
        {
            PlayerHealthController.m_Instance.TakeDamage(m_DamageAmount);
        }

        if (m_ImpactEffect != null)
        {
            Instantiate(m_ImpactEffect, transform.position, transform.rotation);
        }
    }
}
