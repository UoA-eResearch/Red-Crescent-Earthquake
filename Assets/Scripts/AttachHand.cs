using UnityEngine;
using System.Collections;

public class AttachHand : MonoBehaviour {

	public Transform controller;
	//private bool hammerIsAttached = false;

	// Use this for initialization
	void Start () {
        transform.position.Set(1000000, 3000000, -6000000);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation	= controller.rotation;
        //transform.SetParent(controller);
        transform.Rotate(0,0,0);
        //hammerIsAttached = true;
    }

}
