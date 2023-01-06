using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float upSpeed;
    public bool isMovingUp;
    private void Update()
    {
        if (isMovingUp) transform.position += Vector3.up * upSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Gamemanager.GameOver();
        }
    }
    public void Move()
    {
        isMovingUp = true;
    }
}
