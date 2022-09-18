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
        throw new System.NotImplementedException();
    }

    public IMovement GetCharacterMovement()
    {
        throw new System.NotImplementedException();
    }

    public RageController GetRageController()
    {
        throw new System.NotImplementedException();
    }
}
