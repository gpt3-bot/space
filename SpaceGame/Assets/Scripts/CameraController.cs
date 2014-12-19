using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject MetaCamera;
    public GameObject GlassPane;

 public Transform WorldCenter;
  public Transform PlayerCenter;

  private Transform TargetLookAt;

public Button WorldButton;
public Button PlayerButton;
public GameObject sliders;
public GameObject GameController;
private GameController GameControllerScript;
private int InputType;
 public Slider SliderX;
  public Slider SliderY;
    public Slider SliderZoom;
    private float sliderZoomValue;
    public GameObject VoiceControlObject; // you will need this if scriptB is in another GameObject
    public RotateCameraVoiceControl VoiceControlScript;


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
 
 
 void  Start (){
     GameControllerScript = GameController.GetComponent<GameController>();
     InputType = (int)GameControllerScript.InputType;
     TargetLookAt = WorldCenter;
 sliderZoomValue = SliderZoom.value;
  WorldButton.onClick.AddListener(World);
  PlayerButton.onClick.AddListener(Player);
  VoiceControlScript = VoiceControlObject.GetComponent<RotateCameraVoiceControl>();

     if (InputType == 0 || InputType == 2)
     	{
sliders.SetActive(false);
     	}
     // Activate regular camera if not using Meta
     if (InputType == 0 || InputType == 1){
         MetaCamera.SetActive(false);
         GlassPane.SetActive(false);
         MainCamera.SetActive(true);
     }

     if (InputType == 2){
         Distance *= 2;
       MetaCamera.SetActive(true);
         GlassPane.SetActive(true);
         MainCamera.SetActive(false);
     }

 
     Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
     startingDistance = Distance;
     Reset();
 }
 
 void Update (){
     if (TargetLookAt == null)
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
            Debug.Log("Input type = 1");
     	mouseX = SliderX.value;
         mouseY = SliderY.value;
     }

     if (InputType == 2)
       
     {
         Debug.Log("Input type = 2");
         mouseX = VoiceControlScript.GetRotateX();
         mouseY = VoiceControlScript.GetRotateY();

     }
     
   
     
     // this is where the mouseY is limited - Helper script
     mouseY = Mathf.Clamp(mouseY, Y_MinLimit, Y_MaxLimit);
     Debug.Log(mouseY);
   
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
     
 }
 
 void  CalculateDesiredPosition (){
     Debug.Log("Calculating desired position");
     // Evaluate distance
     Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velocityDistance, DistanceSmooth);
     
     // Calculate desired position -> Note : mouse inputs reversed to align to WorldSpace Axis
     desiredPosition = CalculatePosition(mouseY, mouseX, Distance);
     Debug.Log("Desired position:" + desiredPosition);
 }
 

 Vector3  CalculatePosition ( float rotationX ,   float rotationY ,   float distance  ){
     Debug.Log("Calculating position");
     Vector3 direction = new Vector3(0, 0, -distance);
     Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
     return TargetLookAt.position + (rotation * direction);
 }
 
 void  UpdatePosition (){
     Debug.Log("Updating position");
     float posX= Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
     float posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
     float posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
     position = new Vector3(posX, posY, posZ);

     if (InputType == 0 || InputType == 1)
     {

         MainCamera.transform.position = position;

         MainCamera.transform.LookAt(TargetLookAt);

     }
     if (InputType == 2)
     {

         MetaCamera.transform.position = position;

         MetaCamera.transform.LookAt(TargetLookAt);

     }


 }
 
 void  Reset (){
     mouseX = 0;
     mouseY = 10;
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
}