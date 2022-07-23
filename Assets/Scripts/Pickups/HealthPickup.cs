using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthPercentageToHealBy = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Satyr" )
        {
            other.GetComponent<SatyrFighter>().HealHealthByPercentage(healthPercentageToHealBy);
            Destroy(gameObject);
        }
        else if (other.tag == "Minotaur")
        {
            other.GetComponent<SatyrFighter>().HealHealthByPercentage(healthPercentageToHealBy);
            Destroy(gameObject);
        }
    }
}
