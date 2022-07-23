using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("trigger detected");
        //if (other.tag == "Enemy")
        //{
        //    print("collision with enemy detected");
        //    Destroy(gameObject);
        //}
    }
}
