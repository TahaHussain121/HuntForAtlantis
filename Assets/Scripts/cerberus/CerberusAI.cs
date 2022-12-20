using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusAI : MonoBehaviour, ICharacterManager
{
    CerberusAttacker attacker;
  
    public void Awake()
    {
        attacker = GetComponent<CerberusAttacker>();
    }
    [ContextMenu("TestFireball")]
    void TestFireball()
    {
        attacker.PrimaryAttack();
    } 
    [ContextMenu("TestLunge")]
    void TestLunge()
    {
        attacker.MeleeAttack();
    }

    public void Update()
    {
      //  if (attacker.CheckEnemyInRange())
       // {

            attacker.PrimaryAttack();
            
      //  }
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

    public void OnRageBarFilled() {
        Debug.Log("rageFull");
        attacker.OnRageBarFilled();
    
    }


    public void OnRageEmpty() {

        attacker.OnRageBarEmptied();
    }
}
