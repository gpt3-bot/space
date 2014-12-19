using UnityEngine;
using System.Collections;
using Meta;

public class TouchDebug : MetaBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTouch(){
		Debug.Log (Hands.left.pointer.position);
	}

}
