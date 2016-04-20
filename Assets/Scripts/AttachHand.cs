using UnityEngine;
using System.Collections;

public class AttachHand : MonoBehaviour {

	public Transform controller;
	//private bool hammerIsAttached = false;

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(0, -1.33f, -5.83f);
    }
	
	// Update is called once per frame
	void Update ()
    {
       // transform.localPosition = new Vector3(0, -1.33f, -5.83f);
        transform.rotation	= controller.rotation;
       // transform.SetParent(controller);
        transform.Rotate(0,0,0);
        //hammerIsAttached = true;
    }

}
