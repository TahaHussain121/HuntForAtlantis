using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageController : MonoBehaviour
{
    [SerializeField] 
    int rageValue = 0;

    public void UpdateRage(int val)
    {
        rageValue += val;
    }
    public void ResetRage()
    {
        rageValue = 0 ;
    }
}
