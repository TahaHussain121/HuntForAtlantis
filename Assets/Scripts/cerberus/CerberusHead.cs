using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusHead: MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject Laserbeam;
    [SerializeField] float fireballForce;
   
    [SerializeField] int maxHealth ;
    [SerializeField] int currentHealth ;

    public delegate void Attacked(CharacterType ctype, AttackType atype);
    public static Attacked TakeAttack;
    public void Awake()
    {
        head = this.transform;
    }
    public void OnEnable()
    {
        HealthPool.SetupHealth += setupHealth;
    }  
    public void OnDisable3()
    {
        HealthPool.SetupHealth -= setupHealth;
    }
    public void ThrowFireball(Transform target)
    {
        firePoint.LookAt(target);
   
        Vector3 Dir= (target.position - firePoint.position).normalized;
        //fireballdir = Dir;
        GameObject fb = Instantiate(fireball, firePoint);
        fb.GetComponent<Rigidbody>().AddForce(Dir* fireballForce, ForceMode.Impulse);
        Destroy(fb, 1.5f);
    }

    public void ThrowLaserbeam()
    {
       
        GameObject lb = Instantiate(fireball, firePoint);
        lb.GetComponent<Rigidbody>().AddForce(-transform.right* fireballForce, ForceMode.Impulse);
        Destroy(lb, 1.5f);
    }

    private void setupHealth(int val) {

        maxHealth = val;
        currentHealth = maxHealth;
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        



    }
}
