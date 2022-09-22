using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour
{
    [SerializeField] int maxHealth=450;
    [SerializeField] int currentHealth=450;
    [SerializeField] int healthPerhead;
    public delegate void Health(int val);
    public static Health SetupHealth;
    private ICharacterManager characterManager;

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
        currentHealth = currentHealth - val;
    }

    public void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);
    }
}
