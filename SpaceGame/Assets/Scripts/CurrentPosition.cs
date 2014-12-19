using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentPosition : MonoBehaviour {

	public GameObject playership;
	public GameObject elevator;
	private Text text;
	private Vector3 rotation;


	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();

	}

	
	// Update is called once per frame
	void Update () {
	
		text.text = "Current position: " + playership.transform.position.ToString () +
						"\nElevator Height: " + elevator.transform.position.y.ToString () +
						"\nElevator Width: " + elevator.transform.localScale.x.ToString ();
			;
	}
}
