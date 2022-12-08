using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public void OnEnable()
    {
      
        EnemyAttacker.TakeDamage += TakeDamage;
    }
    public void OnDisable()
    {
        EnemyAttacker.TakeDamage -= TakeDamage;
    }
   
    public override void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);

    }

    public override void TakeDamage(int val)
    {
        Debug.Log("taking damage");
        if (currentHealth > 0 && currentHealth > val)
        {
            currentHealth = currentHealth - val;
          

        }
    }

   
}
