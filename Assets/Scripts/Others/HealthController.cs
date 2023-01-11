using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : Health
{
    public void Start()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }
    public override void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);

    }

    public override void TakeDamage(int val)
    {
        Debug.Log("taking damage" +this.gameObject.name);
        if (currentHealth > 0 && currentHealth >= val)
        {
            currentHealth = currentHealth - val;

            UpdateSlider(val);
        }
        if (currentHealth == 0)
        {
            if (gameObject.tag == "Player")
            {
                UpdateSlider(val);

                Gamemanager.GameOver();

            }
            else
            {
                  Destroy(this.gameObject);
            }
        }
    }

    public void UpdateSlider(int val)
    {
        if (healthSlider != null)
        {
            healthSlider.value = val;
        }
        
    }
}
