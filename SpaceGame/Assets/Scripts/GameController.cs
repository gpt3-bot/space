using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Meta.Apps;

public class GameController : MonoBehaviour {

	
	private static GameController _instance;
	
	public static GameController instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameController>();
				
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
    public enum InputTypes {Mouse, Touch, Meta};
    static public InputTypes InputType;
    // 0 = mouse, 1 = touch, 2 = voice

	public float speed;
	public float rotationSpeed;
	GameObject player;
	public GameObject locator;
	public LayerMask elevatorMask;
	Vector3 GoTo;
	Vector3 playerToMouse;
	Quaternion newRotation;
	Vector3 currentposition;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate(){
				Navigate ();
		}


	void Navigate()
	{


		if (player == null) {
			player = NetworkManager.ReturnPlayer();
						return;
				}



		if (InputType == InputTypes.Mouse || InputType == InputTypes.Touch) {

						//Cast a ray from our screen to the mouse pointer, effectively finding a point on the plane
						Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
						//Define a var we'll use later
						RaycastHit floorHit;
						//If the raycast returns true i.e. hits the floor
						if (Physics.Raycast (camRay, out floorHit, Mathf.Infinity, elevatorMask)) {

								locator.transform.position = floorHit.point;

								if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) { 
										// Non-UI mouse click 
										GoTo = floorHit.point;
								}

						}

		} else if (InputType == InputTypes.Meta) {
			GoTo = GlassPane.GetPoint();
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

	public static int returnInputType(){
		return (int)InputType;
	}


}


