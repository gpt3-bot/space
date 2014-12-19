using UnityEngine;
using System.Collections;

public class RotateCameraVoiceControl : MonoBehaviour {
    private float rotateX = 0.0f;
    private float rotateY = 0.0f;
    private float zoom = 250f;
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

   public float GetRotateX()
    {
        return rotateX;
    }

   public float GetRotateY()
    {
        return rotateY;
    }
   public float GetZoom()
   {
       return zoom;

   }

}
