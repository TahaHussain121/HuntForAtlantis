using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RageController : MonoBehaviour
{
    [SerializeField]
    Slider rageSlider;

    [SerializeField]
    int currentRagevalue = 0;
    [SerializeField]
    int MaxRagevalue = 100;

    public void LateUpdate()
    {
        SetDirection();
    }
    public void IncreaseRage(int val)
    {
        if (currentRagevalue < MaxRagevalue)
        {
            currentRagevalue += val;
            rageSlider.value = currentRagevalue;
        }
    }
    public void DecreaseRage(int val)
    {
        if (currentRagevalue > 0 && currentRagevalue > val)
        {
            currentRagevalue -= val;
            rageSlider.value = currentRagevalue;
        }
    }
    public void ResetRage()
    {
        currentRagevalue = 0;
        rageSlider.value = currentRagevalue;
    }

    void SetDirection()
    {
        rageSlider.transform.LookAt(Camera.main.transform);
        rageSlider.transform.Rotate(0, 180, 0);
    }
}
