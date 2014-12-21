using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Elevator : MonoBehaviour {

public float moveSpeed = 20f;
public float LimitUp = 100f;
public float LimitDown = -100f;
public Slider slider;
private float currentSliderValue;
private int InputType;
	Transform PlayerCenter;


void Start() {
	InputType = GameController.returnInputType();
	slider.minValue = LimitDown;
	slider.maxValue = LimitUp;
	slider.value = transform.position.y;
	currentSliderValue = slider.value;
}

void Update ()
{
	
	if (PlayerCenter == null) {
		try {
			PlayerCenter = NetworkManager.ReturnPlayer ().transform;
		} catch {
			return;
		}
		
	} else {

			transform.position = new Vector3(PlayerCenter.position.x, transform.position.y, PlayerCenter.position.z);
	
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

}