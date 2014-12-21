using UnityEngine;
using System.Collections;

public class RotateDirection : MonoBehaviour {

	public float speed = 20f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

if (Input.GetAxis ("Vertical") != 0) {
				
			transform.Rotate (Vector3.right * Input.GetAxis ("Vertical") * speed * Time.deltaTime);



			}
			
			if (Input.GetAxis ("Horizontal") != 0) {
				
				transform.Rotate (Vector3.back * Input.GetAxis ("Horizontal") * speed * Time.deltaTime);
			}


	}
}
