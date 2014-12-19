using UnityEngine;
using System.Collections;

namespace Meta.Apps
{
    public class GlassPane : MetaBehaviour
    {

        public LayerMask glassPaneMask;

        //Public for debug
        private Vector3 GoTo = Vector3.zero;
        private Quaternion newRotation;
        private Vector3 playerToMouse;

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

        Vector3 GetPoint()
        {
            return GoTo;
        }
    }
}
