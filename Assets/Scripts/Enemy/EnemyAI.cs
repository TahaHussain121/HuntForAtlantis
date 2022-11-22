using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Patrol, Attack,Dead }

public class EnemyAI : MonoBehaviour, ICharacterManager
{

    [SerializeField] Transform target;
    [SerializeField] EnemyState eState;
    [SerializeField] IFighter Attacker;
    [SerializeField] IMovement Movement;

    public void Start()
    {
        target = Gamemanager.ActiveIInputHandler.GetTransform();
        // Movement.MoveHorizontally(0.5f);
        Attacker = GetCharacterFighter();
        Movement = GetCharacterMovement();
        Movement.MoveHorizontally(0.5f);

    }

    public void Update()
    {
        //Movement.MoveHorizontally(0.5f);
        
        Navigation();
    }
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

    private float GetDistance(Vector3 A, Vector3 B)
    {
        Debug.Log(Vector3.Distance(new Vector3(A.x, 0, 0), new Vector3(B.x, 0, 0)));
        return Vector3.Distance(new Vector3(A.x, 0, 0), new Vector3(B.x, 0, 0));
    }
    public void Navigation()
    {

        if (GetDistance(this.transform.position, target.position) <= 20)
        {
            if (eState != EnemyState.Attack)
            {
                eState = EnemyState.Attack;
                OnStateChange(eState);
            }
        }
        else
        { 

           if (eState != EnemyState.Patrol)
            {
                eState = EnemyState.Patrol;
                OnStateChange(EnemyState.Patrol);
            }
          

        }

    }
    public void OnStateChange(EnemyState newstate)
    {
        switch (newstate)
        {

            case EnemyState.Attack:
                Attacker.PrimaryAttack();
                Console.WriteLine("enemy attacking");
                break;
            case EnemyState.Patrol:
                Movement.MoveHorizontally(1f);
                Console.WriteLine("enemy patrol");
                break;
            case EnemyState.Dead:
                this.gameObject.SetActive(false);
                Console.WriteLine("enemy dead");
                break;
          
        }
    }
}
