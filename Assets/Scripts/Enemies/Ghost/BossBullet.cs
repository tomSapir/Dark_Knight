using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Rigidbody2D m_RigidBody;
    [SerializeField] private int m_DamageAmount;
    [SerializeField] private GameObject m_ImpactEffect;

    void Start()
    {
        Vector3 direction = transform.position - PlayerHealthController.m_Instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }


    void Update()
    {
        m_RigidBody.velocity = -transform.right * m_MoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D i_Other)
    {
        if(i_Other.gameObject.tag == "Player")
        {
            PlayerHealthController.m_Instance.TakeDamage(m_DamageAmount);

        }

        if(m_ImpactEffect != null)
        {
            Instantiate(m_ImpactEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
