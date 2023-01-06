using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") onTriggered?.Invoke();
    }
}
