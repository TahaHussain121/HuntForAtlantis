using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    
    private Coroutine _patrolCoroutine;
    private Coroutine _followCoroutine;
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 5f;


    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void MoveHorizontally(float hor)
    {
       
    }
    public void Patrol(float sec)
    {
        Debug.Log("patrol");
        if (_followCoroutine!=null)
        {
            StopCoroutine(_followCoroutine);
        }
         _patrolCoroutine = StartCoroutine(_MovingToNextWaypoint(sec));
         
    }


    private IEnumerator _MovingToNextWaypoint(float sec)
    {
        Transform wp = waypoints[_currentWaypointIndex];

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, wp.position) <= 0)
            {
               
                    if (_currentWaypointIndex + 1 < waypoints.Length)
                    {
                        _currentWaypointIndex += 1;
                    }
                    else
                    {
                        _currentWaypointIndex = 0;
                    }
                    wp = waypoints[_currentWaypointIndex];
                yield return new WaitForSeconds(sec);

            }
            yield return null;
        }

    

    }
        
        
   
    public void Follow(Transform target)
    {
        Debug.Log("follow");
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
        }
        _followCoroutine = StartCoroutine(_followTarget(target));
    }

    private IEnumerator _followTarget(Transform target)
    {
        
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,0,0), _speed * Time.deltaTime);
            
            yield return null;
        }

    }


    public void HandleRotation(float axis)
    {
        Quaternion toRotate;
        switch (axis)
        {
            case 0:
                toRotate = Quaternion.Euler(0, 0, 0);
                //this.transform.rotation = toRotate;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, toRotate, 0.5f);
                break;
            case 1:
                toRotate = Quaternion.Euler(0, 180, 0);
                //this.transform.rotation = toRotate;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, toRotate, 0.5f);
                break;
        }

    }

}
