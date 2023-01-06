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
    [SerializeField] float LungeForce;
    [SerializeField] Rigidbody headRb;
    [SerializeField] Vector3 InitialPos;
    [SerializeField] Quaternion fireptInitialRot;

    [SerializeField] ShakeEffect shakeEffect;
   
    [SerializeField] int maxHealth ;
    [SerializeField] int currentHealth ;
  
    public delegate void Attacked(CharacterType ctype, AttackType atype,CerberusHead head);
    public static Attacked TakeAttack; 
    public delegate void Death(CerberusHead head);
    public static Death OnDeath;
    public void Awake()
    {
        headRb = GetComponent<Rigidbody>();
        shakeEffect = GetComponent<ShakeEffect>();
        head = this.transform;
        InitialPos = transform.position;
        fireptInitialRot = firePoint.rotation;
    }
    public void OnEnable()
    {
      //  CerberusAttacker.TakeDamage += UpdateHealth;
        HealthPool.SetupHealth += setupHealth;
    }  
    public void OnDisable()
    {
       // CerberusAttacker.TakeDamage -= UpdateHealth;
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

    public bool CheckRange()
    {
        RaycastHit hit;

        Vector3 dir = -transform.right;

        float dis = 100f;

        Ray rangeRay = new Ray(firePoint.position,dir);


        if (Physics.Raycast(rangeRay, out hit, dis))
        {
           
            if (hit.collider.GetComponent<IAttackable>()!=null)
            {
                return true;
            }

        }
        return false;
    }
   
    public void TakePosForLunge()
  {
        headRb.position = new Vector3(headRb.position.x+3, headRb.position.y, headRb.position.z);
     
  }
    public void PullBack()
  {
        headRb.position = new Vector3(headRb.position.x+5, headRb.position.y, headRb.position.z);
      
  }
    [ContextMenu("LungeAttack")]
    public void LungeAttack()
  {
       
       headRb.AddForce(-LungeForce,0f,0f, ForceMode.VelocityChange);
   }
   public void ResetPos()
  {
        headRb.position = InitialPos;
        firePoint.rotation = fireptInitialRot;
     // transform.position = InitialPos;
   }
   public void Shake()
    {
        shakeEffect.StartShake(1f, 1f, 0f);
      //  shakeEffect.StartShake(0.5f, 0.2f, 0f);
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
    public void UpdateHealth(int val)
    {
        if (currentHealth >= val)
        {
            currentHealth -= val;
           
        }
        if(currentHealth<=0)
        {
            onDeath(this);

            //death
        }
      
    }

    private void onDeath(CerberusHead head)
    {
        OnDeath(head);
        this.gameObject.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {

        IAttackable Attacker = collision.gameObject.GetComponent<IAttackable>();
        if (Attacker != null)
        {
            Debug.Log("Something Hit");
            TakeAttack(Attacker.GetCharacterType(), Attacker.GetAttackType(), this);
        }
       

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Debug.Log("Arrow");

            TakeAttack(CharacterType.Satyr, AttackType.Ranged, this);

        }
    }
}
