
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AdvanceGroundChecker : MonoBehaviour
{

    /// simple raycast data

    [SerializeField]
    float xAdjuster;
    [SerializeField]
    float yAdjuster;
    [SerializeField]
    float slipSpeed;
    CharacterController charCtrl;
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    void Update()
    {

        AdvGroundSimpleRaycast();

    }

    

    public void AdvGroundSimpleRaycast()
    {

        if (charCtrl.isGrounded)
        {

           // Debug.Log("here");
            RaycastHit hit;

            Vector3 middle = transform.position + Vector3.up * yAdjuster;


            Vector3 front = transform.forward * xAdjuster;

            Vector3 Back = -transform.forward * xAdjuster;

            float dis = xAdjuster;

            Ray frontRay = new Ray(middle, front);
            Ray backRay = new Ray(middle, Back);

            if (Physics.Raycast(frontRay, out hit, dis) || Physics.Raycast(backRay, out hit, dis))
            {

           // Debug.Log(hit.collider.name);

                slip(hit.normal);

            }

        }
    }

    public void slip(Vector3 dir) 
    {
           // Debug.Log("Slip called");

        charCtrl.Move(((dir*slipSpeed)+Vector3.down)/**Time.deltaTime*/);
    }

    void OnDrawGizmos()
    {
        Vector3 ray_spwan_pos = transform.position + Vector3.up * yAdjuster;
        Vector3 forward = transform.forward * xAdjuster;
        Vector3 back = -transform.forward * xAdjuster;
        Vector3 right = transform.right * xAdjuster;
        Vector3 left = -transform.right * xAdjuster;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray_spwan_pos, forward);
        Gizmos.DrawRay(ray_spwan_pos, back);
        Gizmos.DrawRay(ray_spwan_pos, right);
        Gizmos.DrawRay(ray_spwan_pos, left);
    }
        public float GetDistance(Vector3 A, Vector3 B)
    {
        return Vector3.Distance(new Vector3(0, A.y, 0), new Vector3(0, B.y, 0));
    }


    ///
}