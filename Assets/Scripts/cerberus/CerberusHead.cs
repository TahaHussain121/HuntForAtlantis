using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusHead: MonoBehaviour
{
    public Transform head;
    public Transform firePoint;
    public GameObject fireball;
    public float fireballForce;
    // private Rigidbody rg;
    [ContextMenu("TestFireball")]
    void TestFireball()
    {
        ThrowFireball(-transform.right);
    }
    public void Awake()
    {
        head = this.transform;
    }
    private void ThrowFireball(Vector3 Dir)
    {
        GameObject fb = Instantiate(fireball, firePoint);
        fb.GetComponent<Rigidbody>().AddForce(Dir * fireballForce,ForceMode.Impulse);
    }

}
