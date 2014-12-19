using UnityEngine;
using System.Collections;
using Meta;
using Meta.Apps;

public class RaycastElevator : MetaBehaviour {

	public GameObject camera;
	public float speed;
	public float rotationSpeed;
	public GameObject player;
	public LayerMask elevatorMask;
	Vector3 GoTo;
	Vector3 playerToMouse;
	Quaternion newRotation;
	public Rigidbody rigidbody;
	Vector3 currentposition;
	RaycastHit floorHit;
	Ray camRay;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Navigate ();
	}

	public void  OnTouchEnter() {
		camRay.direction = Hands.left.pointer.position;

		}

	public void OnTouchExit()
	{
		if (Physics.Raycast (camRay, out floorHit, Mathf.Infinity, elevatorMask)) {
						GoTo = floorHit.point;
				}
	}

	void Navigate()
	{
		
		
		
		//Cast a ray from our screen to the mouse pointer, effectively finding a point on the plane
		//Define a var we'll use later
		//If the raycast returns true i.e. hits the floor

		//Move toward this new position
		player.transform.position = Vector3.MoveTowards(player.transform.position,GoTo, speed);
		
		
		playerToMouse = GoTo - player.transform.position;
		
		
		
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);
		
		
		
		
		
		newRotation = Quaternion.LookRotation (playerToMouse);
		if (player.transform.position != GoTo){
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
			
			
			
			
			
			
			
			
			
			
			
		}

}

}
