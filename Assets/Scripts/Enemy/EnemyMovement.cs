using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    bool inRoutine = false;
    private Coroutine _prevCoroutine;
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;


    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void MoveHorizontally(float hor)
    {
        Patrol(hor);
    }
    public void Patrol(float sec)
    {
       
         _prevCoroutine = StartCoroutine(_MovingToNextWaypoint(sec));
         
    }

   
    private IEnumerator _MovingToNextWaypoint(float sec)
    {
        Transform wp = waypoints[_currentWaypointIndex];

     while (Mathf.Abs(transform.position.x-wp.position.x)< 1f)
        {
            transform.position = Vector3.Lerp(transform.position, wp.position, 0.1f);
            yield return null;
        }

        transform.position = wp.position;
        yield return new WaitForSeconds(sec);
        Debug.Log("new way point");
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;

        StopCoroutine(_prevCoroutine);
        _prevCoroutine = StartCoroutine(_MovingToNextWaypoint(sec));
        
    }

   
    public void Follow(Transform target)
    {
        throw new System.NotImplementedException();
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
