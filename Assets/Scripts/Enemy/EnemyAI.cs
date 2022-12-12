using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Patrol, Attack,Dead }

public class EnemyAI : MonoBehaviour, ICharacterManager
{

    [SerializeField] Transform target;
    [SerializeField] EnemyState eState;
    [SerializeField] EnemyAttacker Attacker;
    [SerializeField] EnemyMovement Movement;

    public void Awake()
    {
        Attacker = gameObject.GetComponent<EnemyAttacker>();
        Movement = gameObject.GetComponent<EnemyMovement>();
        Movement.InitializeWaypoints();
    }
    public void Start()
    {
        target = Gamemanager.ActiveIInputHandler.GetTransform();
        // Movement.MoveHorizontally(0.5f);
       
        Movement.Patrol(1f);

    }

    public void Update()
    {
        
        
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

    
    private Vector2 GetDistance(Vector3 A, Vector3 B)
    {
        return new Vector2(Mathf.Sqrt(Mathf.Pow((A.x-B.x),2)), Mathf.Sqrt(Mathf.Pow((A.y - B.y), 2)));
    }

    //private float GetDistance(Vector3 A, Vector3 B)
    //{
    //    return Vector3.Distance(new Vector3(A.x, A.y, 0), new Vector3(B.x,B.y, 0));
    //}
    public void Navigation()
    {
        target = Gamemanager.ActiveIInputHandler.GetTransform();
        Vector2 dist = GetDistance(this.transform.position, target.position);
       // Debug.Log("y distance"+dist.y);
        if ( dist.x<= 10&&dist.y<8)
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
                Movement.Follow(target);
                Attacker.PrimaryAttack(target);
                Console.WriteLine("enemy attacking");
                break;
            case EnemyState.Patrol:
                Attacker.EndPrimaryAttack();
                Movement.Patrol(1f);
                Console.WriteLine("enemy patrol");
                break;
            case EnemyState.Dead:
                this.gameObject.SetActive(false);
                Console.WriteLine("enemy dead");
                break;
          
        }
    }

    public Health GetHealthController()
    {
        return gameObject.GetComponent<HealthController>();
        
    }
}
