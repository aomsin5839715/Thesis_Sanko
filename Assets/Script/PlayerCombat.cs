using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    void Update()
    {
        // Check if the attack input is triggered
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack1"); // "Attack" is the name of the attack animation trigger parameter
    }
}
