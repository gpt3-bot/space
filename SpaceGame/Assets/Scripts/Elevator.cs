using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Elevator : MonoBehaviour {

	public float moveSpeed = 20f;
	public float LimitUp;
	public float LimitDown;
	public Slider slider;
	private float currentSliderValue;
    public GameObject GameController;
    private GameController GameControllerScript;
    private int InputType;
	public GameObject ElevatorObject;
	public GameObject OuterSphere;
	private float h;


	void Start() {
        GameControllerScript = GameController.GetComponent<GameController>();
        InputType = (int)GameControllerScript.InputType;
		slider.minValue = LimitDown;
		slider.maxValue = LimitUp;
		slider.value = transform.position.y;
		currentSliderValue = slider.value;
	}
	
	void Update ()
	{
		float height = Mathf.Clamp (transform.position.y, LimitDown, LimitUp);

		h = Mathf.Abs(height);

		ElevatorObject.transform.localScale = new Vector3 (100 - h * Mathf.Tan(h/LimitUp),
		                                                  0,
		                                                   100 - h * Mathf.Tan(h/LimitUp));


		if (Input.GetAxis ("Vertical") != 0 && InputType == 0) {

			Vector3 pos = transform.position;
			pos.y = Mathf.Clamp(pos.y,LimitDown,LimitUp);
			transform.position = pos;


			transform.Translate (Vector3.up * Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime);
				}

		
		if (InputType == 1 && slider.value != currentSliderValue) {
			
			Vector3 pos = transform.position;
			pos.y = Mathf.Clamp(slider.value,LimitDown,LimitUp);
			transform.position = pos;

			currentSliderValue = slider.value;
		}




}

}