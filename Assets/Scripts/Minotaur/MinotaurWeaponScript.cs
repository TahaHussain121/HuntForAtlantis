using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurWeaponScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BreakableBlock")
        {
            Destroy(other.gameObject);
        }
    }
}