using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPool : Health
{

    [SerializeField] int healthPerhead;

    private ICharacterManager characterManager;
    public void OnEnable()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = currentHealth;
     
    }
    
    private void Awake()
    {
        characterManager = GetComponent<ICharacterManager>();
    }
    public void Start()
    {
  
        healthPerhead = maxHealth / 3;
        if (SetupHealth != null)
        {
            SetupHealth(healthPerhead);
        }
    }

    
    public override void TakeDamage(int val)
    {
        Debug.Log("taking damage");
        if (currentHealth > 0 && currentHealth >= val)
        {
            currentHealth = currentHealth - val;
            healthSlider.value = currentHealth;
            Debug.Log(healthSlider.value);

        }

    }

    public override void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);
    }
}
