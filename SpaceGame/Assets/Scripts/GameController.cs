using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Meta.Apps;

public class GameController : MonoBehaviour {

	public GameObject camera;
    public enum InputTypes {Mouse, Touch, Meta};
    public InputTypes InputType;
    // 0 = mouse, 1 = touch, 2 = voice
    public GameObject GlassPaneObject; // you will need this if scriptB is in another GameObject
    private GlassPane GlassPaneScript;
	public float speed;
	public float rotationSpeed;
	public GameObject player;
	public GameObject locator;
	public LayerMask elevatorMask;
	Vector3 GoTo;
	Vector3 playerToMouse;
	Quaternion newRotation;
	public Rigidbody rigidbody;
	Vector3 currentposition;

	// Use this for initialization
	void Start () {
        GlassPaneScript = GlassPaneObject.GetComponent<GlassPane>();

	}
	
	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate(){
				Navigate ();
		}


	void Navigate()
	{



		//Cast a ray from our screen to the mouse pointer, effectively finding a point on the plane
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//Define a var we'll use later
		RaycastHit floorHit;
		//If the raycast returns true i.e. hits the floor
		if (Physics.Raycast (camRay, out floorHit, Mathf.Infinity, elevatorMask))
		{

			locator.transform.position = floorHit.point;

			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){ 
				// Non-UI mouse click 
				GoTo = floorHit.point;
			}

		}

        if (GoTo != player.transform.position && GoTo != Vector3.zero)
        {

            //Move toward this new position
            player.transform.position = Vector3.MoveTowards(player.transform.position, GoTo, speed);


            playerToMouse = GoTo - player.transform.position;



            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);
            newRotation = Quaternion.LookRotation(playerToMouse);

        }

			if (player.transform.position != GoTo){
				player.transform.rotation = Quaternion.Slerp(player.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);









			

		}
	
	}


}


