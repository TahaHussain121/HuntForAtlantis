using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector3 speed;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.position += speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        print("trigger detected");
        if (other.tag == "Enemy")
        {
            print("collision with enemy detected");
            Destroy(gameObject);
        }
    }
}
