using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusHead: MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject fireball;
    [SerializeField] float fireballForce;
    [SerializeField] Vector3 fireballdir;
   // [SerializeField] bool IsfireballThrown;

    [ContextMenu("TestFireball")]
    void TestFireball()
    {
       // ThrowFireball(-transform.right);
    }
    public void Awake()
    {
        head = this.transform;
    }
    public void ThrowFireball(Transform target)
    {
        firePoint.LookAt(target);
        //firePoint.rotation = rot;
        Vector3 Dir= (target.position - firePoint.position).normalized;
        fireballdir = Dir;
        GameObject fb = Instantiate(fireball, firePoint);
        fb.GetComponent<Rigidbody>().AddForce(Dir* fireballForce, ForceMode.Impulse);
        Destroy(fb, 1.5f);
    }

    //public bool GetFireballThrown()
    //{
    //    return IsfireballThrown;
    //}
    //public void SetFireballThrown(bool Val)
    //{
    //    IsfireballThrown=Val;
    //}

}
