using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rotator : MonoBehaviour {
	
	public Slider sliderY;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (0, sliderY.value, 0);
	}
}
