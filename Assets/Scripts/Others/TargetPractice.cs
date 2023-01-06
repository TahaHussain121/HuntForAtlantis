using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPractice : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Arrow")
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
