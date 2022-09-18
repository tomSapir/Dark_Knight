using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator m_Animator;
    public PlayerMovement m_PlayerMovement;

    void Update()
    {
        checkAttackTypeAndHandle();
    }

    private void checkAttackTypeAndHandle()
    {
        bool zPressed = Input.GetKeyDown(KeyCode.Z);
        bool currentlyJumping = m_Animator.GetBool(PlayerMovement.sr_AnimatorJumpingParameterName);
        bool currentlyCrouching = m_PlayerMovement.Crouch;

        if(zPressed)
        {
            if(currentlyJumping)
            {
                Debug.Log("Air attack is playing");
            }
            else if(!currentlyCrouching)
            {
                normalAttack();
            }
        }


    }

    private void normalAttack()
    {
        m_Animator.SetTrigger("NormalAttack");
    }


}
