using UnityEngine;
using System.Collections;

public class ResizeElevator : MonoBehaviour {

	float x;
	float y = 0;
	float z;


	// Update is called once per frame
	void Update () {
						x = (50 - Mathf.Abs (transform.parent.transform.position.y)) * 2 + 5;
						z = (50 - Mathf.Abs (transform.parent.transform.position.y)) * 2 + 5;

		transform.localScale = new Vector3(x, y, z);

	}
}
