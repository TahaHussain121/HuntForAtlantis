using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoToGive = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Satyr")
        {
            other.GetComponent<SatyrFighter>().AddAmmo(ammoToGive);
            Destroy(gameObject);
        }
    }
}
