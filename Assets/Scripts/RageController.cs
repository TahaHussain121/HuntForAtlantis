using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RageController : MonoBehaviour
{
    [SerializeField] public int primaryAttackPoints;
    [SerializeField] public int attackedWithMeleePoints;
    [SerializeField] public int attackedWithRangePoints;

    [SerializeField]
    Slider rageSlider;

    [SerializeField]
    int currentRagevalue = 0;
    [SerializeField]
    int MaxRagevalue = 100;

    [SerializeField] private ICharacterManager characterManager;

    private void Awake()
    {
        characterManager = GetComponent<ICharacterManager>();
    }
    public void LateUpdate()
    {
        SetDirection();
    }
    public void IncreaseRage(int val)
    {
        if (currentRagevalue < MaxRagevalue)
        {
            currentRagevalue += val;
            Mathf.Min(currentRagevalue, MaxRagevalue);

            rageSlider.value = currentRagevalue;

            if (currentRagevalue >= MaxRagevalue)
            {
                characterManager.GetCharacterFighter().OnRageBarFilled();
            }
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
        if (rageSlider.transform.forward != Vector3.forward)
        {
            rageSlider.transform.LookAt(Camera.main.transform);
            rageSlider.transform.Rotate(0, 180, 0);
        }
    }
}
