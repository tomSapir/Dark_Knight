using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_ActivationDelay; // Time from when player touch fire until next fire
    [SerializeField] private float m_ActiveTime; // how long the trap stays active after it has been activated
    [SerializeField] private Sprite m_TriggeredSprite;
    [SerializeField] private Sprite m_NormalSprite;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    private bool m_IsTriggered; // when the trap gets triggered
    private bool m_IsActive; // when the trap is active and can hurt the player

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.tag == "Player")
        {
            if(!m_IsTriggered)
            {
                StartCoroutine(ActivateFireTrap());
            }

            if(m_IsActive)
            {
                PlayerHealthController.m_Instance.TakeDamage(m_Damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        m_IsTriggered = true;
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(m_ActivationDelay);
        m_IsActive = true;
        m_SpriteRenderer.color = Color.white;
        m_Animator.SetBool("Activated", true);

        yield return new WaitForSeconds(m_ActiveTime);
        m_IsActive = false;
        m_IsTriggered = false;
        m_Animator.SetBool("Activated", false);
    }

}
