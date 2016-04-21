using UnityEngine;
using System.Collections;

public class DoorSequence : MonoBehaviour
{
    public bool doorOpened = false;
    public Quaternion angleY;

	// Use this for initialization
	void Start ()
    {
        angleY.y = this.transform.rotation.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.rotation.y > 0.9396927)
        {
            doorOpened = true;
        }
        else
        {
            doorOpened = false;
        }
	}
}
