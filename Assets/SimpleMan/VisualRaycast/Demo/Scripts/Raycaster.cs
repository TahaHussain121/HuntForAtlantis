using UnityEngine;

namespace SimpleMan.VisualRaycast.Demo
{
    public class Raycaster : MonoBehaviour
    {
        //******     FIELDS AND PROPERTIES   	******\\
        public enum CastType
        {
            Raycast,
            Boxcast,
            Spherecast
        }
        public CastType castType = CastType.Raycast;
        public bool castAll;
        public float radius;
        public Vector3 pos;
        public Vector3 val;

        public void Start()
        {
            pos = transform.position;
        }
        //******    	    METHODS  	  	    ******\\
        private void Update()
        {
            
            CastResult castResult;

            //Make cast from origin position to forward
            switch (castType)
            {
                case CastType.Raycast:
                    castResult = this.Raycast(castAll, transform.position, Vector3.down); break;

                case CastType.Boxcast:
                    castResult = this.Boxcast(castAll, pos, -transform.up, Vector3.one); break;

                case CastType.Spherecast:
                    castResult = this.SphereCast(castAll, pos, -transform.up, radius); break;
            }

            //Did raycast hit something? -> Try paint it
            if (castResult)
            {
                Debug.Log("name " + castResult.FirstHit.collider.name);
                PaintCastTargets(castResult, Color.white);
            }

            if (Input.GetKeyDown(KeyCode.S))
                MakeSphereOverlap();

            else if (Input.GetKeyDown(KeyCode.B))
                MakeBoxOverlap();
        }

        /// <summary>
        /// Change color of material on target game object
        /// </summary>
        /// <param name="target"> Target game object </param>
        /// <param name="newColor"> New color </param>
        private void PaintCastTargets(CastResult result, Color newColor)
        {
            foreach (var item in result.Hits)
            {
                if (item.transform.TryGetComponent(out Renderer renderer))
                    renderer.material.color = newColor;
            }
        }

        private void MakeBoxOverlap()
        {
            this.BoxOverlap(transform.position, Vector3.one * 10);
        }

        private void MakeSphereOverlap()
        {
            this.SphereOverlap(transform.position, 5);
        }
    }
}