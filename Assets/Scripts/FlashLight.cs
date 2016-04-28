using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour
{
    public bool isCarried = false;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.GetComponent<Rigidbody>().useGravity == false)
        {
            isCarried = true;
        }

        if (this.GetComponent<Rigidbody>().useGravity == true)
        {
            isCarried = false;
        }

    }
}
