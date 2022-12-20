using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ICharacterManager
{
   

    public IFighter GetCharacterFighter()
    {
        return gameObject.GetComponent<EnemyAttacker>();
    }

    public IMovement GetCharacterMovement()
    {
        return gameObject.GetComponent<EnemyMovement>();

    }

    public RageController GetRageController()
    {
        return gameObject.GetComponent<RageController>();

    }
}
