using UnityEngine;
using System.Collections;

public class RotateCameraVoiceControl : MonoBehaviour {


	
	
	private static RotateCameraVoiceControl _instance;
	
	public static RotateCameraVoiceControl instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<RotateCameraVoiceControl>();
				
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

    private static float rotateX = 0.0f;
	private static float rotateY = 0.0f;
	private static float zoom = 250f;
    Quaternion newRotation;
    // if not, you can omit this
    // you'll realize in the inspector a field GameObject will appear
    // assign it just by dragging the game object there
    private UDP_RecoServer server; // this will be the container of the script

    void Start()
    {
        // first you need to get the script component from game object A
        // getComponent can get any components, rigidbody, collider, etc from a game object
        // giving it <scriptA> meaning you want to get a component with type scriptA
        // note that if your script is not from another game object, you don't need "a."
        // script = a.gameObject.getComponent<scriptA>(); <-- this is a bit wrong, thanks to user2320445 for spotting that
        // don't need .gameObject because a itself is already a gameObject
        server = GetComponent<UDP_RecoServer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (server.UDPGetPacket() == "zoom out")
        {
            zoom = 350f;
        }
        if (server.UDPGetPacket() == "zoom in")
        {
            zoom = 100f;
        }

        if (server.UDPGetPacket() == "right")
        {
            rotateY = 45f;

        }

        if (server.UDPGetPacket() == "left")
        {
            rotateY = -45f;

        }

        if (server.UDPGetPacket() == "up")
        {
            rotateX = 45f;
        }

        if (server.UDPGetPacket() == "down")
        {
            rotateY = -45f;

        }

    }

   public static float GetRotateX()
    {
        return rotateX;
    }

	public static float GetRotateY()
    {
        return rotateY;
    }
	public static float GetZoom()
   {
       return zoom;

   }

}
