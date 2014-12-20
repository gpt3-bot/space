using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {
	
	public static string userName;
	public static bool connecting = false;
	public static bool connected = false;
	private static GameObject myShip;


	private static NetworkManager _instance;
	
	public static NetworkManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<NetworkManager>();
				
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

	
	// Use this for initialization
	void Start () {
		connecting = false;
		connected = false;
		userName = PlayerPrefs.GetString("userName", "RedShirt");
	}

	void OnDestroy() {
		PlayerPrefs.SetString("userName", userName);
	}
	
	void OnGUI() {
		if(!connecting) {
			GUILayout.BeginArea( new Rect(Screen.width/2 - 100, 0, 200, Screen.height) );
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			
			GUILayout.Label("Your Name: ");
			userName = GUILayout.TextField(userName, 24);
			
			GUILayout.Space(20);
			if(userName.Length > 0 && GUILayout.Button("Multi-Player")) {
				connecting = true;
				PhotonNetwork.ConnectUsingSettings("alpha 0.1");
			}
			
			GUILayout.Space(20);
			if(userName.Length > 0 && GUILayout.Button("Single-Player")) {
				connecting = true;
				PhotonNetwork.offlineMode = true; 
				PhotonNetwork.CreateRoom(null);

			}
			
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
		else if(!connected) {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		}
	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom(null);
	}
	
	void OnJoinedRoom() {
		connected = true;
		SpawnPlayer();
		CameraController.setPlayerCenter ();
	}
	
	void SpawnPlayer() {
		myShip = PhotonNetwork.Instantiate( "playerShip", Random.onUnitSphere * 2f, Quaternion.identity, 0 );
	}

	public static GameObject ReturnPlayer(){
		return myShip;
	}

}
