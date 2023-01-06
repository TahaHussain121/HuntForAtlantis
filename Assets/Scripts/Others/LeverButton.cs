using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverButton : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Arrow")
        {
            Destroy(other.gameObject);

            ShowBridge();
        }
    }
    public void ShowBridge()
    {
        bridge.SetActive(true);
        gameObject.SetActive(false);
    }
}
