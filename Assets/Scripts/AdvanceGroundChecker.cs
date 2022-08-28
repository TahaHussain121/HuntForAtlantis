using SimpleMan.VisualRaycast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SimpleMan.VisualRaycast.Demo.Raycaster;

public class AdvanceGroundChecker : MonoBehaviour
{
    [SerializeField]
    float adjustx = 0.02f;
    [SerializeField]
    float adjustforwadX = 0.02f;
    [SerializeField]
    float adjustBackX = 0.02f;
    CharacterController charCtrl;
    public CastType castType = CastType.Raycast;
    bool slip;
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        // RaycastHit hit;
        CastResult Down;
       
        CastResult BackwardDown;
        //CastResult frontdir;
        //CastResult backdir;


        //Vector3 p1 = transform.position + charCtrl.center;
        //float distanceToObstacle = 0;

        Down = this.Raycast(new Vector3(transform.position.x + adjustx, transform.position.y, transform.position.z), -transform.up, 30f);
      
        BackwardDown = this.Raycast(new Vector3(transform.position.x + adjustBackX, transform.position.y, transform.position.z), -transform.up, 30f);
      
            string downHit = Down.FirstHit.collider.name;
         
            string backHit = BackwardDown.FirstHit.collider.name;

         if ((downHit == "Quad"|| backHit=="Quad")&&!(downHit == "Quad" && backHit == "Quad"))
         {
            float hitpos;
            float distance=0;
            Vector3 direction=Vector3.zero;
            if (Down)
            {
                hitpos = Down.FirstHit.collider.transform.position.y;

                distance = GetDistance(hitpos);
                direction = Down.FirstHit.collider.transform.position.normalized;
            }
            if (BackwardDown)
            {
                hitpos = BackwardDown.FirstHit.collider.transform.position.y;

                distance = GetDistance(hitpos);
                direction = BackwardDown.FirstHit.collider.transform.position.normalized;

            }

            Debug.Log("direction" + direction);

                if (distance >= 9 && distance < 11)
                {
                   


                }

            }

        


        }
      
    public float GetDistance( float B)
    {
       return Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, B, 0));
    }
}

