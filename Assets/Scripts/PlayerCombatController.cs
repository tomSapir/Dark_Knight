using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public Animator m_Animator;

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // play attack anim
        m_Animator.SetTrigger("Attack");
        // detect enemis in range of attack
        // damage them
    }
}
