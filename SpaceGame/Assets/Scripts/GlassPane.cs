using UnityEngine;
using System.Collections;

namespace Meta.Apps
{
    public class GlassPane : MetaBehaviour
    {

		
		
		private static GlassPane _instance;
		
		public static GlassPane instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = GameObject.FindObjectOfType<GlassPane>();
					
					//Tell unity not to destroy this object when loading a new scene!
					DontDestroyOnLoad(_instance.gameObject);
				}
				
				return _instance;
			}
		}
		
		void Awake() 
		{
			if(_instance == null)
			{
				//If I am the first instance, make me the Singleton
				_instance = this;
				DontDestroyOnLoad(this);
			}
			else
			{
				//If a Singleton already exists and you find
				//another reference in scene, destroy it!
				if(this != _instance)
					Destroy(this.gameObject);
			}
		}

        public LayerMask glassPaneMask;

        static Vector3 GoTo = Vector3.zero;
        Quaternion newRotation;
        Vector3 playerToMouse;

        Vector3 rayStart = Vector3.zero;
        Ray ray;
        RaycastHit hit;



        void OnStart()
        {
            ray.origin = rayStart;
        }

        void OnTouchEnter()
        {



            GetComponent<Renderer>().material.color = Color.red;
            if (GetComponent<Collider>().bounds.Contains(Hands.left.pointer.position))
            {
                ray.direction = Hands.left.pointer.position - ray.origin;
                Physics.Raycast(ray, out hit, Mathf.Infinity, glassPaneMask);
                Debug.DrawLine(ray.origin, hit.point, Color.red, 5f, false);
                GoTo = hit.point;
                Debug.Log(Hands.right.pointer.position + " // " + hit.point);
            }
            else if (GetComponent<Collider>().bounds.Contains(Hands.right.pointer.position))
            {
                ray.direction = Hands.right.pointer.position - ray.origin;
                Physics.Raycast(ray, out hit, Mathf.Infinity, glassPaneMask);
                Debug.DrawRay(ray.origin, hit.point, Color.green, 5f, false);
                GoTo = hit.point;
                Debug.Log(Hands.right.pointer.position + " // " + hit.point + " // ");

            }
            else
            {
                Debug.Log("Touch not inside collider.");
            }
        }

        void OnTouchExit()
        {
            GetComponent<Renderer>().material.color = Color.clear;
        }

       public static Vector3 GetPoint()
        {
            return GoTo;
        }
    }
}
