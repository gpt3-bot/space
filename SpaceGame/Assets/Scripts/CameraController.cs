using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	// Singleton pattern
	private static CameraController _instance;
	
	public static CameraController instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<CameraController>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	//field initializers


	GameObject MainCamera;
	GameObject MetaCamera;
	static GameObject camera;
 	public Transform WorldCenter;
 	static Transform PlayerCenter;
	GameObject player;
	Transform TargetLookAt;
	Button WorldButton;
	Button PlayerButton;
	GameObject ViewportNavigation;

	int InputType;
	Slider SliderX;
	Slider SliderY;
	Slider SliderZoom;
    float sliderZoomValue;


 public float Distance = 120.0f;
 public float DistanceMin = 5.0f;
 public float DistanceMax = 100.0f;    
 private float mouseX = 0.0f;
 private float mouseY = 40.0f;
 private float startingDistance = 0.0f;    
 private float desiredDistance = 0.0f;    
 public float X_MouseSensitivity = 50.0f;
 public float Y_MouseSensitivity = 50.0f;
 public float MouseWheelSensitivity = 5.0f;
 public float Y_MinLimit = -40.0f;
 public float Y_MaxLimit = 80.0f;    
 public float DistanceSmooth = 0.05f;    
 private float velocityDistance = 0.0f;    
 private Vector3 desiredPosition = Vector3.zero;    
 public float X_Smooth = 0.05f;
 public float Y_Smooth = 0.1f;
 private float velX = 0.0f;
 private float velY = 0.0f;
 private float velZ = 0.0f;
 private Vector3 position = Vector3.zero;   
 

	
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

		MainCamera = GameObject.FindWithTag("MainCamera");
		MetaCamera = GameObject.FindWithTag("MetaCamera");
		InputType = GameController.returnInputType ();
		
		SliderX = GameObject.Find("SliderX").GetComponent<Slider>();
		SliderY = GameObject.Find("SliderY").GetComponent<Slider>();
		SliderZoom = GameObject.Find("SliderZoom").GetComponent<Slider>();
		
		WorldButton = GameObject.Find ("WorldButton").GetComponent<Button>();
		PlayerButton = GameObject.Find ("PlayerButton").GetComponent<Button>();
		ViewportNavigation = GameObject.Find ("ViewportNavigation");

	}

 void  Start (){


		
		sliderZoomValue = SliderZoom.value;

		WorldButton.onClick.AddListener(World);
		PlayerButton.onClick.AddListener(Player);
		
		if (InputType == 0 || InputType == 2)
		{
			ViewportNavigation.SetActive(false);
		}
		// Activate regular camera if not using Meta
		if (InputType == 0 || InputType == 1){
			camera = MainCamera;
		}

		// Activate the Meta camera
		if (InputType == 2){
			Distance *= 2;
			MetaCamera.SetActive(true);
			MainCamera.SetActive(false);
			camera = MetaCamera;
		}


		TargetLookAt = WorldCenter;
 
     Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
     startingDistance = Distance;
     Reset();


	}
	
 void Update (){

		if(PlayerCenter == null)
		return;

     HandlePlayerInput();
         
     CalculateDesiredPosition();
         
     UpdatePosition();
     
     if(Input.GetKey(KeyCode.R)){
     TargetLookAt = WorldCenter;
     }
     
      if(Input.GetKey(KeyCode.T)){
     TargetLookAt = PlayerCenter;
     }

     
 }
 
 void  HandlePlayerInput (){
     float deadZone= 0.01f; // mousewheel deadZone
     
     if (Input.GetMouseButton(1))
     {
         mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
         mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;
     }

		     if (InputType == 1)
		     	{
		     	mouseX = SliderX.value;
		         mouseY = SliderY.value;
		     }
		
		     if (InputType == 2)
		     {
		         mouseX = RotateCameraVoiceControl.GetRotateX();
				mouseY = RotateCameraVoiceControl.GetRotateY();
		
		     }
     
  
     
     // this is where the mouseY is limited - Helper script
     mouseY = Mathf.Clamp(mouseY, Y_MinLimit, Y_MaxLimit);
   
     // get Mouse Wheel Input
     if ((Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone))
     {
         desiredDistance = Mathf.Clamp(Distance - (Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity), 
                                                                             DistanceMin, DistanceMax);
     }
     
     if(InputType == 1 && SliderZoom.value != sliderZoomValue){
     desiredDistance = Mathf.Clamp(SliderZoom.value, DistanceMin, DistanceMax);
     sliderZoomValue = SliderZoom.value;
     }

		if(InputType == 2){
			desiredDistance = Mathf.Clamp(sliderZoomValue, DistanceMin, DistanceMax);
			sliderZoomValue = RotateCameraVoiceControl.GetZoom();
		}


     
 }
 
 void  CalculateDesiredPosition (){
     // Evaluate distance
     Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velocityDistance, DistanceSmooth);
     
     // Calculate desired position -> Note : mouse inputs reversed to align to WorldSpace Axis
     desiredPosition = CalculatePosition(mouseY, mouseX, Distance);
 }
 

 Vector3  CalculatePosition ( float rotationX ,   float rotationY ,   float distance  ){
     Vector3 direction = new Vector3(0, 0, -distance);
     Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
     return TargetLookAt.position + (rotation * direction);
 }
 
 void  UpdatePosition (){

     float posX= Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
     float posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
     float posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
     position = new Vector3(posX, posY, posZ);

     if (InputType == 0 || InputType == 1)
     {

         camera.transform.position = position;

			camera.transform.LookAt(TargetLookAt);

     }
     if (InputType == 2)
     {

			camera.transform.position = position;

			camera.transform.LookAt(TargetLookAt);

     }


 }
 
 void  Reset (){
     Distance = startingDistance;
     desiredDistance = Distance;
 }
 
 float  ClampAngle ( float angle ,   float min ,   float max  ){
     while (angle < -360 || angle > 360)
     {
         if (angle < -360)
             angle += 360;
         if (angle > 360)
             angle -= 360;
     }
     
     return Mathf.Clamp(angle, min, max);
     
     
 }
 
 void  World (){
      TargetLookAt = WorldCenter;

 }
 
  void  Player (){
      TargetLookAt = PlayerCenter;
}

	public static void setPlayerCenter(){
		PlayerCenter = NetworkManager.ReturnPlayer().transform;
	}

	public static GameObject returnCamera(){
		return camera;
	}


}