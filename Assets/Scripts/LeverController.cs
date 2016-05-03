using UnityEngine;
using System.Collections;
using NewtonVR;

public class LeverController : MonoBehaviour {

    public GameObject gasAxle;
    public GameObject electricAxle;

    public bool gasOff = false;
    public bool electricityOff = false;

    public Quaternion angleZ;

	// Use this for initialization
	void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        angleZ.z = gasAxle.transform.rotation.z;

        if(electricAxle.transform.rotation.x > -0.0616284)
        {
            electricityOff = true;
        }

        else
        {
            //electricityOff = false;
        }

        if (gasAxle.transform.rotation.z < -0.4)
        {
            gasOff = true;
        }
        else
        {
            //gasOff = false;
        }
    }
}
