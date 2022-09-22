using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusAI : MonoBehaviour, ICharacterManager
{
    CerberusAttacker attavker;
   
    public void Awake()
    {
        attavker = GetComponent<CerberusAttacker>();
    }
    [ContextMenu("TestFireball")]
    void TestFireball()
    {
        attavker.PrimaryAttack();
    }
    public IFighter GetCharacterFighter()
    {
        return gameObject.GetComponent<CerberusAttacker>();
    }

    public IMovement GetCharacterMovement()
    {
        return gameObject.GetComponent<CerberusMovement>();

    }

    public RageController GetRageController()
    {
        return gameObject.GetComponent<RageController>();

    }
}
