using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float m_ShootArrowCooldown;
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private GameObject m_Arrow;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_TimeFromStartAnimationUntilShoot;
    private float m_CooldownTimer;


    IEnumerator ShootArrow()
    {
        yield return new WaitForSeconds(m_TimeFromStartAnimationUntilShoot);
        GameObject newArrow = Instantiate(m_Arrow, m_FirePoint.position, Quaternion.identity);

        if(transform.localScale.x > 0)
        {
            newArrow.GetComponent<Arrow>().SetDirection(new Vector3(1, 0, 1));
        }
        else
        {
            newArrow.GetComponent<Arrow>().SetDirection(new Vector3(-1, 0, 1));
        }

    }

    void Update()
    {
        m_CooldownTimer += Time.deltaTime;

        if(m_CooldownTimer >= m_ShootArrowCooldown)
        {
            m_Animator.SetTrigger("Shoot");
            m_CooldownTimer = 0;
            StartCoroutine(ShootArrow());
        }
    }

}
