using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPool : MonoBehaviour
{
    [SerializeField] int maxHealth=450;
    [SerializeField] int currentHealth=450;
    [SerializeField] int healthPerhead;

    [SerializeField]  Slider healthSlider;
    public delegate void Health(int val);
    public static Health SetupHealth;

    private ICharacterManager characterManager;
    public void OnEnable()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        CerberusAttacker.TakeDamage += TakeDamage;
    }
    public void OnDisable()
    {
        CerberusAttacker.TakeDamage -= TakeDamage;
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

    
    public void TakeDamage(int val)
    {
        Debug.Log("talking damage");
        if (currentHealth > 0 && currentHealth > val)
        {
            currentHealth = currentHealth - val;
            healthSlider.value = currentHealth;
            Debug.Log(healthSlider.value);

        }

    }

    public void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);
    }
}
