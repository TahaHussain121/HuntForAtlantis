using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowButton : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggered;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {
            Destroy(other.gameObject);
            print("arrow detected");
            onTriggered?.Invoke();
            Destroy(gameObject);
        }

    }
  
}
